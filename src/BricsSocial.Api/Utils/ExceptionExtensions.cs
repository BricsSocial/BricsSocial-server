using System.Text;

namespace BricsSocial.Api.Utils
{
    public static class ExceptionExtensions
    {
        public static string GetMessage(this Exception ex)
        {
            var messageBuilder = new StringBuilder();

            var currentEx = ex;
            do
            {
                messageBuilder.Append(currentEx.Message);

                if (currentEx.InnerException is null)
                    messageBuilder.AppendDot();
                else
                    messageBuilder.AppendDotWithSpace();

                currentEx = currentEx.InnerException;
            }
            while (currentEx != null);

            return messageBuilder.ToString();
        }
    }

}
