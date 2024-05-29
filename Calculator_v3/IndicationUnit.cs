using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event
{
    public class IndicationUnit
    {
        public static event EventHandler<string> ShowingAtAnswerScreen;
        public static event EventHandler<string> ShowingAtEquationScreen;

        public void ShowAtAnswerScreen(string strToShow)
        {
            ShowingAtAnswerScreen.Invoke(this, strToShow);
        }

        public void ShowAtEquationScreen(Data data)
        {
            string equationScreenIndication;

            if (string.IsNullOrEmpty(data.Num2))
                equationScreenIndication = $"{data.Num1} {data.Oper}";
            else
                equationScreenIndication = $"{data.Num1} {data.Oper} {data.Num2} =";

            ShowingAtEquationScreen.Invoke(this, equationScreenIndication);
        }
    }
}
