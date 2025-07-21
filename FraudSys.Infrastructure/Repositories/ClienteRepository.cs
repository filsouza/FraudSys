using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;
using FraudSys.Domain.Entities;
using FraudSys.Domain.Interfaces;
using FraudSys.Infrastructure.Config;
using FraudSys.Infrastructure.Dynamo;

namespace FraudSys.Infrastructure.Repositories
{
  public class ClienteRepository : IClienteRepository
  {
    private readonly ITable _table;

    public ClienteRepository(DynamoDbContext context, AWSSettings settings)
    {
      _table = Table.LoadTable(context.Client, settings.TableName);
    }

    public async Task CadastrarAsync(Cliente cliente)
    {
      var doc = new Document
      {
        ["cpf"] = cliente.Cpf,
        ["Agencia"] = cliente.Agencia,
        ["Conta"] = cliente.Conta,
        ["LimitePix"] = cliente.LimitePix
      };

      await _table.PutItemAsync(doc);
    }

    public async Task<Cliente> ObterPorCpfAsync(string cpf)
    {
      var doc = await _table.GetItemAsync(cpf);
      if (doc == null) return null;

      return new Cliente
      {
        Cpf = doc["cpf"],
        Agencia = doc["Agencia"],
        Conta = doc["Conta"],
        LimitePix = decimal.Parse(doc["LimitePix"].ToString())
      };
    }

    public async Task AtualizarLimiteAsync(string cpf, decimal novoLimite)
    {
      var cliente = await ObterPorCpfAsync(cpf);
      if (cliente == null) return;

      cliente.LimitePix = novoLimite;
      await CadastrarAsync(cliente);
    }

    public async Task RemoverAsync(string cpf)
    {
      await _table.DeleteItemAsync(cpf);
    }
  }
}
