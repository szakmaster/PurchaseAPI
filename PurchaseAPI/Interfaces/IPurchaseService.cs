using PurchaseAPI.Models;

namespace PurchaseAPI.Interfaces
{
    public interface IPurchaseService
    {
        PurchaseData? CalculatePurchaseData(double? net, double? gross, double? vatAmount, int vatRateInput);
    }
}
