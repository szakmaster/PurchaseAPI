using Microsoft.AspNetCore.Mvc;
using PurchaseAPI.Extensions;
using PurchaseAPI.Interfaces;

namespace PurchaseAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PurchaseDataController : ControllerBase
    {
        private readonly ILogger<PurchaseDataController> _logger;
        private readonly IPurchaseService _purchaseService;

        public PurchaseDataController(ILogger<PurchaseDataController> logger, IPurchaseService purchaseService) 
        {
            _logger = logger;
            _purchaseService = purchaseService;
        }

        /// <summary>
        /// Calculates the missing purchase data
        /// </summary>
        /// <param name="net">Net amount of the purchase</param>
        /// <param name="gross">Gross amount of the purchase</param>
        /// <param name="vatAmount">VAT amount of the purchase</param>
        /// <param name="vatRate">The VAT Rate - REQUIRED parameter</param>
        /// <response code="200">Returns the calculated purchase data</response>
        /// <response code="400">If the VAT Rate is invalid, more than one input was given 
        /// (net, gross, VATAmount) or invalid data format.</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ServiceFilter(typeof(InputValidationAttribute))]
        public IActionResult GetPurchaseData(double? net, double? gross, double? vatAmount, int vatRate)
        {
            _logger.LogInformation("Calculate missing purchase data based on the given input and VAT Rate");

            var result = _purchaseService.CalculatePurchaseData(net, gross, vatAmount, vatRate);

            _logger.LogInformation("Calculation of purchase data completed.");

            return Ok(result);
        }
    }
}
