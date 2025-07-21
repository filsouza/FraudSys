using Microsoft.AspNetCore.Mvc;
using FraudSys.Application.DTOs;
using FraudSys.Application.UseCases.Interfaces;
using System.Threading.Tasks;

namespace FraudSys.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ClienteController : ControllerBase
  {
    private readonly ICadastrarClienteUseCase _cadastrar;
    private readonly IConsultarClienteUseCase _consultar;
    private readonly IAtualizarLimiteUseCase _atualizar;
    private readonly IRemoverClienteUseCase _remover;

    public ClienteController(
        ICadastrarClienteUseCase cadastrar,
        IConsultarClienteUseCase consultar,
        IAtualizarLimiteUseCase atualizar,
        IRemoverClienteUseCase remover)
    {
      _cadastrar = cadastrar;
      _consultar = consultar;
      _atualizar = atualizar;
      _remover = remover;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ClienteDTO dto)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      await _cadastrar.ExecuteAsync(dto);
      return Ok("Cliente cadastrado com sucesso");
    }

    [HttpGet("{cpf}")]
    public async Task<IActionResult> Get(string cpf)
    {
      var cliente = await _consultar.ExecuteAsync(cpf);
      if (cliente == null)
        return NotFound("Cliente não encontrado");

      return Ok(cliente);
    }

    [HttpPut("{cpf}/limite")]
    public async Task<IActionResult> Put(string cpf, [FromBody] AtualizarLimiteDTO dto)
    {
      await _atualizar.ExecuteAsync(cpf, dto.NovoLimite);
      return Ok("Limite atualizado com sucesso");
    }

    [HttpDelete("{cpf}")]
    public async Task<IActionResult> Delete(string cpf)
    {
      await _remover.ExecuteAsync(cpf);
      return Ok("Cliente removido com sucesso");
    }
  }
}
