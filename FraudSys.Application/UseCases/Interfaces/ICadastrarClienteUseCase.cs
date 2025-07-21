using FraudSys.Application.DTOs;
using System.Threading.Tasks;

namespace FraudSys.Application.UseCases.Interfaces
{
  public interface ICadastrarClienteUseCase
  {
    Task ExecuteAsync(ClienteDTO cliente);
  }
}
