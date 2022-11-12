using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PurchaseAPI.Extensions
{
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        private const int MAX_ALLOWED_NUM_OF_PARAMS = 2;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);

                return;
            }

            // Check the VAT rate and the number of given input values
            if (context.ModelState.Keys.Any(i => i == "vatRate"))
            {
                if (context.ModelState.Count != MAX_ALLOWED_NUM_OF_PARAMS)
                {
                    context.Result = new BadRequestObjectResult("Too few or too much input data was entered." +
                        " (One input data and the VAT Rate is allowed!)");

                    return;
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult("VAT rate is missing!");

                return;
            }

            // Check the type of input values
            var vatRateRawValue = context.ModelState.Where(i => i.Key == "vatRate").Select(i => i.Value).Single()?.RawValue?.ToString();
            var successParseVatRate = int.TryParse(vatRateRawValue, out int vatRate);

            var inputRawValue = context.ModelState.Where(i => i.Key != "vatRate").Select(i => i.Value).Single()?.RawValue?.ToString();
            var successParseInputValue = double.TryParse(inputRawValue, out double inputValue);

            if (!successParseVatRate || !successParseInputValue)
            {
                context.Result = new BadRequestObjectResult("Invalid input data format!");
            }

            if (vatRate <= 0 || inputValue <= 0)
            {
                context.Result = new BadRequestObjectResult("VAT Rate and input data should be greater than zero");
            }
        }
    }
}
