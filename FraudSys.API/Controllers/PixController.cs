using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FraudSys.Application.DTOs;
using FraudSys.Application.UseCases;

namespace FraudSys.API.Controllers
{
  /// <summary>
  /// Controller para processar transações PIX com verificação de limite.
  /// </summary>
  [ApiController]
  [Route("api/[controller]")]
  public class PixController : ControllerBase
  {
    private readonly RealizarTransacaoPixUseCase _useCase;

    public PixController(RealizarTransacaoPixUseCase useCase)
    {
      _useCase = useCase;
    }

    /// <summary>
    /// Realiza uma transação PIX descontando do limite do cliente.
    /// </summary>
    /// <param name="dto">Dados da transação PIX.</param>
    /// <returns>Resultado da transação.</returns>
    /// <response code="200">Transação aprovada.</response>
    /// <response code="400">Transação negada: limite insuficiente ou cliente não encontrado.</response>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TransacaoPixDTO dto)
    {
      var sucesso = await _useCase.ExecuteAsync(dto);
      if (!sucesso)
        return BadRequest("Transação negada: limite insuficiente ou cliente não encontrado");

      return Ok("Transação aprovada");
    }
  }
}
