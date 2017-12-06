using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebCore.Models;

namespace WebCore.ModelBinders
{
    public class StringArrayModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(StringArrayModel))
                return new StringArrayModelBinder();

            return null;
        }
    }
}
