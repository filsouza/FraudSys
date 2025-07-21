using FraudSys.Application.DTOs;
using System.Threading.Tasks;

namespace FraudSys.Application.UseCases.Interfaces
{
  public interface IConsultarClienteUseCase
  {
    Task<ClienteDTO?> ExecuteAsync(string cpf);
  }
}
