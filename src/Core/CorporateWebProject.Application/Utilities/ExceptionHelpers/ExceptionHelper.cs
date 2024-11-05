using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Utilities.ExceptionHelpers
{
    public static class ExceptionHelper
    {
        public static string GetErrorMessage(Exception exception )
        {
            var st = new StackTrace(exception, true);
            var frame = st.GetFrame(0);
            var line = frame.GetFileLineNumber();
            return "Hata :" + exception.Message + "\n Satır : " + line;
        }
    }
}
