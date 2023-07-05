using System.Net;
using System.Net.Mail;
using AdminBaker.Entities.Configuration;
using AdminBaker.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AdminBaker.Services.Implementations;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly Smtpconfiguration _smtpconfiguration;

    public EmailService(IOptions<AppConfig> options, ILogger<EmailService> logger)
    {
        _logger = logger;
        _smtpconfiguration = options.Value.SmtpConfiguration;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        try
        {
            var mailMessage = new MailMessage(new MailAddress(_smtpconfiguration.UserName, _smtpconfiguration.FromName),
                new MailAddress(email))
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            using var smtpClient = new SmtpClient(_smtpconfiguration.Server, _smtpconfiguration.PortNumber)
            {
                Credentials = new NetworkCredential(_smtpconfiguration.UserName, _smtpconfiguration.Password),
                EnableSsl = _smtpconfiguration.EnableSsl
            };

            await smtpClient.SendMailAsync(mailMessage);
        }
        catch (SmtpException ex)
        {
            _logger.LogWarning(ex, "No se puede enviar el correo {message}", ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Error al enviar el correo a {email} {message}", email, ex.Message);
        }
    }
}