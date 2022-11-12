using PurchaseAPI.Interfaces;
using PurchaseAPI.Models;
using PurchaseAPI.Services.Calculators;

namespace PurchaseAPI.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IConfiguration _configuration;

        public PurchaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public PurchaseData? CalculatePurchaseData(double? net, double? gross, double? vatAmount, int vatRateInput)
        {
            var vatRatesList = _configuration.GetValue<string>("VATRates")?.Split(",")?.ToList();

            ValidateVATRate(vatRatesList, vatRateInput);

            // Find the only one purchase data that will be the input to calculate missing data of the purchase 
            var purchaseDataInput = new Dictionary<PurchaseDataType, double?> {
                { PurchaseDataType.NET, net },
                { PurchaseDataType.GROSS, gross },
                { PurchaseDataType.VATAMOUNT, vatAmount }
            }.Where(i => i.Value.HasValue).Single();

            var purchaseDataCalculator = CalculatorFactory.CreateCalculator(purchaseDataInput.Key);
            var purchaseDataResult = purchaseDataCalculator.CalculateData((double)purchaseDataInput.Value!, (double)vatRateInput);

            return purchaseDataResult;
        }

        private static void ValidateVATRate(List<string>? vatRatesList, int vatRateInput)
        {
            if (vatRatesList == null || !vatRatesList.Any())
            {
                throw new InvalidDataException("VAT rates cannot be found.");
            }

            var vatRates = vatRatesList.Select(s => int.TryParse(s, out int number) ? number : (int?)null)
                               .Where(n => n.HasValue)
                               .Select(n => n!.Value)
                               .ToList();

            if (!vatRates.Contains(vatRateInput))
            {
                throw new InvalidDataException($"Invalid VAT rate {vatRateInput} %. It should be one of {string.Join(", ", vatRates)}");
            }
        }
    }
}
