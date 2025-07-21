namespace FraudSys.Domain.Services
{
  public static class TransacaoPolicy
  {
    /// <summary>
    /// Verifica se uma transação pode ser realizada com base no limite disponível.
    /// </summary>
    /// <param name="limite">Limite atual do cliente.</param>
    /// <param name="valor">Valor da transação desejada.</param>
    /// <returns>True se a transação pode ser aprovada, false caso contrário.</returns>
    public static bool PodeRealizar(decimal limite, decimal valor)
    {
      return limite >= valor;
    }
  }
}
