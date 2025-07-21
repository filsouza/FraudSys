using System.Threading.Tasks;
using Xunit;
using Moq;
using FraudSys.Application.DTOs;
using FraudSys.Application.UseCases;
using FraudSys.Domain.Entities;
using FraudSys.Domain.Interfaces;

namespace FraudSys.Tests.UseCases
{
  public class CadastrarClienteUseCaseTests
  {
    [Fact]
    public async Task ExecuteAsync_DeveCadastrarClienteComSucesso()
    {
      // Arrange
      var mockRepo = new Mock<IClienteRepository>();
      var useCase = new CadastrarClienteUseCase(mockRepo.Object);

      var dto = new ClienteDTO
      {
        Cpf = "12345678900",
        Agencia = "0001",
        Conta = "12345-6",
        LimitePix = 1000.00m
      };

      // Act
      await useCase.ExecuteAsync(dto);

      // Assert
      mockRepo.Verify(repo => repo.CadastrarAsync(It.Is<Cliente>(c =>
          c.Cpf == dto.Cpf &&
          c.Agencia == dto.Agencia &&
          c.Conta == dto.Conta &&
          c.LimitePix == dto.LimitePix
      )), Times.Once);
    }
  }
}
