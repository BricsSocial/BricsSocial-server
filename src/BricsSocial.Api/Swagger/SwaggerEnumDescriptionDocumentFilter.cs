using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection.Metadata;
using System.Xml.XPath;

namespace BricsSocial.Api.Swagger
{
    // https://stackoverflow.com/questions/36452468/swagger-ui-web-api-documentation-present-enums-as-strings
    //public class SwaggerEnumDescriptionDocumentFilter : IDocumentFilter
    //{
    //    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    //    {
    //        // Describe enums in models
    //        foreach (var schemaItem in context.SchemaRepository.Schemas)
    //        {
    //            var schema = schemaItem.Value;
    //            foreach (var propertyItem in schema.Properties)
    //            {
    //                var property = propertyItem.Value;
    //                var propertyEnums = property.Enum;
    //                if (property.Enum.Any())
    //                {
    //                    property.Description += DescribeEnum(propertyEnums);
    //                }
    //            }
    //        }

    //        // Describe enums in parameters
    //        if (swaggerDoc.Paths.Any())
    //        {
    //            foreach (var pathItem in swaggerDoc.Paths.Values)
    //            {
    //                DescribeEnumParameters(pathItem.Parameters);

    //                // head, patch, options, delete left out
    //                var possibleParameterisedOperations = new List<OperationType> { OperationType.Get, OperationType.Post, OperationType.Put };

    //                foreach (var parameters in pathItem.Operations
    //                    .Where(o => possibleParameterisedOperations.Contains(o.Key))
    //                    .Select(o => o.Value.Parameters))
    //                    DescribeEnumParameters(parameters);
    //            }
    //        }
    //    }


    //    private void DescribeEnumParameters(IList<OpenApiParameter> parameters)
    //    {
    //        foreach (var param in parameters)
    //        {
    //            var paramEnums = param.Schema.Enum;
    //            if (paramEnums.Any())
    //            {
    //                param.Description += DescribeEnum(paramEnums);
    //            }
    //        }
    //    }

    //    private string DescribeEnum(IList<IOpenApiAny> enums)
    //    {
    //        var enumDescriptions = new List<string>();
    //        foreach (object enumOption in enums)
    //        {
    //            enumDescriptions.Add(string.Format("{0} = {1}", (int)enumOption, Enum.GetName(enumOption.GetType(), enumOption)));
    //        }
    //        return string.Join(", ", enumDescriptions.ToArray());
    //    }
    //}


    internal sealed class SwaggerEnumDescriptionDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // add enum descriptions to result models
            foreach (var property in swaggerDoc.Components.Schemas.Where(x => x.Value.Enum.Any()))
            {
                var propertyEnums = property.Value.Enum;
                if (propertyEnums.Any())
                {
                    property.Value.Description += DescribeEnum(propertyEnums, property.Key);
                }
            }

            // add enum descriptions to input parameters
            foreach (var pathItem in swaggerDoc.Paths)
            {
                DescribeEnumParameters(pathItem.Value.Operations, swaggerDoc, context.ApiDescriptions, pathItem.Key);
            }
        }

        private void DescribeEnumParameters(IDictionary<OperationType, OpenApiOperation> operations, OpenApiDocument swaggerDoc, IEnumerable<ApiDescription> apiDescriptions, string path)
        {
            path = path.Trim('/');
            
            var pathDescriptions = apiDescriptions.Where(a => a.RelativePath == path);
            foreach (var operation in operations)
            {
                var operationDescription = pathDescriptions.FirstOrDefault(a => a.HttpMethod.Equals(operation.Key.ToString(), StringComparison.InvariantCultureIgnoreCase));
                foreach (var parameter in operation.Value.Parameters)
                {
                    var parameterDescription = operationDescription.ParameterDescriptions.FirstOrDefault(a => a.Name == parameter.Name);
                    if (parameterDescription != null && parameterDescription.Type != null && TryGetEnumType(parameterDescription.Type, out Type enumType))
                    {
                        var paramEnum = swaggerDoc.Components.Schemas.FirstOrDefault(x => x.Key == enumType.Name);
                        if (paramEnum.Value != null)
                        {
                            parameter.Description += DescribeEnum(paramEnum.Value.Enum, paramEnum.Key);
                        }
                    }
                }
            }
        }

        private bool TryGetEnumType(Type type, out Type enumType)
        {
            if (type.IsEnum)
            {
                enumType = type;
                return true;
            }
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var underlyingType = Nullable.GetUnderlyingType(type);
                if (underlyingType != null && underlyingType.IsEnum == true)
                {
                    enumType = underlyingType;
                    return true;
                }
            }
            else
            {
                var underlyingType = GetTypeIEnumerableType(type);
                if (underlyingType != null && underlyingType.IsEnum)
                {
                    enumType = underlyingType;
                    return true;
                }
                else
                {
                    var interfaces = type.GetInterfaces();
                    foreach (var interfaceType in interfaces)
                    {
                        underlyingType = GetTypeIEnumerableType(interfaceType);
                        if (underlyingType != null && underlyingType.IsEnum)
                        {
                            enumType = underlyingType;
                            return true;
                        }
                    }
                }
            }

            enumType = null;
            return false;
        }

        private Type? GetTypeIEnumerableType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                var underlyingType = type.GetGenericArguments()[0];
                if (underlyingType.IsEnum)
                {
                    return underlyingType;
                }
            }

            return null;
        }

        private Type GetEnumTypeByName(string enumTypeName)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .FirstOrDefault(x => x.Name == enumTypeName);
        }

        private string DescribeEnum(IList<IOpenApiAny> enums, string propertyTypeName)
        {
            List<string> enumDescriptions = new List<string>();
            var enumType = GetEnumTypeByName(propertyTypeName);
            if (enumType == null)
                return null;

            foreach (OpenApiInteger enumOption in enums)
            {
                int enumInt = enumOption.Value;

                enumDescriptions.Add(string.Format("{0} = {1}", enumInt, Enum.GetName(enumType, enumInt)));
            }

            return string.Join(", ", enumDescriptions.ToArray());
        }
    }
}

