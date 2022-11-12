using PurchaseAPI.Interfaces;
using PurchaseAPI.Models;

namespace PurchaseAPI.Services.Calculators
{
    public class VatAmountInput : ICalculator
    {
        public PurchaseData CalculateData(double input, double vatRate)
        {
            var calculatedNetValue = input / (vatRate / 100);
            var calculatedGrossValue = calculatedNetValue * (1 + vatRate / 100);

            return new PurchaseData
            {
                NetAmount = calculatedNetValue,
                GrossAmount = calculatedGrossValue,
                VATAmount = input
            };
        }
    }
}
