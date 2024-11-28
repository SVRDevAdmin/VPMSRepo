using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPMS.Lib
{
    public class Helpers
    {
        #region Generate Random String
        public static String GenerateRandomKeyString(int keylength)
        {
            Random rRnd = new Random(Environment.TickCount);

            String chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] buffer = new char[keylength];

            for (int i = 0; i < keylength; i++)
            {
                buffer[i] = chars[rRnd.Next(chars.Length)];
            }

            return new string(buffer);
        }
        #endregion

        #region Get Week of Month 
        static int GetWeekOfYear(DateTime time)
        {
            GregorianCalendar _gc = new GregorianCalendar();
            return _gc.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }

        public static int GetWeekOfMonth(DateTime time)
        {
            DateTime first = new DateTime(time.Year, time.Month, 1);
            return GetWeekOfYear(time) - GetWeekOfYear(first) + 1;
        }
        #endregion

        #region Get Quarter of Year
        public static int GetQuarterOfYear(int iMonth)
        {
            return (iMonth + 2) / 3;
        }
        #endregion
    }
}
