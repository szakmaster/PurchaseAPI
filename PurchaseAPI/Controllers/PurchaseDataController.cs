using Microsoft.AspNetCore.Mvc;
using PurchaseAPI.Extensions;
using PurchaseAPI.Interfaces;

namespace PurchaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseDataController : ControllerBase
    {
        private readonly ILogger<PurchaseDataController> _logger;
        private readonly IPurchaseService _purchaseService;

        public PurchaseDataController(ILogger<PurchaseDataController> logger, IPurchaseService purchaseService) 
        {
            _logger = logger;
            _purchaseService = purchaseService;
        }

        [HttpGet]
        [ServiceFilter(typeof(ModelValidationAttribute))]
        public IActionResult GetPurchaseData(double? net, double? gross, double? vatAmount, int vatRate)
        {
            _logger.LogInformation("Calculate missing purchase data based on the given input and VAT Rate");

            var result = _purchaseService.CalculatePurchaseData(net, gross, vatAmount, vatRate);

            _logger.LogInformation("Calculation of purchase data completed.");


            return Ok(result);
        }
    }
}
