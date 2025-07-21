using System.Threading.Tasks;
using Xunit;
using Moq;
using FraudSys.Application.UseCases;
using FraudSys.Domain.Interfaces;

namespace FraudSys.Tests.UseCases
{
  public class AtualizarLimiteUseCaseTests
  {
    [Fact]
    public async Task ExecuteAsync_DeveChamarRepositorioComParametrosCorretos()
    {
      // Arrange
      var mockRepository = new Mock<IClienteRepository>();
      var useCase = new AtualizarLimiteUseCase(mockRepository.Object);
      string cpfTeste = "12345678900";
      decimal novoLimiteTeste = 5000m;

      // Act
      await useCase.ExecuteAsync(cpfTeste, novoLimiteTeste);

      // Assert
      mockRepository.Verify(
          repo => repo.AtualizarLimiteAsync(cpfTeste, novoLimiteTeste),
          Times.Once);
    }
  }
}
