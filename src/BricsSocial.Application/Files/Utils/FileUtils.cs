using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Files.Utils
{
    public static class FileUtils
    {
        private static string[] _validContentTypes = new string[] { "image/jpeg", "image/png" };

        public static bool BeNotEmptyFile(IFormFile formFile)
            => formFile.Length != 0;

        public static bool HaveValidContentType(IFormFile formFile)
            => _validContentTypes.Contains(formFile.ContentType);

        public static string InvalidContentTypeMessage(string contentType)
            => $"Invalid file content type ({contentType}). Only {string.Join(", ", _validContentTypes)} content types are allowed";
        public static string CannotByEmptyFileMessage()
            => "File cannot be empty"; 

    }
}
