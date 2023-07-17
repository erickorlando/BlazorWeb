using AdminBaker.Services.Interfaces;
using AdminBaker.Shared.Request;
using Microsoft.AspNetCore.Mvc;

namespace AdminBaker.Server.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class PayPalController : ControllerBase
{
    private readonly IPayPalTransactionService _service;

    public PayPalController(IPayPalTransactionService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOrder(PaymentOrderDtoRequest request)
    {
        var response = await _service.CreateOrderAsync(request);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("{pedidoId:int}/{orderId}")]
    public async Task<IActionResult> CapturePayment(int pedidoId, string orderId)
    {
        var response = await _service.CapturePaymentAsync(pedidoId, orderId);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}