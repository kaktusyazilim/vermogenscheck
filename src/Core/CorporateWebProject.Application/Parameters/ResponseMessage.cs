using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Parameters
{
    public static class ResponseMessage
    {
        // WEB API
        public static string SuccessMessage { get { return "İşleminiz başarıyla tamamlandı."; } }
        public static string FailedMessage { get { return "İşleminiz tamamlanırken bir hata meydana geldi. Lütfen daha sonra tekrar deneyiniz"; } }
        public static string NotFoundObject { get { return "Obje bulunamadı."; } }
        public static string DifferentDevice { get { return "Giriş yapmaya çalıştığını cihaz ile sisteme kayıtlı olan cihaz bilgileri farklı."; } }
        public static string NullObject { get { return "Göndermiş olduğunuz model null."; } }
        public static string DontFoundOrder { get { return "İade etmeye çalıştığınız sipariş bulunamadı."; } }
        public static string RequiredMessage { get { return "Lütfen zorunlu alanların hepsini doldurunuz."; } }
        public static string IncorrectPassword { get { return "Girmiş olduğunuz şifre ve mevcut şifreniz farklı olduğu için işleminize devam edilemiyor."; } }
        public static string AlreadyCustomerData { get { return "Bu telefon numarası ile daha önce hesap açılmış. Lütfen farklı bir telefon numarası deneyiniz."; } }
        public static string RemovedUser { get { return "İşlem yapmaya çalıştığınız kullanıcı silinmiş veya pasife alınmış olabilir."; } }
        public static string Unauthorized { get { return "Geçersiz yetki erişimi."; } }
        public static string RemovedLesson { get { return "İşlem yapmaya çalıştığınız ders silinmiş veya pasife alınmış olabilir."; } }
        public static string RemovedLessonContent { get { return "İşlem yapmaya çalıştığınız ders içeriği silinmiş veya pasife alınmış olabilir."; } }
        public static string DontFoundCustomer { get { return "Girmiş olduğunuz telefon numarasına kayıtlı bir kullanıcı bulunamadı."; } }
        public static string WrongPassword { get { return "Girmiş olduğunuz şifre yanlış."; } }
        public static string FailedCustomer { get { return "Müşteri sisteme eklenirken bir hata meydana geldi. Lütfen daha sonra tekrar deneyiniz."; } }
        public static string FailedAddVertification { get { return "Müşteriye ait onay kodu oluşturulurken bir hata meydana geldi. Lütfen daha sonra tekrar deneyiniz."; } }
        public static string NotFoundSmsSupporter { get { return "Mesaj gönderilecek telekom operatörü bulunamadı. Lütfen daha sonra tekrar deneyiniz."; } }
        public static string FailedSendSms { get { return "Müşteriye ait onay kodu mesaj olarak iletilirken bir hata meydana geldi. Lütfen daha sonra tekrar deneyiniz."; } }
        public static string WrongToken { get { return "Telefonunuza gelmiş olan onay kodu ile sistemdeki onay kodu birbiriyle eşleşmemektedir."; } }
        public static string AgainSendToken { get { return "Onay kodunuz zaman aşımına uğradığı için yeni bir onay kodu telefonunuza gönderilmiştir. Lütfen telefonunuza gönderilen son mesajtaki onay kodunu giriniz."; } }
        public static string NotVertificationUser { get { return "401"; } }
        public static string WaitToken { get { return "Göndermiş olduğumuz onay kodunun ömrü henüz dolmadı. Lütfen 3 dakika sonra tekrar deneyiniz."; } }
        public static string DontFoundLessonOrCustomer { get { return "Satın almaya çalıştığınız ürün veya işlem yaptığınız hesap bilgisi bulunamadı."; } }

        // WEB UI
        public static string SuccessResponse { get { return "İşleminiz başarıyla tamamlandı."; } }
        public static string ErrorResponse { get { return "Bir hata meydana geldi. Lütfen daha sonra tekrar deneyiniz."; } }
        public static string ModelNotValid { get { return "Lütfen tüm alanları eksiksiz ve doğru bir şekilde gönderdiğinizden emin olun."; } }
        public static string SetHasAlreadyData { get { return "Bu e-posta adresi ile daha önce bir kullanıcı oluşturulmuş. Lütfen başka bir e-posta adresi ile hesap açınız."; } }
        public static string SetCustomerHasAlreadyData { get { return "Bu telefon numarası veya müşteri kodu kullanılarak daha önce bir kullanıcı oluşturulmuş. Lütfen başka bir telefon numarası veya müşteri kodu ile hesap açınız."; } }

    }
}
