using System.Threading.Tasks;
using FraudSys.Application.DTOs;
using FraudSys.Application.UseCases.Interfaces;
using FraudSys.Domain.Entities;
using FraudSys.Domain.Interfaces;

namespace FraudSys.Application.UseCases
{
  public class CadastrarClienteUseCase : ICadastrarClienteUseCase
  {
    private readonly IClienteRepository _repository;

    public CadastrarClienteUseCase(IClienteRepository repository)
    {
      _repository = repository;
    }

    public async Task ExecuteAsync(ClienteDTO dto)
    {
      var cliente = new Cliente
      {
        Cpf = dto.Cpf,
        Agencia = dto.Agencia,
        Conta = dto.Conta,
        LimitePix = dto.LimitePix
      };

      await _repository.CadastrarAsync(cliente);
    }
  }
}
