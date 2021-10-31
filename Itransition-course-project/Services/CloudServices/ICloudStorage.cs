using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Itransition_course_project.Services.CloudServices
{
    public interface ICloudStorage
    {
        Task<string> UploadFileAsync(IFormFile imageFile, string fileNameForStorage);
        Task DeleteFileAsync(string fileNameForStorage);
    }
}