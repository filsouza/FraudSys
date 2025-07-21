using System.Threading.Tasks;
using Moq;
using Xunit;
using FraudSys.Application.UseCases;
using FraudSys.Domain.Entities;
using FraudSys.Domain.Interfaces;

namespace FraudSys.Tests.UseCases
{
  public class ConsultarClienteUseCaseTests
  {
    private readonly Mock<IClienteRepository> _mockRepo;
    private readonly ConsultarClienteUseCase _useCase;

    public ConsultarClienteUseCaseTests()
    {
      _mockRepo = new Mock<IClienteRepository>();
      _useCase = new ConsultarClienteUseCase(_mockRepo.Object);
    }

    [Fact(DisplayName = "ExecuteAsync retorna ClienteDTO com dados corretos quando cliente existe")]
    public async Task ExecuteAsync_DeveRetornarClienteDTO_QuandoClienteExiste()
    {
      // Arrange
      var cliente = new Cliente
      {
        Cpf = "12345678900",
        Agencia = "0001",
        Conta = "12345-6",
        LimitePix = 1500m
      };
      _mockRepo.Setup(r => r.ObterPorCpfAsync(cliente.Cpf))
               .ReturnsAsync(cliente);

      // Act
      var resultado = await _useCase.ExecuteAsync(cliente.Cpf);

      // Assert
      Assert.NotNull(resultado);
      Assert.Equal(cliente.Cpf, resultado.Cpf);
      Assert.Equal(cliente.Agencia, resultado.Agencia);
      Assert.Equal(cliente.Conta, resultado.Conta);
      Assert.Equal(cliente.LimitePix, resultado.LimitePix);

      // Verifica se método do repo foi chamado exatamente uma vez com o CPF correto
      _mockRepo.Verify(r => r.ObterPorCpfAsync(cliente.Cpf), Times.Once);
    }

    [Fact(DisplayName = "ExecuteAsync retorna null quando cliente não existe")]
    public async Task ExecuteAsync_DeveRetornarNull_QuandoClienteNaoExiste()
    {
      // Arrange
      var cpfInexistente = "99999999999";
      _mockRepo.Setup(r => r.ObterPorCpfAsync(cpfInexistente))
               .ReturnsAsync((Cliente)null);

      // Act
      var resultado = await _useCase.ExecuteAsync(cpfInexistente);

      // Assert
      Assert.Null(resultado);
      _mockRepo.Verify(r => r.ObterPorCpfAsync(cpfInexistente), Times.Once);
    }

    [Fact(DisplayName = "ExecuteAsync lança exceção se repositório lança exceção")]
    public async Task ExecuteAsync_DeveLancarExcecao_QuandoRepositorioFalha()
    {
      // Arrange
      var cpfTeste = "12345678900";
      _mockRepo.Setup(r => r.ObterPorCpfAsync(It.IsAny<string>()))
               .ThrowsAsync(new System.Exception("Erro inesperado"));

      // Act & Assert
      var ex = await Assert.ThrowsAsync<System.Exception>(async () =>
          await _useCase.ExecuteAsync(cpfTeste));

      Assert.Equal("Erro inesperado", ex.Message);
    }
  }
}
