using PurchaseAPI.Models;

namespace PurchaseAPI.Interfaces
{
    public interface ICalculator
    {
        PurchaseData CalculateData(double input, double vatRate);
    }
}
