using System.Threading.Tasks;

namespace FraudSys.Application.UseCases.Interfaces
{
  public interface IRemoverClienteUseCase
  {
    Task ExecuteAsync(string cpf);
  }
}
