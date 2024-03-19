using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using static MatchMateInfrastructure.DataConstants;

namespace MatchMate.ModelBinders
{
    public class DateModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            string value = valueResult.FirstValue;

            if (valueResult != ValueProviderResult.None && !string.IsNullOrEmpty(value))
            {
                DateTime dateResult = DateTime.Now;
                bool success = false;
                try
                {
                    dateResult = DateTime.ParseExact(value, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
                    success = true;
                }
                catch (FormatException fe)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe, bindingContext.ModelMetadata);
                }
                if (success)
                {
                    bindingContext.Result = ModelBindingResult.Success(dateResult);

                }
            }
            return Task.CompletedTask;
        }
    }
}
