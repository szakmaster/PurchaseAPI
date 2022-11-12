using PurchaseAPI.Models;
using PurchaseAPI.Services.Calculators;

namespace PurchaseAPITest
{
    public class CalculatorsTest
    {
        [Fact]
        public void CalculatorFactory_CreateCalculatorNetInput_Success()
        {
            // Arrange
            var inputDataType = PurchaseDataType.NET;

            // Act
            var calculator = CalculatorFactory.CreateCalculator(inputDataType);

            // Assert
            Assert.True(calculator is NetInput);
        }

        [Fact]
        public void CalculatorFactory_CreateCalculatorGrossInput_Success()
        {
            // Arrange
            var inputDataType = PurchaseDataType.GROSS;

            // Act
            var calculator = CalculatorFactory.CreateCalculator(inputDataType);

            // Assert
            Assert.True(calculator is GrossInput);
        }

        [Fact]
        public void CalculatorFactory_CreateCalculatorVatAmountInput_Success()
        {
            // Arrange
            var inputDataType = PurchaseDataType.VATAMOUNT;

            // Act
            var calculator = CalculatorFactory.CreateCalculator(inputDataType);

            // Assert
            Assert.True(calculator is VatAmountInput);
        }
    }
}