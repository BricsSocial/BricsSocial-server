using MediatR;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace BricsSocial.Api.Swagger
{
    internal static class OperationFilterContextExtensions
    {
        public static IEnumerable<T> GetActionMediatRRequestAttributes<T>(this OperationFilterContext context) where T : Attribute
        {
            if (context.MethodInfo != null)
            {
                // Try to get from action parameter
                var actionRequestType = context.MethodInfo
                    .GetParameters()
                    .Where(p => typeof(IBaseRequest).IsAssignableFrom(p.ParameterType))
                    .Select(p => p.ParameterType)
                    .FirstOrDefault();

                // Try to get from RequestType action attribute
                if (actionRequestType == null)
                {
                    var requestTypeAttribute = context.MethodInfo.GetCustomAttribute<RequestTypeAttribute>();

                    if (requestTypeAttribute != null)
                        actionRequestType = requestTypeAttribute.RequestType;
                }

                if(actionRequestType != null)
                {
                    var requestAttributes = actionRequestType.GetCustomAttributes<T>();

                    var result = new List<T>(requestAttributes);

                    return result;
                }
            }

            return Enumerable.Empty<T>();
        }
    }
}
