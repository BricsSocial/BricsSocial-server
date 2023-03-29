using BricsSocial.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Common.Interfaces
{
    public interface IFileStorage
    {
        public Task<FileUploadResponse> UploadFileAsync(FileUploadInfo fileUploadInfo, CancellationToken cancellationToken);
        public Task DeleteFileAsync(string fileUrl, CancellationToken cancellationToken);

    }
}
