using System.Net.Mail;
using System.Net;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.Application.Repositories;

namespace CorporateWebProject.WebUI.Handlers.Mail
{
    public class MailHelper
    {

        public async void SendMail(string title,string description,string sendMailAddress, HttpContext context,ContactMessages messageContent,Exchanges exc)
        {
            try
            {
                //sendMailAddress = "erdalkuzu@kaktusyazilim.com";
                var services = context.RequestServices;
                var smtpRepository = (ISmtpSettingRepository)services.GetService(typeof(ISmtpSettingRepository));
                //SmtpSettings smtpSettings = (await smtpRepository.GetListAsync()).Data.Last();

                SmtpClient client = new SmtpClient("mail.smtp2go.com", Convert.ToInt32("2525"));
                client.Timeout = 5;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential("info@vermogenscheck24.de", "147896325q");
                MailAddress from = new MailAddress("info@vermogenscheck24.de", "Yeni Üye Kaydı - Vermogenscheck24");
                MailAddress to = new MailAddress(sendMailAddress);
                MailMessage message = new MailMessage(from, to);

                // Değişkenler
                string logoUrl = "https://www.vermogenscheck24.de/themes/assets/image/logo2.svg"; // Logo yolu belirtiniz

                string emailTemplateImages = string.Join("", new string[] { "facebook.png", "youtube.png", "twitter.png", "gplus.png", "linkedin.png", "pinterest.png" });


                string mail = $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Contact Form</title>
    <style>
        .email-container {{
            max-width: 600px;
            margin: 0 auto;
            font-family: Arial, sans-serif;
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 8px;
        }}
        .logo {{
            text-align: center;
            margin-bottom: 30px;
        }}
        .logo img {{
            max-width: 200px;
        }}
        .user-info {{
            margin-bottom: 25px;
        }}
        .info-row {{
            margin-bottom: 15px;
        }}
        .label {{
            font-weight: bold;
            color: #333;
        }}
        .message-box {{
            background-color: #f9f9f9;
            padding: 15px;
            border-radius: 5px;
            margin-bottom: 20px;
        }}
        .reference {{
            border-top: 1px solid #ddd;
            padding-top: 15px;
            display: flex;
            align-items: center;
        }}
        .bank-icon {{
            width: 150px;
            margin-right: 10px;
        }}
        .bank-info {{
            display: flex;
            align-items: center;
            gap: 10px;
        }}
    </style>
</head>
<body>
    <div class=""email-container"">
        <div class=""logo"">
            <img src=""{logoUrl}"" alt=""Company Logo"">
        </div>
        
        <div class=""user-info"">
            <div class=""info-row"">
                <span class=""label"">İsim Soyisim:</span>
                <span>{messageContent.Name} {messageContent.Surname}</span>
            </div>
            <div class=""info-row"">
                <span class=""label"">Email:</span>
                <span>{messageContent.Mail}</span>
            </div>
            <div class=""info-row"">
                <span class=""label"">Telefon:</span>
                <span>{messageContent.Phone}</span>
            </div>
            <div class=""info-row"">
                <span class=""label"">Bütçe:</span>
                <span>{messageContent.Count}</span>
            </div>
        </div>

        <div class=""message-box"">
            <div class=""info-row"">
                <span class=""label"">Başvuru Nedeni:</span>
                <p>{messageContent.Reason}</p>
            </div>
            <div class=""info-row"">
                <span class=""label"">Mesaj:</span>
                <p>{messageContent.Message}</p>
            </div>
        </div>

        <div class=""reference"">
            <div class=""bank-info"">
                <img src=""https://vermogenscheck24.de{exc.Icon}"" width=""200"" alt=""Bank Icon"" class=""bank-icon"">
                <span class=""label"">Referans Banka:</span>
                <span>{exc.Name}</span>
                <span class=""label"">Oran:</span>
                <span>{exc.Count}%</span>
            </div>
        </div>
    </div>
</body>
</html>";



//                string mail = $@"
//<!DOCTYPE html>
//<html>
//<head>
//    <title>{title}</title>
//    <!-- Diğer meta etiketleri, stil tanımları vs. -->
//</head>
//<body style=""margin: 0 auto; padding:0px;"" bgcolor=""#f2f2f2"" id=""body"">
//    <!-- HTML içeriği -->
//    <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" align=""center"" role=""presentation"">
//        <!-- Tablonun içeriği -->
//        <tr>
//            <td align=""center"" valign=""middle"" style=""padding-top:100px;padding-bottom:100px; "" bgcolor=""#f2f2f2"">
//                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""640"" align=""center"" class=""table-full"" role=""presentation"" style=""min-width:320px; max-width:640px;"">
//                    <tbody id=""base"">
//                        <!-- Tablo içeriği -->
//                        <tr>
//                            <td align=""center"" valign=""middle"">
//                                <table width=""640"" border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" class=""table"" style=""max-width:640px;"" role=""presentation"">
//                                    <tbody>
//                                        <!-- İç içe tablo içeriği -->
//                                        <tr>
//                                            <td align=""center"" valign=""middle"" bgcolor=""#ffffff"" style=""background-color:#ffffff;"">
//                                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" align=""center"" style=""padding:85px; min-width:320px;"" role=""presentation"">
//                                                    <tbody>
//                                                        <!-- Başlık ve logo içeren tablo içeriği -->
//                                                        <tr>
//                                                            <td align=""center"" valign=""middle"" style=""padding:10px 0px 10px 0px;"">
//                                                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" role=""presentation"">
//                                                                    <tbody>
//                                                                        <tr>
//                                                                            <td align=""center"" valign=""middle"" height=""70"" style=""vertical-align: middle;"">
//                                                                                <a href=""#"" target=""_blank"" style=""text-decoration: none;"">
//                                                                                    <img src=""{logoUrl}"" alt="""" title="""" height=""50"" border=""0"" style=""min-height:160px; display:block; font-family:'pp-sans-big-medium', Tahoma, Arial, sans-serif; font-size:32px; color:#003288;"">
//                                                                                </a>
//                                                                            </td>
//                                                                        </tr>
//                                                                    </tbody>
//                                                                </table>
//                                                            </td>
//                                                        </tr>
//                                                        <!-- Diğer içerikler -->
//                                                        <tr>
//                                                            <td align=""center"" valign=""middle"" style=""padding:0px 0px 0px 0px; vertical-align: middle; line-height: 1px;"">
//                                                                <!-- Ayrı bir tablo içeriği -->
//                                                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" role=""presentation"">
//                                                                    <tbody>
//                                                                        <tr>
//                                                                            <td align=""center"" valign=""middle"" height=""1"" style=""vertical-align: middle; line-height: 1px;"">
//                                                                                <img src=""https://images.ctfassets.net/7rifqg28wcbd/1tFsF7cjjNpwaLC3AKwtu7/3709b2fab644d1c377323faf87f300f9/headergrad_onwhite.jpg"" alt="""" height=""1"" border=""0"" style=""display:block;"" class=""header-border"">
//                                                                            </td>
//                                                                        </tr>
//                                                                    </tbody>
//                                                                </table>
//                                                            </td>
//                                                        </tr>
//                                                        <!-- Diğer içerikler devam eder -->
//                                                        <tr>
//                                                            <td align=""center"" valign=""middle"" style=""padding:0px 0px 0px 0px; background-color:#ffffff;"" bgcolor=""#ffffff"">
//                                                                <table width=""520"" border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" class=""table"" style=""max-width: 520px;"" role=""presentation"">
//                                                                    <tbody>
//                                                                        <tr>
//                                                                            <td align=""center"" valign=""middle"" style=""padding: 20px 0px 0px 0px;"" class=""pad2035nobot"">
//                                                                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" align=""center"" role=""presentation"">
//                                                                                    <tbody>
//                                                                                        <tr style='margin-top:40px;margin-bottom:40px;'>
//                                                                                            <td align=""center"" valign=""middle"" style=""padding:0px 0px 0px 0px;"">
                                                                                               

//<p style=""margin-top:30px;margin-bottom:30px; margin: 0px; font-family:Math Sans, Tahoma, Arial, sans-serif; font-size:16px; mso-line-height-rule:exactly; line-height:1.5; color:#6c7378;"">{description}</p>
//<hr/>

//<p style=""margin: 0px; font-family:Math Sans, Tahoma, Arial, sans-serif; font-size:16px; mso-line-height-rule:exactly; line-height:1.5; color:#6c7378;"">{address}</p>

//<p style=""margin: 0px; font-family:Math Sans, Tahoma, Arial, sans-serif; font-size:16px; mso-line-height-rule:exactly; line-height:1.5; color:#6c7378;"">{kvkkText}</p>
//                                                                                            </td>
//                                                                                        </tr>
//                                                                                        <!-- Ödeme butonu -->
//                                                                                        <tr>
//                                                                                            <td align=""center"" valign=""middle"" style=""padding:0px 0px 0px 0px; vertical-align: middle;"">
//                                                                                                <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" class=""neptuneButtonwhite"">
//                                                                                                    <tbody>
//                                                                                                        <tr>
//                                                                                                            <td align=""center"" style=""padding:40px 30px 30px 30px;"">
//                                                                                                                <table border=""0"" cellspacing=""0"" cellpadding=""0"" class=""stackTbl"" style=""color:#ffffff !important;"">
                                                                                                                    
//                                                                                                                </table>
//                                                                                                            </td>
//                                                                                                        </tr>
//                                                                                                    </tbody>
//                                                                                                </table>
//                                                                                            </td>
//                                                                                        </tr>
//                                                                                        <!-- İmza -->
//                                                                                        <tr>
//                                                                                            <td align=""center"" valign=""middle"" style=""padding:0px 0px 0px 0px;"">
//                                                                                                <p style=""margin: 20px 0px 0px 0px; font-family:Math Sans, Tahoma, Arial, sans-serif; font-size:14px; mso-line-height-rule:exactly; line-height:1.5; color:#6c7378;"">Saygılarımla,<br>
//                                                                                                {companyName}</p>
//                                                                                            </td>
//                                                                                        </tr>
//                                                                                    </tbody>
//                                                                                </table>
//                                                                            </td>
//                                                                        </tr>
//                                                                    </tbody>
//                                                                </table>
//                                                            </td>
//                                                        </tr>
//                                                        <!-- Diğer içerikler devam eder -->
//                                                    </tbody>
//                                                </table>
//                                            </td>
//                                        </tr>
//                                        <!-- Diğer içerikler devam eder -->
//                                    </tbody>
//                                </table>
//                            </td>
//                        </tr>
//                        <!-- Diğer içerikler devam eder -->
//                    </tbody>
//                </table>
//            </td>
//        </tr>
//        <!-- Diğer içerikler devam eder -->
//    </table>
//</body>
//</html>
//";
                message.Body = mail;
                message.IsBodyHtml = true;
                message.Subject = title;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {

            }
        }
   
    
    
    }
}
