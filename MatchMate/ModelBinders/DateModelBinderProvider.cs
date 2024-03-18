using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MatchMate.ModelBinders
{
    public class DateModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(DateTime) ||
                context.Metadata.ModelType == typeof(DateTime?))
            {
                return new DateModelBinder();
            }

            return null;
        }
    }
}
