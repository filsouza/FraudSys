using System.Threading.Tasks;
using FraudSys.Application.UseCases.Interfaces;
using FraudSys.Domain.Interfaces;

namespace FraudSys.Application.UseCases
{
  public class AtualizarLimiteUseCase : IAtualizarLimiteUseCase
  {
    private readonly IClienteRepository _repository;

    public AtualizarLimiteUseCase(IClienteRepository repository)
    {
      _repository = repository;
    }

    public async Task ExecuteAsync(string cpf, decimal novoLimite)
    {
      await _repository.AtualizarLimiteAsync(cpf, novoLimite);
    }
  }
}
