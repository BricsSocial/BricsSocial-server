using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BricsSocial.Application.Common.Models
{
    public sealed class FileUploadInfo
    {
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public Stream InputStream { get; set; }


        public string GenerateFileKey()
        {
            string uniqueFileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(FileName);

            return $"{FolderName}/{uniqueFileName}{extension}";
        }
    }
}
