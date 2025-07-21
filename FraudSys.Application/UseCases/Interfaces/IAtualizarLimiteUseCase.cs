using System.Threading.Tasks;

namespace FraudSys.Application.UseCases.Interfaces
{
  public interface IAtualizarLimiteUseCase
  {
    Task ExecuteAsync(string cpf, decimal novoLimite);
  }
}
