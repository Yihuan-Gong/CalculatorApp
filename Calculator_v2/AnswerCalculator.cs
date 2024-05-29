using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_v2
{
    internal class AnswerCalculator
    {
        /// <summary>
        /// 將num1、運算元和num2補齊
        /// </summary>
        /// <param name="data">計算機資料</param>
        public static void completeEquation(CalculatorData data)
        {
            if (data.recievedNum1 == false)
                data.num1 = data.answer;

            if (data.recievedNum2 == false)
                data.num2 = (data.recievedOp) ? data.num1 : data.num2;

            data.recievedNum1 = true;
            data.recievedNum2 = true;
            data.recievedOp = true;
        }

        /// <summary>
        /// 根據num1、運算元和num2，計算答案
        /// </summary>
        /// <param name="data">計算機資料</param>
        /// <returns></returns>
        /// <exception cref="Exception">num1、運算元和num2資料不齊全</exception>
        public static float calculate(CalculatorData data)
        {
            if (!(data.recievedNum1 && data.recievedNum2 && data.recievedOp))
                throw new Exception("Num1, operator and num2 should be given");
            
            if (data.op == string.Empty)
                data.answer = data.num1;
            else
                data.answer = operation(data.num1, data.num2, data.op);

            return data.answer;
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
