using Aspose.Imaging.AsyncTask;

namespace CorporateWebProject.WebUI.Models.Service
{
    public interface IServiceVM
    {
        public Task LoadLanguageSettingsAsync(HttpContext context, string language = "tr");
        public Task LoadCityAndDistrictDataAsync(HttpContext context);
        public Task LoadModulAndPageDataAsync(HttpContext context);
    }
}
