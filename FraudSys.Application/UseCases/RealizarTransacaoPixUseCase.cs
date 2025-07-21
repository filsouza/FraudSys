using System.Threading.Tasks;
using FraudSys.Application.DTOs;
using FraudSys.Domain.Interfaces;
using FraudSys.Domain.Services;  

namespace FraudSys.Application.UseCases
{
  public class RealizarTransacaoPixUseCase
  {
    private readonly IClienteRepository _repository;

    public RealizarTransacaoPixUseCase(IClienteRepository repository)
    {
      _repository = repository;
    }

    public async Task<bool> ExecuteAsync(TransacaoPixDTO dto)
    {
      var cliente = await _repository.ObterPorCpfAsync(dto.cpf);
      if (cliente == null)
        return false; // Cliente não encontrado

      if (!TransacaoPolicy.PodeRealizar(cliente.LimitePix, dto.Valor))
        return false; // Transação negada - limite insuficiente

      var novoLimite = cliente.LimitePix - dto.Valor;
      await _repository.AtualizarLimiteAsync(dto.cpf, novoLimite);

      return true; // Transação aprovada
    }
  }
}
