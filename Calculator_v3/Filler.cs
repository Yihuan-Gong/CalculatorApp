using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event
{
    public static class Filler
    {
        public static void FillEquation(Data currData, Data prevData)
        {
            bool recievedNum1 = !string.IsNullOrEmpty(currData.Num1);
            bool recievedNum2 = !string.IsNullOrEmpty(currData.Num2);
            bool recievedOp = !string.IsNullOrEmpty(currData.Oper);

            if (!recievedNum1)
            {
                currData.Num1 = prevData.Ans;
            }

            if (!recievedOp)
            {
                currData.Oper = prevData.Oper;
            }

            if (!recievedNum2)
            {
                currData.Num2 = recievedOp ? currData.Num1 : prevData.Num2;

                if (string.IsNullOrEmpty(currData.Num2)) return;
            }

            CalculationUnit.Calculate(currData);
        }
    }
}
