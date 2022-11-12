using PurchaseAPI.Interfaces;
using PurchaseAPI.Models;

namespace PurchaseAPI.Services.Calculators
{
    public class GrossInput : ICalculator
    {
        public PurchaseData CalculateData(double input, double vatRate)
        {
            var calculatedNetValue = input / (1 + vatRate / 100);
            var calculatedVatValue = input - calculatedNetValue;

            return new PurchaseData
            {
                NetAmount = calculatedNetValue,
                GrossAmount = input,
                VATAmount = calculatedVatValue
            };
        }
    }
}
