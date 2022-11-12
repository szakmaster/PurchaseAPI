using Microsoft.Extensions.Configuration;
using Moq;
using PurchaseAPI.Services;

namespace PurchaseAPITest
{
    public class PurchaseServiceTest
    {
        private readonly Mock<IConfiguration> _configuration;
        private const string VAT_RATES = "10,13,20";

        public PurchaseServiceTest()
        {
            _configuration = new Mock<IConfiguration>();
            var configSection = new Mock<IConfigurationSection>();
            configSection.Setup(x => x.Value).Returns(VAT_RATES);
            _configuration.Setup(x => x.GetSection("VATRates")).Returns(configSection.Object);
        }

        [Fact]
        public void CalculatePurchaseData_NetValueInput_Success()
        {
            // Arrange
            var purchaseService = new PurchaseService(_configuration.Object);
            var vatRateInput = 20;
            var netInputValue = 500;
            var expectedGrossValue = 600;
            var expectedVatAmountValue = 100;

            // Act
            var result = purchaseService.CalculatePurchaseData(netInputValue, null, null, vatRateInput);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result!.GrossAmount, expectedGrossValue);
            Assert.Equal(result!.VATAmount, expectedVatAmountValue);
        }

        [Fact]
        public void CalculatePurchaseData_GrossValueInput_Success()
        {
            // Arrange
            var purchaseService = new PurchaseService(_configuration.Object);
            var vatRateInput = 20;
            var grossInputValue = 600;
            var expectedNetValue = 500;
            var expectedVatAmountValue = 100;

            // Act
            var result = purchaseService.CalculatePurchaseData(null, grossInputValue, null, vatRateInput);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result!.NetAmount, expectedNetValue);
            Assert.Equal(result!.VATAmount, expectedVatAmountValue);
        }

        [Fact]
        public void CalculatePurchaseData_VATAmountValueInput_Success()
        {
            // Arrange
            var purchaseService = new PurchaseService(_configuration.Object);
            var vatRateInput = 20;
            var vatAmountInputValue = 100;
            var expectedNetValue = 500;
            var expectedGrossValue = 600;

            // Act
            var result = purchaseService.CalculatePurchaseData(null, null, vatAmountInputValue, vatRateInput);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result!.NetAmount, expectedNetValue);
            Assert.Equal(result!.GrossAmount, expectedGrossValue);
        }

        [Fact]
        public void CalculatePurchaseData_InvalidVATRatesFromConfig()
        {
            // Arrange
            var configSection = new Mock<IConfigurationSection>();
            configSection.Setup(x => x.Value).Returns(string.Empty);
            _configuration.Setup(x => x.GetSection("VATRates")).Returns(configSection.Object);

            var purchaseService = new PurchaseService(_configuration.Object);

            // Act, Assert
            Assert.Throws<InvalidDataException>(() => purchaseService.CalculatePurchaseData(
                    It.IsAny<double?>(), 
                    It.IsAny<double?>(), 
                    It.IsAny<double?>(), 
                    It.IsAny<int>()));
        }

        [Fact]
        public void CalculatePurchaseData_InvalidVATRateInput()
        {
            // Arrange
            var purchaseService = new PurchaseService(_configuration.Object);
            var invalidVATRate = 25;

            // Act, Assert
            Assert.Throws<InvalidDataException>(() => purchaseService.CalculatePurchaseData(
                    It.IsAny<double?>(),
                    It.IsAny<double?>(),
                    It.IsAny<double?>(),
                    invalidVATRate));
        }
    }
}
