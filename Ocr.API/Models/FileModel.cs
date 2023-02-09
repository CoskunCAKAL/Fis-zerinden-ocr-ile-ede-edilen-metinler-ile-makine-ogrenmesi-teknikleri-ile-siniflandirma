using Microsoft.AspNetCore.Http;

namespace Ocr.API.Models
{
    public class FileModel
    {
        public string FileName { get; set; }
        public IFormFile file { get; set; }
    }
}
