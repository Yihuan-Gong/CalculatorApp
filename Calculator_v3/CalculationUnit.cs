using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event
{
    public static class CalculationUnit
    {
        public static void Calculate(Data data)
        {
            if (string.IsNullOrEmpty(data.Num1) || string.IsNullOrEmpty(data.Num2))
            {
                throw new Exception("Num1 or num2 should not be empty");
            }

            float num1 = float.Parse(data.Num1);
            float num2 = float.Parse(data.Num2);

            data.Ans = operation(num1, num2, data.Oper).ToString();
        }

        static float operation(float num1, float num2, string op)
        {
            switch (op)
            {
                case "+":
                    return num1 + num2;

                case "-":
                    return num1 - num2;

                case "*":
                    return num1 * num2;

                case "/":
                    return num1 / num2;
            }

            throw new Exception("Operator need to be +, - ,*, or /");
        }
    }
}
