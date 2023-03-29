using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Infrastructure.FileStorage
{
    public sealed class S3FileStorage : IFileStorage
    {

        private readonly S3Options _s3Options;

        private readonly BasicAWSCredentials _credentials;
        private readonly AmazonS3Config _config;

        public S3FileStorage(S3Options s3Options)
        {
            _s3Options = s3Options;

            _credentials = new BasicAWSCredentials(_s3Options.KeyId, _s3Options.KeySecret);
            _config = new AmazonS3Config
            {
                ServiceURL = _s3Options.ServiceUrl,
            };
        }

        public async Task DeleteFileAsync(string fileUrl, CancellationToken cancellationToken)
        {
            var s3URI = new AmazonS3Uri(fileUrl);

            var request = new DeleteObjectRequest
            {
                BucketName = s3URI.Bucket,
                Key = s3URI.Key
            };

            using var client = new AmazonS3Client(_credentials, _config);

            var response = await client.DeleteObjectAsync(request, cancellationToken);

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK
                && response.HttpStatusCode != System.Net.HttpStatusCode.NoContent
                && response.HttpStatusCode != System.Net.HttpStatusCode.NotFound)
                throw new Exception($"Cannot delete object from bucket. StatusCode = {(int)response.HttpStatusCode} ({response.HttpStatusCode})");
        }

        public async Task<FileUploadResponse> UploadFileAsync(FileUploadInfo fileUploadInfo, CancellationToken cancellationToken)
        {
            var fileKey = fileUploadInfo.GenerateFileKey();
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = fileUploadInfo.InputStream,
                Key = fileKey,
                BucketName = _s3Options.BucketName,
            };

            using var client = new AmazonS3Client(_credentials, _config);

            var transferUtility = new TransferUtility(client);

            await transferUtility.UploadAsync(uploadRequest, cancellationToken);

            return new FileUploadResponse
            {
                FileUrl = $"{_s3Options.ServiceUrl}/{_s3Options.BucketName}/{fileKey}"
            };
        }
    }
}
