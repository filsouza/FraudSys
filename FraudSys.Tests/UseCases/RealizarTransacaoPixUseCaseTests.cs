using System.Threading.Tasks;
using Moq;
using Xunit;
using FraudSys.Application.DTOs;
using FraudSys.Application.UseCases;
using FraudSys.Domain.Entities;
using FraudSys.Domain.Interfaces;

namespace FraudSys.Tests.UseCases
{
  public class RealizarTransacaoPixUseCaseTests
  {
    private readonly Mock<IClienteRepository> _mockRepo;
    private readonly RealizarTransacaoPixUseCase _useCase;

    public RealizarTransacaoPixUseCaseTests()
    {
      _mockRepo = new Mock<IClienteRepository>();
      _useCase = new RealizarTransacaoPixUseCase(_mockRepo.Object);
    }

    [Fact(DisplayName = "ExecuteAsync retorna false quando cliente não existe")]
    public async Task ExecuteAsync_DeveRetornarFalse_QuandoClienteNaoExiste()
    {
      // Arrange
      string cpf = "99999999999";
      _mockRepo.Setup(r => r.ObterPorCpfAsync(cpf))
               .ReturnsAsync((Cliente)null);

      var dto = new TransacaoPixDTO { cpf = cpf, Valor = 100 };

      // Act
      var resultado = await _useCase.ExecuteAsync(dto);

      // Assert
      Assert.False(resultado);
      _mockRepo.Verify(r => r.ObterPorCpfAsync(cpf), Times.Once);
      _mockRepo.Verify(r => r.AtualizarLimiteAsync(It.IsAny<string>(), It.IsAny<decimal>()), Times.Never);
    }

    [Fact(DisplayName = "ExecuteAsync retorna false quando limite é insuficiente")]
    public async Task ExecuteAsync_DeveRetornarFalse_QuandoLimiteInsuficiente()
    {
      // Arrange
      string cpf = "12345678900";
      var cliente = new Cliente { Cpf = cpf, LimitePix = 50 };
      _mockRepo.Setup(r => r.ObterPorCpfAsync(cpf))
               .ReturnsAsync(cliente);

      var dto = new TransacaoPixDTO { cpf = cpf, Valor = 100 };

      // Act
      var resultado = await _useCase.ExecuteAsync(dto);

      // Assert
      Assert.False(resultado);
      _mockRepo.Verify(r => r.ObterPorCpfAsync(cpf), Times.Once);
      _mockRepo.Verify(r => r.AtualizarLimiteAsync(It.IsAny<string>(), It.IsAny<decimal>()), Times.Never);
    }

    [Fact(DisplayName = "ExecuteAsync atualiza limite e retorna true quando transação válida")]
    public async Task ExecuteAsync_DeveAtualizarLimiteERetornarTrue_QuandoTransacaoValida()
    {
      // Arrange
      string cpf = "12345678900";
      decimal limiteInicial = 500;
      decimal valorTransacao = 100;
      var cliente = new Cliente { Cpf = cpf, LimitePix = limiteInicial };

      _mockRepo.Setup(r => r.ObterPorCpfAsync(cpf))
               .ReturnsAsync(cliente);

      _mockRepo.Setup(r => r.AtualizarLimiteAsync(cpf, limiteInicial - valorTransacao))
               .Returns(Task.CompletedTask);

      var dto = new TransacaoPixDTO { cpf = cpf, Valor = valorTransacao };

      // Act
      var resultado = await _useCase.ExecuteAsync(dto);

      // Assert
      Assert.True(resultado);
      _mockRepo.Verify(r => r.ObterPorCpfAsync(cpf), Times.Once);
      _mockRepo.Verify(r => r.AtualizarLimiteAsync(cpf, limiteInicial - valorTransacao), Times.Once);
    }
  }
}
