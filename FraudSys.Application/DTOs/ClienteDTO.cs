namespace FraudSys.Application.DTOs
{
  using System.ComponentModel.DataAnnotations;

  public class ClienteDTO
  {
    [Required]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve ter 11 dígitos.")]
    public string Cpf { get; set; }

    [Required]
    public string Agencia { get; set; }

    [Required]
    public string Conta { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "LimitePix deve ser positivo.")]
    public decimal LimitePix { get; set; }
  }
}
