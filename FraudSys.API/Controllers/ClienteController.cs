using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FraudSys.Application.DTOs;
using FraudSys.Application.UseCases;

namespace FraudSys.API.Controllers
{
  /// <summary>
  /// Controller para gerenciar clientes e seus limites para transações PIX.
  /// </summary>
  [ApiController]
  [Route("api/[controller]")]
  public class ClienteController : ControllerBase
  {
    private readonly CadastrarClienteUseCase _cadastrar;
    private readonly ConsultarClienteUseCase _consultar;
    private readonly AtualizarLimiteUseCase _atualizar;
    private readonly RemoverClienteUseCase _remover;

    public ClienteController(
        CadastrarClienteUseCase cadastrar,
        ConsultarClienteUseCase consultar,
        AtualizarLimiteUseCase atualizar,
        RemoverClienteUseCase remover)
    {
      _cadastrar = cadastrar;
      _consultar = consultar;
      _atualizar = atualizar;
      _remover = remover;
    }

    /// <summary>
    /// Cadastra um novo cliente com seus dados e limite para transações PIX.
    /// </summary>
    /// <param name="dto">Dados do cliente para cadastro.</param>
    /// <returns>Mensagem de sucesso.</returns>
    /// <response code="200">Cliente cadastrado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ClienteDTO dto)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      await _cadastrar.ExecuteAsync(dto);
      return Ok("Cliente cadastrado com sucesso");
    }

    /// <summary>
    /// Busca os dados de um cliente pelo CPF.
    /// </summary>
    /// <param name="cpf">CPF do cliente.</param>
    /// <returns>Dados do cliente.</returns>
    /// <response code="200">Cliente encontrado.</response>
    /// <response code="404">Cliente não encontrado.</response>
    [HttpGet("{cpf}")]
    public async Task<IActionResult> Get(string cpf)
    {
      var cliente = await _consultar.ExecuteAsync(cpf);
      if (cliente == null)
        return NotFound("Cliente não encontrado");

      return Ok(cliente);
    }

    /// <summary>
    /// Atualiza o limite para transações PIX de um cliente.
    /// </summary>
    /// <param name="cpf">CPF do cliente.</param>
    /// <param name="dto">Novo limite.</param>
    /// <returns>Mensagem de sucesso.</returns>
    /// <response code="200">Limite atualizado com sucesso.</response>
    [HttpPut("{cpf}/limite")]
    public async Task<IActionResult> Put(string cpf, [FromBody] AtualizarLimiteDTO dto)
    {
      await _atualizar.ExecuteAsync(cpf, dto.NovoLimite);
      return Ok("Limite atualizado com sucesso");
    }

    /// <summary>
    /// Remove um cliente pelo CPF.
    /// </summary>
    /// <param name="cpf">CPF do cliente.</param>
    /// <returns>Mensagem de sucesso.</returns>
    /// <response code="200">Cliente removido com sucesso.</response>
    [HttpDelete("{cpf}")]
    public async Task<IActionResult> Delete(string cpf)
    {
      await _remover.ExecuteAsync(cpf);
      return Ok("Cliente removido com sucesso");
    }
  }
}
