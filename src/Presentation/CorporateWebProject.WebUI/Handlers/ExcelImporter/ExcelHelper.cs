using CorporateWebProject.Domain.Entities;
using OfficeOpenXml;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CorporateWebProject.WebUI.Handlers.ExcelImporter
{
    public class ExcelHelper
    {
        public List<ExcelModel> Import(string filePath)
        {
			try
			{
                var mod = new Dictionary<string, int>();
                var leads = new List<ExcelModel>();
                // Load the Excel file
                var fileInfo = new FileInfo(filePath);
                using (var package = new ExcelPackage(fileInfo))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // Assume data is in the first worksheet

                    for (int row = 1; row <= worksheet.Dimension.End.Column; row++)
                    {
                         mod.TryAdd(worksheet.Cells[1, row].Text.ToString(), row);

                    }

                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        try
                        {

                            var dateString = worksheet.Cells[row, 2].Text;
                            var date = ParseDateTime(dateString);
                            var lead = new ExcelModel
                            {
                                Id = worksheet.Cells[row, 1].Text,
                                CreatedTime = date.ToString(),
                                AdId = worksheet.Cells[row, 3].Text,
                                AdName = worksheet.Cells[row, 4].Text,
                                AdsetId = worksheet.Cells[row, 5].Text,
                                AdsetName = worksheet.Cells[row, 6].Text,
                                CampaignId = worksheet.Cells[row, 7].Text,
                                CampaignName = worksheet.Cells[row, 8].Text,
                                FormId = worksheet.Cells[row, 9].Text,
                                FormName = worksheet.Cells[row, 10].Text,
                                IsOrganic = bool.Parse(worksheet.Cells[row, 11].Text),
                                Platform = worksheet.Cells[row, 12].Text,
                                DanismanlikAlmakIsteginizKonuNedir = worksheet.Cells[row, 13].Text,
                                Email = worksheet.Cells[row, mod["email"]].Text,
                                FullName = worksheet.Cells[row, mod["full_name"]].Text,
                                PhoneNumber = ConvertToInternationalFormat(worksheet.Cells[row, mod["phone_number"]].Text),
                                JobTitle = worksheet.Cells[row, mod["job_title"]].Text,
                                LeadStatus = worksheet.Cells[row, 18].Text
                            };
                            leads.Add(lead);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

                return leads;
            }
			catch (Exception ex)
			{
                return null;
			}
        }

        public static string ConvertToInternationalFormat(string phoneNumber)
        {
            // 1. Tüm gereksiz karakterleri kaldır
            string cleanedNumber = Regex.Replace(phoneNumber, @"[^\d]", "");

            // 2. Numaranın uzunluğuna göre formatı uygula
            if (cleanedNumber.Length == 10)
            {
                // 10 haneli numara, başına +90 ekle
                return "+90" + cleanedNumber;
            }
            else if (cleanedNumber.Length == 11 && cleanedNumber.StartsWith("0"))
            {
                // 11 haneli ve 0 ile başlıyorsa, 0'ı kaldır ve başına +90 ekle
                return "+90" + cleanedNumber.Substring(1);
            }
            else if (cleanedNumber.Length == 12 && cleanedNumber.StartsWith("90"))
            {
                // 12 haneli ve 90 ile başlıyorsa, başına + ekle
                return "+" + cleanedNumber;
            }
            else if (cleanedNumber.Length == 12 && cleanedNumber.StartsWith("0"))
            {
                // 12 haneli ve 0 ile başlıyorsa, 0'ı kaldır ve başına +90 ekle
                return "+90" + cleanedNumber.Substring(1);
            }
            else if (cleanedNumber.Length == 12 && !cleanedNumber.StartsWith("90"))
            {
                // 12 haneli ve 90 ile başlamıyorsa, başına +90 ekle
                return "+90" + cleanedNumber.Substring(2);
            }
            else if (cleanedNumber.Length > 12 && cleanedNumber.StartsWith("90"))
            {
                // 13 veya daha fazla haneli ve 90 ile başlıyorsa, başına + ekle
                return "+" + cleanedNumber;
            }
            else if (cleanedNumber.Length > 12 && cleanedNumber.StartsWith("0"))
            {
                // 13 veya daha fazla haneli ve 0 ile başlıyorsa, 0'ı kaldır ve başına +90 ekle
                return "+90" + cleanedNumber.Substring(1);
            }
            else
            {
                return phoneNumber;
                // Geçersiz format
                //throw new FormatException("Geçersiz telefon numarası formatı.");
            }
        }

        private DateTime ParseDateTime(string dateTimeStr)
        {
            DateTime dateTime;
            string[] formats = { "MM.dd.yy HH:mm", "M.dd.yy HH:mm", "M.dd.yy H:mm", "MM/dd/yyyy HH:mm", "M/dd/yyyy HH:mm", "M/dd/yyyy H:mm", "dd.MM.yyyy HH:mm", "yyyy-MM-dd HH:mm", "MM-dd-yyyy HH:mm" };

            if (DateTime.TryParseExact(dateTimeStr.Replace("/","."), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                return dateTime;
            }
            else
            {
                throw new FormatException($"String '{dateTimeStr}' was not recognized as a valid DateTime.");
            }
        }
    }
}
