namespace BricsSocial.Api.Swagger
{
    public static class CustomSchemaIdGenerator
    {
        public static string Generate(Type type)
        {
            var typeName = type.Name;
            if (type.IsGenericType)
            {
                var genericArgs = string.Join(", ", type.GetGenericArguments().Select(Generate));

                int index = typeName.IndexOf('`');
                var typeNameWithoutGenericArity = index == -1 ? typeName : typeName.Substring(0, index);

                return $"{typeNameWithoutGenericArity}<{genericArgs}>";
            }
            return typeName;
        }
    }
}
