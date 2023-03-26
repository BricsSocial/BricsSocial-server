using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace BricsSocial.Api.Swagger
{
    public static class CustomOperationIdGenerator
    {
        public static string Generate(ApiDescription ad)
        {
            var controllerName = ad.ActionDescriptor.RouteValues["controller"];
            var actionName = ad.ActionDescriptor.RouteValues["action"];

            return $"{controllerName}{actionName}";
        }
    }
}
