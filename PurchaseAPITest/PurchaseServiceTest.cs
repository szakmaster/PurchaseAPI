using Microsoft.Extensions.Configuration;
using Moq;
using PurchaseAPI.Services;

namespace PurchaseAPITest
{
    public class PurchaseServiceTest
    {
        [Fact]
        public void CalculatePurchaseData_NetValueInput_Success()
        {
            // Arrange
            var purchaseService = new PurchaseService();
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
            var purchaseService = new PurchaseService();
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
            var purchaseService = new PurchaseService();
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
    }
}
