using System.Threading.Tasks;
using FraudSys.Application.DTOs;
using FraudSys.Application.UseCases.Interfaces;
using FraudSys.Domain.Interfaces;

namespace FraudSys.Application.UseCases
{
  public class ConsultarClienteUseCase : IConsultarClienteUseCase
  {
    private readonly IClienteRepository _repository;

    public ConsultarClienteUseCase(IClienteRepository repository)
    {
      _repository = repository;
    }

    public async Task<ClienteDTO> ExecuteAsync(string cpf)
    {
      var cliente = await _repository.ObterPorCpfAsync(cpf);
      if (cliente == null)
      {
        return null;
      }

      return new ClienteDTO
      {
        Cpf = cliente.Cpf,
        Agencia = cliente.Agencia,
        Conta = cliente.Conta,
        LimitePix = cliente.LimitePix
      };
    }
  }
}
