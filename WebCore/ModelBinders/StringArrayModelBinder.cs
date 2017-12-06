using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebCore.Models;

namespace WebCore.ModelBinders
{
    public class StringArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string day = bindingContext.ActionContext.HttpContext.Request.Query["d"];
            string month = bindingContext.ActionContext.HttpContext.Request.Query["m"];
            string year = bindingContext.ActionContext.HttpContext.Request.Query["y"];

            //if (!(bindingContext.ActionContext.HttpContext.Request.Query["day"], out day) ||
            //    !int.TryParse(bindingContext.ActionContext.HttpContext.Request.Query["month"], out month) ||
            //    !int.TryParse(bindingContext.ActionContext.HttpContext.Request.Query["year"], out year))
            //{
            //    return Task.CompletedTask;
            //}

            var result = new StringArrayModel
            {
                ValuesList = new[] {day?? string.Empty, month ?? string.Empty, year ?? string.Empty}
            };

            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}
