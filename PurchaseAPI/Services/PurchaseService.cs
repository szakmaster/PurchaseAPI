using PurchaseAPI.Interfaces;
using PurchaseAPI.Models;
using PurchaseAPI.Services.Calculators;

namespace PurchaseAPI.Services
{
    public class PurchaseService : IPurchaseService
    {
        public PurchaseData? CalculatePurchaseData(double? net, double? gross, double? vatAmount, int vatRateInput)
        {
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
    }
}
