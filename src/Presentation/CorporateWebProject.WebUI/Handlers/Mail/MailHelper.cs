using System.Net.Mail;
using System.Net;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.Application.Repositories;

namespace CorporateWebProject.WebUI.Handlers.Mail
{
    public class MailHelper
    {

        public async void SendMail(string title,string description,string sendMailAddress, HttpContext context,bool isUser=false)
        {

            try
            {
                //sendMailAddress = "erdalkuzu@kaktusyazilim.com";
                var services = context.RequestServices;
                var smtpRepository = (ISmtpSettingRepository)services.GetService(typeof(ISmtpSettingRepository));
                SmtpSettings smtpSettings = (await smtpRepository.GetListAsync()).Data.Last();


                SmtpClient client = new SmtpClient(smtpSettings.Server, Convert.ToInt32(smtpSettings.Port));
                client.Timeout = 5;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(smtpSettings.Mail, smtpSettings.MailPassword);
                MailAddress from = new MailAddress(smtpSettings.Mail, smtpSettings.Title);
                MailAddress to = new MailAddress(sendMailAddress);
                MailMessage message = new MailMessage(from, to);

                // Değişkenler
                string logoUrl = "https://www.uplifeacademy.com/uploads/63846Uplife_website_logo.png"; // Logo yolu belirtiniz
                string emailTemplatePath = "https://begos.kaktusyazilim.com/manager/assets/images/email-template/";
                string emailTemplateImages = string.Join("", new string[] { "facebook.png", "youtube.png", "twitter.png", "gplus.png", "linkedin.png", "pinterest.png" });
                string email = "info@uplifeacademy.com";
                string phoneNumber = "+905547969170";
                string address = "Barbaros, Begonya Sk. No:3, 34746 Ataşehir/İstanbul";
                string receiverName = "Erdal Kuzu";
                string paymentLink = "https://pro.uplifeacademy.com/";
                string companyName = "UpLife Academy";
                string kvkkText = "<a target='_blank' href='https://www.uplifeacademy.com/gizlilik-politikasi'>Kişisel Verileri Koruma Kanunu</a>";
                string isUserText=isUser==true? "<tr>\r\n                                                                                                                        <td bgcolor=\"#0070BA\" align=\"center\" valign=\"middle\" style=\"padding:13px 90px 13px 90px; display: block; color:#ffffff !important; text-decoration:none; white-space: nowrap;  -webkit-print-color-adjust: exact; display: block; font-family:Math Sans, Trebuchet, Arial, sans serif; font-size:17px; line-height:21px; color:#ffffff !important;border-radius:25px;\" class=\"ppsans mobilePadding9\">\r\n                                                                                                                            <a href=\"{paymentLink}\" isLinkToBeTracked=\"True\" style=\"text-decoration:none;color:#ffffff !important; white-space: nowrap;\" target=\"_blank\">\r\n                                                                                                                               Kullanıcı Panelim\r\n                                                                                                                            </a>\r\n                                                                                                                        </td>\r\n                                                                                                                    </tr>":"";
                string mail = $@"
<!DOCTYPE html>
<html>
<head>
    <title>{title}</title>
    <!-- Diğer meta etiketleri, stil tanımları vs. -->
</head>
<body style=""margin: 0 auto; padding:0px;"" bgcolor=""#f2f2f2"" id=""body"">
    <!-- HTML içeriği -->
    <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" align=""center"" role=""presentation"">
        <!-- Tablonun içeriği -->
        <tr>
            <td align=""center"" valign=""middle"" style=""padding-top:100px;padding-bottom:100px; "" bgcolor=""#f2f2f2"">
                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""640"" align=""center"" class=""table-full"" role=""presentation"" style=""min-width:320px; max-width:640px;"">
                    <tbody id=""base"">
                        <!-- Tablo içeriği -->
                        <tr>
                            <td align=""center"" valign=""middle"">
                                <table width=""640"" border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" class=""table"" style=""max-width:640px;"" role=""presentation"">
                                    <tbody>
                                        <!-- İç içe tablo içeriği -->
                                        <tr>
                                            <td align=""center"" valign=""middle"" bgcolor=""#ffffff"" style=""background-color:#ffffff;"">
                                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" align=""center"" style=""padding:85px; min-width:320px;"" role=""presentation"">
                                                    <tbody>
                                                        <!-- Başlık ve logo içeren tablo içeriği -->
                                                        <tr>
                                                            <td align=""center"" valign=""middle"" style=""padding:10px 0px 10px 0px;"">
                                                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" role=""presentation"">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align=""center"" valign=""middle"" height=""70"" style=""vertical-align: middle;"">
                                                                                <a href=""#"" target=""_blank"" style=""text-decoration: none;"">
                                                                                    <img src=""{logoUrl}"" alt="""" title="""" height=""50"" border=""0"" style=""min-height:160px; display:block; font-family:'pp-sans-big-medium', Tahoma, Arial, sans-serif; font-size:32px; color:#003288;"">
                                                                                </a>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <!-- Diğer içerikler -->
                                                        <tr>
                                                            <td align=""center"" valign=""middle"" style=""padding:0px 0px 0px 0px; vertical-align: middle; line-height: 1px;"">
                                                                <!-- Ayrı bir tablo içeriği -->
                                                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" role=""presentation"">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align=""center"" valign=""middle"" height=""1"" style=""vertical-align: middle; line-height: 1px;"">
                                                                                <img src=""https://images.ctfassets.net/7rifqg28wcbd/1tFsF7cjjNpwaLC3AKwtu7/3709b2fab644d1c377323faf87f300f9/headergrad_onwhite.jpg"" alt="""" height=""1"" border=""0"" style=""display:block;"" class=""header-border"">
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <!-- Diğer içerikler devam eder -->
                                                        <tr>
                                                            <td align=""center"" valign=""middle"" style=""padding:0px 0px 0px 0px; background-color:#ffffff;"" bgcolor=""#ffffff"">
                                                                <table width=""520"" border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"" class=""table"" style=""max-width: 520px;"" role=""presentation"">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align=""center"" valign=""middle"" style=""padding: 20px 0px 0px 0px;"" class=""pad2035nobot"">
                                                                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" align=""center"" role=""presentation"">
                                                                                    <tbody>
                                                                                        <tr style='margin-top:40px;margin-bottom:40px;'>
                                                                                            <td align=""center"" valign=""middle"" style=""padding:0px 0px 0px 0px;"">
                                                                                               

<p style=""margin-top:30px;margin-bottom:30px; margin: 0px; font-family:Math Sans, Tahoma, Arial, sans-serif; font-size:16px; mso-line-height-rule:exactly; line-height:1.5; color:#6c7378;"">{description}</p>
<hr/>

<p style=""margin: 0px; font-family:Math Sans, Tahoma, Arial, sans-serif; font-size:16px; mso-line-height-rule:exactly; line-height:1.5; color:#6c7378;"">{address}</p>

<p style=""margin: 0px; font-family:Math Sans, Tahoma, Arial, sans-serif; font-size:16px; mso-line-height-rule:exactly; line-height:1.5; color:#6c7378;"">Telefon: {phoneNumber}</p>

<p style=""margin: 0px; font-family:Math Sans, Tahoma, Arial, sans-serif; font-size:16px; mso-line-height-rule:exactly; line-height:1.5; color:#6c7378;"">{kvkkText}</p>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <!-- Ödeme butonu -->
                                                                                        <tr>
                                                                                            <td align=""center"" valign=""middle"" style=""padding:0px 0px 0px 0px; vertical-align: middle;"">
                                                                                                <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" class=""neptuneButtonwhite"">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td align=""center"" style=""padding:40px 30px 30px 30px;"">
                                                                                                                <table border=""0"" cellspacing=""0"" cellpadding=""0"" class=""stackTbl"" style=""color:#ffffff !important;"">
                                                                                                                    {isUserText}
                                                                                                                </table>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <!-- İmza -->
                                                                                        <tr>
                                                                                            <td align=""center"" valign=""middle"" style=""padding:0px 0px 0px 0px;"">
                                                                                                <p style=""margin: 20px 0px 0px 0px; font-family:Math Sans, Tahoma, Arial, sans-serif; font-size:14px; mso-line-height-rule:exactly; line-height:1.5; color:#6c7378;"">Saygılarımla,<br>
                                                                                                {companyName}</p>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <!-- Diğer içerikler devam eder -->
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <!-- Diğer içerikler devam eder -->
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <!-- Diğer içerikler devam eder -->
                    </tbody>
                </table>
            </td>
        </tr>
        <!-- Diğer içerikler devam eder -->
    </table>
</body>
</html>
";
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
