namespace FraudSys.Domain.Entities
{
  public class Cliente
  {
    public string Cpf { get; set; }
    public string Agencia { get; set; }
    public string Conta { get; set; }
    public decimal LimitePix { get; set; }
  }
}
