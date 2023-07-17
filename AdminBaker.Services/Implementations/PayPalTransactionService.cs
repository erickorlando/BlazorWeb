using AdminBaker.Entities.Configuration;
using AdminBaker.Repositories.Interfaces;
using AdminBaker.Services.Interfaces;
using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using System.Text.Json;

namespace AdminBaker.Services.Implementations;

public class PayPalTransactionService : IPayPalTransactionService
{
    private readonly ILogger<PayPalTransactionService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPedidoRepository _pedidoRepository;
    private readonly AppConfig _appConfig;

    public PayPalTransactionService(IPedidoRepository pedidoRepository,
        IOptions<AppConfig> options,
        ILogger<PayPalTransactionService> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _pedidoRepository = pedidoRepository;
        _appConfig = options.Value;
    }

    private PayPalHttpClient Client()
    {
        PayPalEnvironment environment = new SandboxEnvironment(_appConfig.PayPalConfiguration.ClientId,
            _appConfig.PayPalConfiguration.SecretId);

        PayPalHttpClient client = new PayPalHttpClient(environment);

        return client;
    }

    public async Task<BaseResponseGeneric<PaymentOrderDtoResponse>> CreateOrderAsync(PaymentOrderDtoRequest request)
    {
        var response = new BaseResponseGeneric<PaymentOrderDtoResponse>();

        try
        {
            var pedido = await _pedidoRepository.FindByIdAsync(request.PedidoId);
            if (pedido is null)
            {
                response.ErrorMessage = "Pedido no encontrado";
                return response;
            }

            var host =
                $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext!.Request.Host}";

            var amountUsd = pedido.TotalVenta / 815;

            var order = new OrderRequest
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>()
                {
                    new PurchaseUnitRequest()
                    {
                        AmountWithBreakdown = new AmountWithBreakdown
                        {
                            CurrencyCode = "USD",
                            Value = amountUsd.ToString("#.00"),
                            AmountBreakdown = new AmountBreakdown
                            {
                                ItemTotal = new Money
                                {
                                    CurrencyCode = "USD",
                                    Value = amountUsd.ToString("#.00")
                                }
                            }
                        },
                    }
                },
                ApplicationContext = new ApplicationContext()
                {
                    ReturnUrl = $"{host}/PayPalTransaction/PaymentApproved",
                    CancelUrl = $"{host}/PayPalTransaction/Cancel"
                }
            };

            var orderRequest = new OrdersCreateRequest();
            orderRequest.Prefer("return=representation");
            orderRequest.RequestBody(order);
            var result = await Client().Execute(orderRequest);
            var resultOrder = result.Result<Order>();

            response.Data = new PaymentOrderDtoResponse()
            {
                ApproveUrl = resultOrder.Links?.FirstOrDefault(p => p.Rel == "approve")?.Href ?? string.Empty,
                OrderId = resultOrder.Id ?? string.Empty,
                PedidoId = pedido.Id
            };

            pedido.JsonPayPalResponse = JsonSerializer.Serialize(response.Data);

            await _pedidoRepository.UpdateAsync();
            
            response.Success = resultOrder.Status == "CREATED";
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al crear la transacción de PayPal";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponse> CapturePaymentAsync(int pedidoId, string orderId)
    {
        var response = new BaseResponse();

        try
        {
            var pedido = await _pedidoRepository.FindByIdAsync(pedidoId);
            if (pedido is null)
            {
                response.ErrorMessage = "No se encontró el pedido";
                return response;
            }

            var request = new OrdersCaptureRequest(orderId);
            request.RequestBody(new OrderActionRequest());
            var result = await Client().Execute(request);
            var resultOrder = result.Result<Order>(); 

            pedido.PayPalUrlOrder = resultOrder.Links?.FirstOrDefault(p => p.Rel == "self")?.Href ?? string.Empty;

            response.Success = resultOrder.Status == "COMPLETED";
            await _pedidoRepository.UpdateAsync();
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al intentar capturar el pago de la Orden de PayPal";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }
}