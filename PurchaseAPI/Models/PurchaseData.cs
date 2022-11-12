namespace PurchaseAPI.Models
{
    public class PurchaseData
    {
        public double NetAmount { get; set; }
        public double GrossAmount { get; set; }
        public double VATAmount { get; set; }
    }

    public enum PurchaseDataType
    {
        NET,
        GROSS,
        VATAMOUNT
    }
}
