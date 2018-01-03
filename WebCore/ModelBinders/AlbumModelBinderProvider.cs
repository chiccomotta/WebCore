using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebCore.ModelBinders
{
    public class AlbumModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if(context == null) throw new ArgumentNullException(nameof(context));

            if (context.Metadata.ModelType == typeof(AlbumModelBinder))
                return new AlbumModelBinder();

            return null;
        }
    }
}
