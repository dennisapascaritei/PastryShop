using Microsoft.AspNetCore.Mvc.Filters;

namespace PastryShop.Api.Filters
{
    public class ValidateGuidAttribute : ActionFilterAttribute
    {
        private readonly List<string> _keys;
        public ValidateGuidAttribute(params string[] keys)
        {
            _keys = keys.ToList();
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool isError = false;
            var apiError = new ErrorResponse();

            _keys.ForEach(key =>
            {
                if (!context.ActionArguments.TryGetValue(key, out var value)) return;

                if(!Guid.TryParse(value?.ToString(), out var guid)) isError = true;

                if (isError)
                {
                    apiError.Errors.Add($"The identifier for {key} is not correct GUID format");
                    apiError.StatusCode = 400;
                    apiError.StatusPhrase = "Bad Request";
                    apiError.TimeStamp = DateTime.Now;
                    context.Result = new ObjectResult(apiError);
                }

            });
            
        }
    }
}
