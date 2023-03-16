using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection.Metadata;
using System.Xml.XPath;

namespace BricsSocial.Api.Swagger
{
    // https://stackoverflow.com/questions/36452468/swagger-ui-web-api-documentation-present-enums-as-strings
    public class SwaggerEnumDescriptionDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            // add enum descriptions to result models
            foreach (KeyValuePair<string, Schema> schemaDictionaryItem in swaggerDoc.definitions)
            {
                Schema schema = schemaDictionaryItem.Value;
                foreach (KeyValuePair<string, Schema> propertyDictionaryItem in schema.properties)
                {
                    Schema property = propertyDictionaryItem.Value;
                    IList<object> propertyEnums = property.@enum;
                    if (propertyEnums != null && propertyEnums.Count > 0)
                    {
                        property.description += DescribeEnum(propertyEnums);
                    }
                }
            }

            // add enum descriptions to input parameters
            if (swaggerDoc.paths.Count > 0)
            {
                foreach (PathItem pathItem in swaggerDoc.paths.Values)
                {
                    DescribeEnumParameters(pathItem.parameters);

                    // head, patch, options, delete left out
                    List<Operation> possibleParameterisedOperations = new List<Operation> { pathItem.get, pathItem.post, pathItem.put };
                    possibleParameterisedOperations.FindAll(x => x != null).ForEach(x => DescribeEnumParameters(x.parameters));
                }
            }
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var schemaItem in context.SchemaRepository.Schemas)
            {
                var schema = schemaItem.Value;
                foreach(var propertyItem in schema.Properties)
                {
                    var property = propertyItem.Value;
                    var propertyEnums = property.Enum;
                    if (property.Enum.Any())
                    {
                        property.Description += DescribeEnum(propertyEnums);
                    }
                }
            }

            if (swaggerDoc.Paths.Any())
            {
                foreach (var pathItem in swaggerDoc.Paths.Values)
                {
                    DescribeEnumParameters(pathItem.Parameters);

                    // head, patch, options, delete left out
                    var possibleParameterisedOperations = new List<OperationType> { OperationType.Get, OperationType.Post, OperationType.Put };

                    foreach (var parameters in pathItem.Operations
                        .Where(o => possibleParameterisedOperations.Contains(o.Key))
                        .Select(o => o.Value.Parameters))
                        DescribeEnumParameters(parameters);
                }
            }
        }


        private void DescribeEnumParameters(IList<OpenApiParameter> parameters)
        {
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    IList<object> paramEnums = param.@enum;
                    if (paramEnums != null && paramEnums.Count > 0)
                    {
                        param.description += DescribeEnum(paramEnums);
                    }
                }
            }
        }

        private string DescribeEnum(IList<IOpenApiAny> enums)
        {
            var enumDescriptions = new List<string>();
            foreach (object enumOption in enums)
            {
                enumDescriptions.Add(string.Format("{0} = {1}", (int)enumOption, Enum.GetName(enumOption.GetType(), enumOption)));
            }
            return string.Join(", ", enumDescriptions.ToArray());
        }
    }
}

