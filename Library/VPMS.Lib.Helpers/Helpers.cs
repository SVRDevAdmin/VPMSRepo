using System;
using System.Collections.Generic;
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
    }
}
