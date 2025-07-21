using System.Threading.Tasks;
using FraudSys.Domain.Entities;

namespace FraudSys.Domain.Interfaces
{
  public interface IClienteRepository
  {
    Task CadastrarAsync(Cliente cliente);
    Task<Cliente> ObterPorCpfAsync(string cpf);
    Task AtualizarLimiteAsync(string cpf, decimal novoLimite);
    Task RemoverAsync(string cpf);
  }
}
