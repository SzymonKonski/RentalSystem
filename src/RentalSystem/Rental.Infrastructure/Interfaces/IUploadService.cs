using System.IO;
using System.Threading.Tasks;

namespace Rental.Infrastructure.Interfaces
{
    public interface IUploadService
    {
        Task<string> UploadPdfAsync(Stream fileStream, string contentType);
        Task<string> UploadImageAsync(Stream fileStream, string contentType);
    }
}
