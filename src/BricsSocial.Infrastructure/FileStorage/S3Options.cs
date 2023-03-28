using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Infrastructure.FileStorage
{
    public sealed class S3Options
    {
        public string ServiceUrl { get; set; }
        public string Region { get; set; }
        public string BucketName { get; set; }
        
        public string KeyId { get; set; }
        public string KeySecret { get; set; }
    }
}
