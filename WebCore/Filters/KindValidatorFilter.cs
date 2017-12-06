using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using WebCore.Models;

namespace WebCore.Filters
{
    public class KindValidatorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Get kind value from route
            var kind = context.HttpContext.GetRouteValue("kind")?.ToString();

            if (kind != null)
            {
                if (!Enum.IsDefined(typeof(DataBucketKind), kind))
                {
                    throw new ArgumentException("Bad kind");
                }
            }
        }
    }
}
