using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PurchaseAPI.Extensions
{
    public class InputValidationAttribute : ActionFilterAttribute
    {
        private const int MAX_ALLOWED_NUM_OF_PARAMS = 2;
        private readonly IConfiguration _configuration;

        public InputValidationAttribute(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Check the existence of VAT rate and the number of given input values
            if (context.ModelState.Keys.Any(i => i == "vatRate"))
            {
                if (context.ModelState.Count != MAX_ALLOWED_NUM_OF_PARAMS)
                {
                    context.Result = new BadRequestObjectResult("Incorrect number of input parameter(s) was entered." +
                        " (One input data and the VAT Rate is allowed!)");

                    return;
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult("VAT rate is missing!");

                return;
            }

            // Parse and check the input values from the request
            // VAT Rate
            var vatRateRawValue = context.ModelState
                .Where(i => i.Key == "vatRate")
                .Select(i => i.Value)
                .Single()?
                .RawValue?
                .ToString();
            
            var successParseVatRate = int.TryParse(vatRateRawValue, out int vatRate);
            if (successParseVatRate)
            {
                var vatRatesList = _configuration.GetValue<string>("VATRates")?.Split(",")?.ToList();
                var errorMessage = ValidateVATRate(vatRatesList, vatRate);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    context.Result = new BadRequestObjectResult(errorMessage);

                    return;
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult("Invalid VAT Rate format!");

                return;
            }

            // Input data
            var inputRawValue = context.ModelState.Where(i => i.Key != "vatRate").Select(i => i.Value).Single()?.RawValue?.ToString();
            var successParseInputValue = double.TryParse(inputRawValue, out double inputValue);

            if (!successParseVatRate)
            {
                context.Result = new BadRequestObjectResult("Invalid input data format!");

                return;
            }

            if (inputValue <= 0)
            {
                context.Result = new BadRequestObjectResult("Input data should be greater than zero");
            }
        }

        private static string ValidateVATRate(List<string>? vatRatesList, int vatRateInput)
        {
            if (vatRatesList == null || !vatRatesList.Any())
            {
                return "Pre-defined allowed VAT rates cannot be loaded.";
            }

            var vatRates = vatRatesList.Select(s => int.TryParse(s, out int number) ? number : (int?)null)
                               .Where(n => n.HasValue)
                               .Select(n => n!.Value)
                               .ToList();

            if (!vatRates.Contains(vatRateInput))
            {
                return $"Invalid VAT rate {vatRateInput}%. It should be one of {string.Join(", ", vatRates)}";
            }

            return string.Empty;
        }
    }
}
