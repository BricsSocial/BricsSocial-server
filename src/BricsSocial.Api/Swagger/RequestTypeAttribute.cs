namespace BricsSocial.Api.Swagger
{
    public class RequestTypeAttribute : Attribute
    {
        public Type RequestType { get; init; }

        public RequestTypeAttribute(Type requestType)
        {
            RequestType = requestType;
        }
    }
}
