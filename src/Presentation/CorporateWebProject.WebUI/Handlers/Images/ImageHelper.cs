using Aspose.Imaging;
using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;

namespace CorporateWebProject.WebUI.Handlers.Images
{
    

    public class ImageResult
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public IFormFile CurrentFile { get; set; }
        public bool Statu { get; set; }
        public string StatuMessage { get; set; }
        public string FullPath { get; set; }
    }
    public class ImageResultJSON
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public bool Statu { get; set; }
        public string StatuMessage { get; set; }
    }
}
