using PurchaseAPI.Interfaces;
using PurchaseAPI.Models;

namespace PurchaseAPI.Services.Calculators
{
    public static class CalculatorFactory
    {
        public static ICalculator CreateCalculator(PurchaseDataType dataType)
        {
            ICalculator calculator;

            switch (dataType)
            {
                case PurchaseDataType.NET:
                    {
                        calculator = new NetInput();
                    }
                    break;
                case PurchaseDataType.GROSS:
                    {
                        calculator = new GrossInput();
                    }
                    break;
                case PurchaseDataType.VATAMOUNT:
                    {
                        calculator = new VatAmountInput();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Unknown data type: {dataType}");
            }

            return calculator;
        }
    }
}
