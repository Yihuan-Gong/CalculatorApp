using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_v2
{
    internal class EqualButton
    {
        /// <summary>
        /// 按下計算機的等於鍵
        /// </summary>
        /// <param name="data">計算機目前儲存的資料</param>
        /// <returns>(string目前的算式, float計算結果)</returns>
        public (string, float) clickEqual(CalculatorData data)
        {
            // 更新目前接收到使用者輸入的數字
            updateNumber(data);

            // 如果使用者沒有完整輸入num1, op, num2，則自動補齊
            AnswerCalculator.completeEquation(data);

            // 計算結果
            AnswerCalculator.calculate(data);

            // 寫算式
            if (data.op == string.Empty)
                data.equation = $"{data.num1} = ";
            else
                data.equation = $"{data.num1} {data.op} {data.num2} = ";

            // 清除這一輪的輸入
            data.resetInput();

            return (data.equation, data.answer);
        }

        void updateNumber(CalculatorData data)
        {
            if (data.recievedOp)
            {
                data.retrieveNumberFromQueue(ref data.num2, ref data.recievedNum2);
                data.resetNumberQueue();
            }
            else
            {
                data.retrieveNumberFromQueue(ref data.num1, ref data.recievedNum1);
                data.resetNumberQueue();
            }
        }
    }
}
