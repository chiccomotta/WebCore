using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebCore.Models;
using WebCore.Services;

namespace WebCore.ModelBinders
{
    public class AlbumModelBinder : IModelBinder
    {      
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var service = bindingContext.HttpContext.RequestServices.GetRequiredService<MusicAlbumRepository>();

            var id = bindingContext.ActionContext.RouteData.Values["id"]?.ToString();

            if (int.TryParse(id, out int albumId))
            {
                var album = service.AlbumList[albumId];

                bindingContext.Result = ModelBindingResult.Success(album);                
            }

            return Task.CompletedTask;
 
        }
    }
}
