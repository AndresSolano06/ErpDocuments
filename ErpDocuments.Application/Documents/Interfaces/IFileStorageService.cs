using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ErpDocuments.Application.Documents.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> UploadAsync(
            Stream stream,
            string fileName,
            string folder,
            CancellationToken cancellationToken = default
        );

        Task<bool> DeleteAsync(
            string key,
            CancellationToken cancellationToken = default
        );
    }
}
