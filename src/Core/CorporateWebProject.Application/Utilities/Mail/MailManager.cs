using CorporateWebProject.Application.Dto.Proposal;
using CorporateWebProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Utilities.Mail
{
    public class MailManager
    {
        public async Task SendProposalForm(ProposalDTO contactMessage, SmtpSettings smtpSettings, Settings Setting)
        {


            try
            {

                SmtpClient client = new SmtpClient(smtpSettings.Server, Convert.ToInt32(smtpSettings.Port));
                client.Timeout = 5;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(smtpSettings.Mail, smtpSettings.MailPassword);
                MailAddress from = new MailAddress(smtpSettings.Mail, smtpSettings.Title);
                MailAddress to = new MailAddress("info@kaktusyazilim.com");
                MailMessage message = new MailMessage(from, to);
                string mail = "";

                mail += "";
                mail += " <style> ";
                mail += "  	body, table, td, a { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; } ";
                mail += " 	table, td { mso-table-lspace: 0pt; mso-table-rspace: 0pt; } ";
                mail += " 	img { -ms-interpolation-mode: bicubic; } ";
                mail += " 	img { border: 0; height: auto; line-height: 100%; outline: none; text-decoration: none; } ";
                mail += " 	table { border-collapse: collapse !important; } ";
                mail += " 	body { height: 100% !important; margin: 0 !important; padding: 0 !important; width: 100% !important; } ";
                mail += " 	a[x-apple-data-detectors] { color: inherit !important;  ";
                mail += " 	text-decoration: none !important; font-size: inherit !important;  ";
                mail += " font-family: inherit !important; font-weight: inherit !important; line-height: inherit !important; } ";
                mail += " 	div[style*='margin: 16px 0;'] { margin: 0 !important; } ";
                mail += "  	</style> ";
                mail += " 	<body style='background-color: #f7f5fa; margin: 0 !important; padding: 0 !important;'> ";
                mail += " 		<table border='0' cellpadding='0' cellspacing='0' width='100%'> ";
                mail += " 			<tr> ";
                mail += " 				<td bgcolor='#e3dada' align='center'> ";
                mail += " 					<table border='0' cellpadding='0' cellspacing='0' width='480' > ";
                mail += " 						<tr> ";
                mail += " 							<td align='center' valign='top' style='padding: 40px 10px 40px 10px;'> ";
                mail += " 								<div style='display: block; font-family: Helvetica, Arial, sans-serif; color: #ffffff; font-size: 18px;' border='0'><img src='" + Setting.Url + Setting.DarkLogo + "' /></div> ";
                mail += " 							</td> ";
                mail += " 						</tr> ";
                mail += " 					</table> ";
                mail += " 				</td> ";
                mail += " 			</tr> ";
                mail += " 			<tr> ";
                mail += " 				<td bgcolor='#e3dada' align='center' style='padding: 0px 10px 0px 10px;'> ";
                mail += " 					<table border='0' cellpadding='0' cellspacing='0' width='480' > ";
                mail += " 						<tr> ";
                mail += " 							<td bgcolor='#ffffff' align='left' valign='top' style='padding: 30px 30px 20px 30px;  ";
                mail += " 							border-radius: 4px 4px 0px 0px; color: #111111; font-family: Helvetica, Arial, sans-serif;  ";
                mail += " font-size: 48px; font-weight: 400; line-height: 48px;'> ";
                mail += " 								<h1 style='font-size: 32px; font-weight: 400; margin: 0;'>İletişim Form Mesajı</h1> ";
                mail += " 							</td> ";
                mail += " 						</tr> ";
                mail += " 					</table> ";
                mail += " 				</td> ";
                mail += " 			</tr> ";
                mail += " 			<tr> ";
                mail += " 				<td bgcolor='#e3dada' align='center' style='padding: 0px 10px 0px 10px;'> ";
                mail += " 					<table border='0' cellpadding='0' cellspacing='0' width='480' > ";
                mail += " 						<tr> ";
                mail += " 							<td bgcolor='#ffffff' align='left'> ";
                mail += " 								<table width='100%' border='0' cellspacing='0' cellpadding='0'> ";
                mail += "                   <tr> ";
                mail += "                     <td colspan='2' style='padding-left:30px;padding-right:15px;padding-bottom:10px;  ";
                mail += " 					font-family: Helvetica, Arial, sans-serif; font-size: 16px; font-weight: 400; line-height: 25px;'> ";
                //mail += "                       <p>Lorem ipsum dolor sit amet,  ";
                //mail += " 					  consetetur sadipscing elitr, sed diam nonumy eirmod  ";
                //mail += " 					  tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum.</p> ";
                mail += "                     </td> ";
                mail += "                   </tr> ";



                mail += " 									<tr> ";
                mail += " 										<th align='left' valign='top' style='padding-left:30px;padding-right:15px;padding-bottom:10px;  ";
                mail += " 										font-family: Helvetica, Arial, sans-serif; font-size: 18px;  ";
                mail += " 										font-weight: 400; line-height: 25px;'>Adınız</th> ";

                mail += " 										<td align='left' valign='top' style='padding-left:15px;padding-right: ";
                mail += " 										30px;padding-bottom:10px;font-family: Helvetica, Arial, sans-serif;  ";
                mail += " 										font-size: 18px; font-weight: 400; line-height: 25px;'>" + contactMessage.Name + "</td> ";
                mail += " 									</tr> ";


                mail += " 									<tr> ";
                mail += " 										<th align='left' valign='top' style='padding-left:30px;padding-right:15px;padding-bottom:10px;  ";
                mail += " 										font-family: Helvetica, Arial, sans-serif; font-size: 18px;  ";
                mail += " 										font-weight: 400; line-height: 25px;'>Soyadınız</th> ";

                mail += " 										<td align='left' valign='top' style='padding-left:15px;padding-right: ";
                mail += " 										30px;padding-bottom:10px;font-family: Helvetica, Arial, sans-serif;  ";
                mail += " 										font-size: 18px; font-weight: 400; line-height: 25px;'>" + contactMessage.Surname + "</td> ";
                mail += " 									</tr> ";





                mail += " 									<tr> ";
                mail += " 										<th align='left' valign='top' style='padding-left:30px;padding-right:15px;padding-bottom:10px;  ";
                mail += " 										font-family: Helvetica, Arial, sans-serif; font-size: 18px;  ";
                mail += " 										font-weight: 400; line-height: 25px;'>Email</th> ";

                mail += " 										<td align='left' valign='top' style='padding-left:15px;padding-right: ";
                mail += " 										30px;padding-bottom:10px;font-family: Helvetica, Arial, sans-serif;  ";
                mail += " 										font-size: 18px; font-weight: 400; line-height: 25px;'>" + contactMessage.Mail + "</td> ";
                mail += " 									</tr> ";





                mail += " 									<tr> ";
                mail += " 										<th align='left' valign='top' style='padding-left:30px;padding-right:15px;padding-bottom:10px;  ";
                mail += " 										font-family: Helvetica, Arial, sans-serif; font-size: 18px;  ";
                mail += " 										font-weight: 400; line-height: 25px;'>Telefon</th> ";

                mail += " 										<td align='left' valign='top' style='padding-left:15px;padding-right: ";
                mail += " 										30px;padding-bottom:10px;font-family: Helvetica, Arial, sans-serif;  ";
                mail += " 										font-size: 18px; font-weight: 400; line-height: 25px;'>" + contactMessage.Phone + "</td> ";
                mail += " 									</tr> ";

                mail += " 									<tr> ";
                mail += " 										<th align='left' valign='top' style='padding-left:30px;padding-right:15px;padding-bottom:10px;  ";
                mail += " 										font-family: Helvetica, Arial, sans-serif; font-size: 18px;  ";
                mail += " 										font-weight: 400; line-height: 25px;'>Marka Adı</th> ";

                mail += " 										<td align='left' valign='top' style='padding-left:15px;padding-right: ";
                mail += " 										30px;padding-bottom:10px;font-family: Helvetica, Arial, sans-serif;  ";
                mail += " 										font-size: 18px; font-weight: 400; line-height: 25px;'>" + contactMessage.BrandName + "</td> ";
                mail += " 									</tr> ";

                mail += " 									<tr> ";
                mail += " 										<th align='left' valign='top' style='padding-left:30px;padding-right:15px;padding-bottom:10px;  ";
                mail += " 										font-family: Helvetica, Arial, sans-serif; font-size: 18px;  ";
                mail += " 										font-weight: 400; line-height: 25px;'>Web Site</th> ";

                mail += " 										<td align='left' valign='top' style='padding-left:15px;padding-right: ";
                mail += " 										30px;padding-bottom:10px;font-family: Helvetica, Arial, sans-serif;  ";
                mail += " 										font-size: 18px; font-weight: 400; line-height: 25px;'>" + contactMessage.Website + "</td> ";
                mail += " 									</tr> ";

                mail += " 									<tr> ";
                mail += " 										<th align='left' valign='top' style='padding-left:30px;padding-right:15px;padding-bottom:10px;  ";
                mail += " 										font-family: Helvetica, Arial, sans-serif; font-size: 18px;  ";
                mail += " 										font-weight: 400; line-height: 25px;'>Hedef Ve Beklenti</th> ";

                mail += " 										<td align='left' valign='top' style='padding-left:15px;padding-right: ";
                mail += " 										30px;padding-bottom:10px;font-family: Helvetica, Arial, sans-serif;  ";
                mail += " 										font-size: 18px; font-weight: 400; line-height: 25px;'>" + contactMessage.Target + "</td> ";
                mail += " 									</tr> ";

                mail += " 									<tr> ";
                mail += " 										<th align='left' valign='top' style='padding-left:30px;padding-right:15px;padding-bottom:10px;  ";
                mail += " 										font-family: Helvetica, Arial, sans-serif; font-size: 18px;  ";
                mail += " 										font-weight: 400; line-height: 25px;'>Teknik Gereksinimler</th> ";

                mail += " 										<td align='left' valign='top' style='padding-left:15px;padding-right: ";
                mail += " 										30px;padding-bottom:10px;font-family: Helvetica, Arial, sans-serif;  ";
                mail += " 										font-size: 18px; font-weight: 400; line-height: 25px;'>" + contactMessage.Tehnical + "</td> ";
                mail += " 									</tr> ";

                mail += " 									<tr> ";
                mail += " 										<th align='left' valign='top' style='padding-left:30px;padding-right:15px;padding-bottom:10px;  ";
                mail += " 										font-family: Helvetica, Arial, sans-serif; font-size: 18px;  ";
                mail += " 										font-weight: 400; line-height: 25px;'>Bütçe Aralığı</th> ";

                mail += " 										<td align='left' valign='top' style='padding-left:15px;padding-right: ";
                mail += " 										30px;padding-bottom:10px;font-family: Helvetica, Arial, sans-serif;  ";
                mail += " 										font-size: 18px; font-weight: 400; line-height: 25px;'>" + contactMessage.Money + "</td> ";
                mail += " 									</tr> ";


                mail += " 									<tr> ";
                mail += " 										<th align='left' valign='top' style='padding-left:30px;padding-right:15px;padding-bottom:10px;  ";
                mail += " 										font-family: Helvetica, Arial, sans-serif; font-size: 18px;  ";
                mail += " 										font-weight: 400; line-height: 25px;'>İstenilen Hizmetler</th> ";

                mail += " 										<td align='left' valign='top' style='padding-left:15px;padding-right: ";
                mail += " 										30px;padding-bottom:10px;font-family: Helvetica, Arial, sans-serif;  ";
                mail += " 										font-size: 18px; font-weight: 400; line-height: 25px;'>" + contactMessage.Services + "</td> ";
                mail += " 									</tr> ";


                mail += " 									<tr> ";
                mail += " 										<th align='left' valign='top' style='padding-left:30px;padding-right:15px;padding-bottom:10px;  ";
                mail += " 										font-family: Helvetica, Arial, sans-serif; font-size: 18px;  ";
                mail += " 										font-weight: 400; line-height: 25px;'>Mesaj</th> ";

                mail += " 										<td align='left' valign='top' style='padding-left:15px;padding-right: ";
                mail += " 										30px;padding-bottom:10px;font-family: Helvetica, Arial, sans-serif;  ";
                mail += " 										font-size: 18px; font-weight: 400; line-height: 25px;'>" + contactMessage.Message + "</td> ";
                mail += " 									</tr> ";



                mail += "                   <tr> ";

                mail += " 								</table> ";
                mail += " 							</td> ";
                mail += " 						</tr> ";
                mail += " 						<tr> ";
                mail += " 							<td bgcolor='#ffffff' align='center'> ";
                mail += " 								<table width='100%' border='0' cellspacing='0' cellpadding='0'> ";
                mail += " 									<tr> ";
                mail += " 										<td bgcolor='#ffffff' align='center' style='padding: 30px 30px 30px 30px; border-top:1px solid #dddddd;'> ";
                mail += " 											<table border='0' cellspacing='0' cellpadding='0'> ";
                mail += " 												<tr> ";
                mail += " 													<td align='left' style='border-radius: 3px;' bgcolor='#e3dada'> ";
                mail += " 														<a href='" + Setting.Url + "/admin'  ";
                mail += " 														target='_blank'  ";
                mail += " 														style='font-size: 20px; font-family: Helvetica, Arial, ";
                mail += " 														sans-serif; color: #ffffff; text-decoration: none; color: black; text-decoration: none; ";
                mail += " 														padding: 11px 22px; border-radius: 2px; border: 1px solid #e3dada; display: inline-block;'>Detaylar</a> ";
                mail += " 													</td> ";
                mail += " 												</tr> ";
                mail += " 											</table> ";
                mail += " 										</td> ";
                mail += " 									</tr> ";
                mail += " 								</table> ";
                mail += " 							</td> ";
                mail += " 						</tr> ";
                mail += " 					</table> ";
                mail += " 				</td> ";
                mail += " 			</tr> ";
                mail += " 			<tr> ";
                mail += " 				<td bgcolor='#e3dada' align='center' style='padding: 0px 10px 0px 10px;'> <table border='0' cellpadding='0' cellspacing='0' width='480'> ";
                mail += " 					<tr> ";
                mail += " 						<td bgcolor='#e3dada' align='left'  ";
                mail += " 						style='padding: 30px 30px 30px 30px; color: #666666;  ";
                mail += " 						font-family: Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 18px;' > ";
                mail += " 							<p style='margin: 0;'>";

                mail += " 						</td> ";
                mail += " 					</tr> ";
                mail += " 				</td> ";
                mail += " 			</tr> ";
                mail += " 		</table> ";
                mail += " 	</body> ";
                mail += " </tbody> ";
                mail += " </table> ";

                message.Body = mail;
                message.IsBodyHtml = true;
                message.Subject = "Kaktüs Teklif Formu";
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

                await client.SendMailAsync(message);




            }
            catch (Exception ex)
            {

            }

        }

        public async Task SendThankYouForm(string mailAddress, SmtpSettings smtpSettings, Settings Setting)
        {


            try
            {

                SmtpClient client = new SmtpClient(smtpSettings.Server, Convert.ToInt32(smtpSettings.Port));
                client.Timeout = 5;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(smtpSettings.Mail, smtpSettings.MailPassword);
                MailAddress from = new MailAddress(smtpSettings.Mail, smtpSettings.Title);
                MailAddress to = new MailAddress(mailAddress);
                MailMessage message = new MailMessage(from, to);
                string mail = "";

                mail += "<div style='border: 3px groove #0E7739;'> <table class='Table' style='width:100.0%;border: 1px solid #27AE60;'> ";
                mail += " 	<tbody> ";
                mail += " 		<tr> ";
                mail += " 			<td> ";
                mail += " 			<h1 style='text-align:center'><span style='font-size:24pt'><span style='font-family:Calibri,sans-serif'><strong><span style='font-size:28.5pt'><span style='font-family:&quot;Arial&quot;,sans-serif'><span style='color:#0e7739'>Dijital &Ccedil;&ouml;z&uuml;mlerinizde Kakt&uuml;s Yazılım Yanınızda!</span></span></span></strong></span></span></h1> ";
                mail += " 			</td> ";
                mail += " 		</tr> ";
                mail += " 	</tbody> ";
                mail += " </table> ";
                mail += " &nbsp; ";
                mail += " <table class='Table' style='box-sizing:border-box; width:100.0%'> ";
                mail += " 	<tbody> ";
                mail += " 		<tr> ";
                mail += " 			<td> ";
                mail += " 			<table align='center' class='Table' style='box-sizing:border-box; width:100.0%'> ";
                mail += " 				<tbody> ";
                mail += " 					<tr> ";
                mail += " 						<td style='border-bottom:none; border-left:none; border-right:none; border-top:1px solid #0e7739'><span style='font-size:11pt'><span style='font-family:Calibri,sans-serif'> </span></span></td> ";
                mail += " 					</tr> ";
                mail += " 				</tbody> ";
                mail += " 			</table> ";
                mail += " 			</td> ";
                mail += " 		</tr> ";
                mail += " 	</tbody> ";
                mail += " </table> ";
                mail += " &nbsp; ";
                mail += " <table class='Table' style='box-sizing:border-box; width:100.0%; word-break:break-word'> ";
                mail += " 	<tbody> ";
                mail += " 		<tr> ";
                mail += " 			<td> ";
                mail += " 			<div style='text-align:center'><span style='font-size:11pt'><span style='font-family:Calibri,sans-serif'><span style='font-size:11.5pt'><span style='font-family:&quot;Arial&quot;,sans-serif'><span style='color:#101112'>Merhaba! Bizimle iletişime ge&ccedil;tiğiniz i&ccedil;in teşekk&uuml;r ederiz. Formunuz tarafımıza ulaştı!</span></span></span></span></span></div> ";
                mail += " 			<div style='text-align:center'><span style='font-size:11pt'><span style='font-family:Calibri,sans-serif'><span style='font-size:11.5pt'><span style='font-family:&quot;Arial&quot;,sans-serif'><span style='color:#101112'>İstek ve ihtiya&ccedil;larınızı inceleyip en kısa s&uuml;rede d&ouml;n&uuml;ş yapıyor olacağız.</span></span></span></span></span></div> ";
                mail += " 			</td> ";
                mail += " 		</tr> ";
                mail += " 	</tbody> ";
                mail += " </table> ";
                mail += " &nbsp; ";
                mail += " <table class='Table' style='box-sizing:border-box; width:100.0%'> ";
                mail += " 	<tbody> ";
                mail += " 		<tr> ";
                mail += " 			<td style='text-align:center; width:594px'><span style='font-size:11pt'><span style='font-family:Calibri,sans-serif'><img height='300' width='auto' alt='Image' src='https://kaktusyazilim.com/uploads/638275612961497950kaktus-geridon.png' style='height:297px; width:594px' /></span></span></td> ";
                mail += " 		</tr> ";
                mail += " 	</tbody> ";
                mail += " </table> ";
                mail += " &nbsp; ";
                mail += " <table class='Table' style='box-sizing:border-box; width:100.0%'> ";
                mail += " 	<tbody> ";
                mail += " 		<tr> ";
                mail += " 			<td> ";
                mail += " 			<table align='center' class='Table' style='box-sizing:border-box'> ";
                mail += " 				<tbody> ";
                mail += " 					<tr> ";
                mail += " 						<td><span style='font-size:11pt'><span style='font-family:Calibri,sans-serif'><a href='https://www.linkedin.com/company/kakt%C3%BCs-yaz%C4%B1l%C4%B1m/' style='color:#0563c1; text-decoration:underline' title='&quot;https://www.linkedin.com/company/kakt%C3%BCs-yaz%C4%B1l%C4%B1m/&quot;'><span style='color:#0078d7'><img height='30' width='auto' alt='Linkedin' src='https://kaktusyazilim.com/uploads/638275614281902097instagram.png' style='height:32px; width:32px' /></span></a></span></span></td> ";
                mail += " 						<td><span style='font-size:11pt'><span style='font-family:Calibri,sans-serif'><a href='https://www.instagram.com/kaktusyazilimcom' style='color:#0563c1; text-decoration:underline' title='&quot;https://www.instagram.com/kaktusyazilimcom&quot;'><span style='color:#0078d7'><img  height='30' width='auto'  alt='Instagram' src='https://kaktusyazilim.com/uploads/638275614386271318instagram-1.png' style='height:32px; width:32px' /></span></a></span></span></td> ";
                mail += " 						<td><span style='font-size:11pt'><span style='font-family:Calibri,sans-serif'><a href='https://www.facebook.com/kaktusyazilimcom' style='color:#0563c1; text-decoration:underline' title='&quot;https://www.facebook.com/kaktusyazilimcom&quot;'><span style='color:#0078d7'><img  height='30' width='auto'  alt='Facebook' src='https://kaktusyazilim.com/uploads/638275614458381351facebook.png' style='height:32px; width:32px' /></span></a></span></span></td> ";
                mail += " 						<td>&nbsp;</td> ";
                mail += " 					</tr> ";
                mail += " 				</tbody> ";
                mail += " 			</table> ";
                mail += " 			</td> ";
                mail += " 		</tr> ";
                mail += " 	</tbody> ";
                mail += " </table> ";
                mail += " &nbsp; ";
                mail += " <table class='Table' style='box-sizing:border-box; width:100.0%'> ";
                mail += " 	<tbody> ";
                mail += " 		<tr> ";
                mail += " 			<td style='text-align:center'><span style='font-size:11pt'><span style='font-family:Calibri,sans-serif'><span style='background-color:silver'><a href='http://www.kaktusyazilim.com/' style='color:#0563c1; text-decoration:underline'>Web sitesinde kaldığın yerden devam et!</a></span></span></span></td> ";
                mail += " 		</tr> ";
                mail += " 	</tbody> ";
                mail += " </table> ";
                mail += " <br /> ";
                mail += " <br /> ";
                mail += " <br /> ";
                mail += " <br /> ";
                mail += " <br /> ";
                mail += " <br /> ";
                mail += " &nbsp; ";
                mail += " <table align='center' class='Table'> ";
                mail += " 	<tbody> ";
                mail += " 		<tr> ";
                mail += " 			<td>&nbsp;</td> ";
                mail += " 			<td>&nbsp;</td> ";
                mail += " 			<td>&nbsp;</td> ";
                mail += " 			<td>&nbsp;</td> ";
                mail += " 		</tr> ";
                mail += " 	</tbody> ";
                mail += " </table> </div>  ";

                message.Body = mail;
                message.IsBodyHtml = true;
                message.Subject = "Kaktüs Yazılım Müşteri Hizmetleri";
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
