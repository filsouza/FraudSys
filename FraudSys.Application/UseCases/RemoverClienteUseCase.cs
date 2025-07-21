using System.Threading.Tasks;
using FraudSys.Application.UseCases.Interfaces;
using FraudSys.Domain.Interfaces;

namespace FraudSys.Application.UseCases
{
  public class RemoverClienteUseCase : IRemoverClienteUseCase
  {
    private readonly IClienteRepository _repository;

    public RemoverClienteUseCase(IClienteRepository repository)
    {
      _repository = repository;
    }

    public async Task ExecuteAsync(string cpf)
    {
      await _repository.RemoverAsync(cpf);
    }
  }
}
