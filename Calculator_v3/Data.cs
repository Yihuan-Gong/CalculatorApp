using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event
{
    public class Data
    {
        public string Num1;
        public string Oper;
        public string Num2;
        public string Ans;

        public Data()
        {
            Ans = "0";
            Num1 = Num2 = Oper = string.Empty;
        }
    }
}
