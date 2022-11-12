using PurchaseAPI.Interfaces;
using PurchaseAPI.Models;

namespace PurchaseAPI.Services.Calculators
{
    public class NetInput : ICalculator
    {
        public PurchaseData CalculateData(double input, double vatRate)
        {
            var calculatedGrossValue = input * (1 + vatRate / 100);
            var calculatedVatValue = input * (vatRate / 100);

            return new PurchaseData
            {
                NetAmount = input,
                GrossAmount = calculatedGrossValue,
                VATAmount = calculatedVatValue
            };
        }
    }
}
