using AdminBaker.DataAccess;
using AdminBaker.Entities.Configuration;
using AdminBaker.Entities;
using AdminBaker.Repositories.Interfaces;
using AdminBaker.Services.Interfaces;
using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;
using AdminBaker.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace AdminBaker.Services.Implementations;

public class UserService : IUserService
{
    private readonly UserManager<IdentityUserECommerce> _userManager;
    private readonly ILogger<UserService> _logger;
    private readonly IClienteRepository _clienteRepository;
    private readonly IVendedorRepository _vendedorRepository;
    private readonly IEmailService _emailService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Jwt _jwt;

    public UserService(UserManager<IdentityUserECommerce> userManager,
        ILogger<UserService> logger,
        IClienteRepository clienteRepository,
        IOptions<AppConfig> options,
        IEmailService emailService,
        IHttpContextAccessor httpContextAccessor,
        IVendedorRepository vendedorRepository)
    {
        _userManager = userManager;
        _logger = logger;
        _clienteRepository = clienteRepository;
        _emailService = emailService;
        _httpContextAccessor = httpContextAccessor;
        _vendedorRepository = vendedorRepository;
        _jwt = options.Value.Jwt;
    }

    public async Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request)
    {
        const string mensajeDesactivado = "Su usuario fue desactivado, no puede iniciar sesion";

        var response = new LoginDtoResponse();
        try
        {
            var identity = await _userManager.FindByEmailAsync(request.UserName);

            if (identity is null)
                throw new SecurityException("Usuario no existe");

            if (await _userManager.IsLockedOutAsync(identity))
                throw new SecurityException($"Demasiados intentos fallidos para el usuario {identity.NombreCompleto}");

            var result = await _userManager.CheckPasswordAsync(identity, request.Password);
            if (!result)
            {
                await _userManager.AccessFailedAsync(identity);
                throw new SecurityException($"Clave incorrecta para el usuario {identity.UserName}");
            }

            var roles = await _userManager.GetRolesAsync(identity);

            var fechaExpiracion = DateTime.Now.AddDays(1);

            // Vamos a crear los claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, identity.NombreCompleto),
                new Claim(ClaimTypes.Sid, identity.Rut),
                new Claim(ClaimTypes.MobilePhone, identity.PhoneNumber ?? string.Empty),
                new Claim(ClaimTypes.Email, request.UserName),
                new Claim(ClaimTypes.Expiration, fechaExpiracion.ToString("yyyy-MM-dd HH:mm:ss"))
            };

            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));
            response.Roles = new List<string>();
            response.Roles.AddRange(roles);

            if (response.Roles.Contains(Constantes.RolVendedor))
            {
                var vendedor = await _vendedorRepository.FindByEmailAsync(request.UserName);
                if (vendedor is not null)
                {
                    claims.Add(new Claim("IdVendedor", vendedor.Id.ToString()));
                    if (!vendedor.Estado)
                    {
                        throw new SecurityException(mensajeDesactivado);
                    }
                }
            }

            if (response.Roles.Contains(Constantes.RolCliente))
            {
                var cliente = await _clienteRepository.FindByEmailAsync(request.UserName);
                if (cliente is not null)
                {
                    claims.Add(new Claim("IdCliente", cliente.Id.ToString()));
                    if (!cliente.Estado)
                    {
                        throw new SecurityException(mensajeDesactivado);
                    }
                }
            }

            // Creacion del JWT
            var llaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SecretKey));
            var credenciales = new SigningCredentials(llaveSimetrica, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(credenciales);

            var payload = new JwtPayload(
                issuer: _jwt.Emisor,
                audience: _jwt.Audiencia,
                notBefore: DateTime.Now,
                claims: claims,
                expires: fechaExpiracion);

            var token = new JwtSecurityToken(header, payload);
            response.Token = new JwtSecurityTokenHandler().WriteToken(token);
            response.FullName = $"{identity.NombreCompleto}";
            response.Success = true;

            _logger.LogInformation("Se creó el JWT de forma satisfactoria");
        }
        catch (SecurityException ex)
        {
            response.ErrorMessage = ex.Message;
            _logger.LogWarning(ex, "Error de seguridad {Message}", ex.Message);
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error de autenticacion";
            _logger.LogCritical(ex, "Error de autenticacion {Message}", ex.Message);
        }
        return response;
    }

    public async Task<BaseResponse> RegisterAsync(RegistrarUsuarioDto request)
    {
        var response = new BaseResponse();

        try
        {
            var user = new IdentityUserECommerce
            {
                Rut = request.Rut,
                NombreCompleto = request.NombreCompleto,
                Email = request.Email,
                Direccion = request.Direccion,
                FechaNacimiento = request.FechaNacimiento,
                PhoneNumber = request.Telefono,
                EmailConfirmed = true,
                UserName = request.Email,
                Latitud = request.Latitud,
                Longitud = request.Longitud,
            };

            var result = await _userManager.CreateAsync(user, request.Clave);

            if (result.Succeeded)
            {
                var userIdentity = await _userManager.FindByEmailAsync(request.Email);

                if (userIdentity is not null)
                {
                    if (request.Vendedor)
                    {
                        await _userManager.AddToRoleAsync(userIdentity, Constantes.RolVendedor);

                        var vendedor = new Vendedor
                        {
                            CodigoTrabajador = userIdentity.Rut,
                            NombreCompleto = userIdentity.NombreCompleto,
                            Email = userIdentity.Email!,
                            Rut = userIdentity.Rut,
                            Direccion = userIdentity.Direccion,
                            Horario = "08:00 - 18:00",
                            Estado = false
                        };

                        await _vendedorRepository.AddAsync(vendedor);

                        // Mandar un correo electronico, felicitando la creacion de la cuenta.
                        await _emailService.SendEmailAsync(request.Email, $"{vendedor.NombreCompleto}, Bienvenido al sistema Admin Baker, debe esperar a que un administrador habilite su usuario", @"<p style=""font-family:Verdana,Helvetica"">Gracias por registrarse en AdminBaker Web</p>");
                    }
                    else
                    {

                        await _userManager.AddToRoleAsync(userIdentity, Constantes.RolCliente);

                        var cliente = new Cliente
                        {
                            NombreCompleto = userIdentity.NombreCompleto,
                            Email = userIdentity.Email!,
                            Rut = userIdentity.Rut,
                            Direccion = userIdentity.Direccion,
                            FechaNacimiento = userIdentity.FechaNacimiento,
                            Latitud = userIdentity.Latitud,
                            Longitud = userIdentity.Longitud,
                            Estado = true
                        };

                        await _clienteRepository.AddAsync(cliente);

                        // Mandar un correo electronico, felicitando la creacion de la cuenta.
                        await _emailService.SendEmailAsync(request.Email,
                            $"{cliente.NombreCompleto}, Bienvenido al sistema Admin Baker",
                            @"<p style=""font-family:Verdana,Helvetica"">Gracias por registrarse en AdminBaker Web</p>");
                    }

                    response.Success = true;
                }
            }
            else
            {
                response.Success = false;
                var sb = new StringBuilder();
                foreach (var identityError in result.Errors)
                {
                    sb.AppendLine(identityError.Description);
                }

                response.ErrorMessage = sb.ToString();
                sb.Clear();
            }
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al crear el usuario";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponse> SendTokenToResetPasswordAsync(GenerateTokenToResetDtoRequest request)
    {
        var response = new BaseResponse();

        try
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                throw new SecurityException("Usuario no existe");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // codificamos el token
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var host = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext!.Request.Host}";

            await _emailService.SendEmailAsync(request.Email, "Admin Baker - Solicitud de reseteo de clave",
                @$"<p>Para recuperar su clave, haga click en el siguiente enlace <a href=""{host}/reset-password?email={request.Email}&token={token}"">Recuperar clave</a></p>");

            response.Success = true;
        }
        catch (SecurityException ex)
        {
            response.ErrorMessage = ex.Message;
            _logger.LogWarning(ex, "Se intentó resetear el password del usuario {email} {Message}", request.Email, response.ErrorMessage);
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al solicitar el token para recuperar la clave";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponse> ResetPasswordAsync(ResetPasswordDtoRequest request)
    {
        var response = new BaseResponse();
        try
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                throw new SecurityException("Usuario no existe");

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Clave);
            response.Success = result.Succeeded;

            if (!result.Succeeded)
            {
                var sb = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    sb.AppendLine(error.Description);
                }

                response.ErrorMessage = sb.ToString();
                sb.Clear();
            }
            else
            {
                // enviamos un email con la confirmación de que se cambio la clave con exito
                await _emailService.SendEmailAsync(request.Email, "Admin Baker - Cambio de clave",
                                       "<p>Su clave ha sido cambiada con exito</p>");
            }
        }
        catch (SecurityException ex)
        {
            response.ErrorMessage = ex.Message;
            _logger.LogWarning(ex, "Se intentó resetear el password del usuario {email} {Message}", request.Email, response.ErrorMessage);
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al recuperar la clave";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponse> ChangePasswordAsync(ChangePasswordDtoRequest request)
    {
        var response = new BaseResponse();
        try
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                throw new SecurityException("Usuario no existe");

            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            response.Success = result.Succeeded;

            if (!result.Succeeded)
            {
                var sb = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    sb.AppendLine(error.Description);
                }

                response.ErrorMessage = sb.ToString();
                sb.Clear();
            }
            else
            {
                // enviamos un email con la confirmación de que se cambio la clave con exito
                await _emailService.SendEmailAsync(request.Email, "Admin Baker - Cambio de clave",
                    "<p>Su clave ha sido cambiada con exito</p>");
            }
        }
        catch (SecurityException ex)
        {
            response.ErrorMessage = ex.Message;
            _logger.LogWarning(ex, "Se intentó cambiar el password del usuario {email} {Message}", request.Email,
                response.ErrorMessage);
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al cambiar la clave";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponse> UpdateProfileAsync(UpdateProfileDtoRequest request)
    {
        var response = new BaseResponse();
        try
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                throw new SecurityException("Usuario no existe");

            user.NombreCompleto = request.NombreCompleto;
            user.Direccion = request.Direccion;
            user.FechaNacimiento = request.FechaNacimiento;
            user.Latitud = request.Latitud;
            user.Longitud = request.Longitud;

            // Si el usuario tiene Rol de Vendedor, tambien se actualiza la tabla de Vendedores
            if (await _userManager.IsInRoleAsync(user, Constantes.RolVendedor))
            {
                var vendedor = await _vendedorRepository.FindByEmailAsync(request.Email);
                if (vendedor is null)
                    throw new SecurityException("Vendedor no existe");

                vendedor.NombreCompleto = request.NombreCompleto;
                vendedor.Rut = request.Rut;
                vendedor.Direccion = request.Direccion;

                await _vendedorRepository.UpdateAsync();
            }
            // Si el usuario tiene Rol de Cliente, tambien se actualiza la tabla de Clientes
            else if (await _userManager.IsInRoleAsync(user, Constantes.RolCliente))
            {
                var cliente = await _clienteRepository.FindByEmailAsync(request.Email);
                if (cliente is null)
                    throw new SecurityException("Cliente no existe");

                cliente.NombreCompleto = request.NombreCompleto;
                cliente.Rut = request.Rut;
                cliente.Direccion = request.Direccion;
                cliente.FechaNacimiento = request.FechaNacimiento;
                cliente.Latitud = request.Latitud;
                cliente.Longitud = request.Longitud;

                await _clienteRepository.UpdateAsync();
            }

            var result = await _userManager.UpdateAsync(user);
            response.Success = result.Succeeded;

            if (!result.Succeeded)
            {
                var sb = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    sb.AppendLine(error.Description);
                }

                response.ErrorMessage = sb.ToString();
                sb.Clear();
            }

        }
        catch (SecurityException ex)
        {
            response.ErrorMessage = ex.Message;
            _logger.LogWarning(ex, "Se intentó actualizar el perfil del usuario {email} {Message}", request.Email,
                response.ErrorMessage);
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al actualizar el perfil";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<ClienteDto>> GetProfileAsync(string email)
    {
        var response = new BaseResponseGeneric<ClienteDto>();

        try
        {
            var userIdentity = await _userManager.FindByEmailAsync(email);
            if (userIdentity is null)
                throw new SecurityException("Usuario no existe");

            response.Data = new ClienteDto
            {
                NombreCompleto = userIdentity.NombreCompleto,
                Direccion = userIdentity.Direccion,
                Email = userIdentity.Email!,
                FechaNacimiento = userIdentity.FechaNacimiento,
                Rut = userIdentity.Rut,
                Latitud = userIdentity.Latitud,
                Longitud = userIdentity.Longitud,
                Estado = "Activo"
            };
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al obtener el perfil";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }
}