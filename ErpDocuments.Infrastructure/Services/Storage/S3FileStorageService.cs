using Amazon.S3;
using Amazon.S3.Model;
using ErpDocuments.Application.Documents.Interfaces;

namespace ErpDocuments.Infrastructure.Services.Storage
{
    public class S3FileStorageService : IFileStorageService
    {
        private readonly IAmazonS3 _s3;
        private readonly string _bucketName;

        public S3FileStorageService(IAmazonS3 s3)
        {
            _s3 = s3;
            _bucketName = "erpdocuments-dev-bucket"; // tu bucket
        }

        public async Task<string> UploadAsync(
            Stream stream,
            string fileName,
            string folder,
            CancellationToken cancellationToken = default)
        {
            var key = $"{folder}/{fileName}";

            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = key,
                InputStream = stream,
                AutoCloseStream = true
            };

            await _s3.PutObjectAsync(request, cancellationToken);

            return key;
        }

        public async Task<bool> DeleteAsync(
            string key,
            CancellationToken cancellationToken = default)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = key
            };

            var response = await _s3.DeleteObjectAsync(request, cancellationToken);
            return response.HttpStatusCode == System.Net.HttpStatusCode.NoContent;
        }
    }
}
