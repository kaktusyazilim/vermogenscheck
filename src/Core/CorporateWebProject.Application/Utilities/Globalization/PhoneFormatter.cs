using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Utilities.Globalization
{
    public static class PhoneFormatter
    {
        public static string SetPhoneNumber(string phone)
        {
            PhoneNumber pn = PhoneNumberUtil.GetInstance().Parse(phone, "TR");

            return PhoneNumberUtil.GetInstance().Format(pn, PhoneNumberFormat.INTERNATIONAL).Replace("+", string.Empty).Replace(" ", string.Empty);
        }
    }
}
