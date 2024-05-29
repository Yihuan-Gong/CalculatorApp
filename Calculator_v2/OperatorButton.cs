using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_v2
{
    internal class OperatorButton
    {
        /// <summary>
        /// 按下計算機的運算元鍵
        /// </summary>
        /// <param name="data">計算機目前儲存的資料</param>
        /// <param name="op">運算元(+-*/)</param>
        /// <returns>
        /// (string目前的算式, float?計算結果)
        /// 如果沒做計算，則float?計算結果 = null
        /// </returns>
        public (string, float?) clickOperator(CalculatorData data, string op)
        {
            // 情況1：使用者在這一輪第一次按下operator按鈕
            if (!data.recievedOp)
            {
                firstTimeClickOp(data, op);
                return (data.equation, null);
            }

            // 情況2：使用者沒有輸入num2，就重複按了operator按鈕
            if (data.numberQueue == string.Empty)
            {
                repeatedlyClickOp(data, op);
                return (data.equation, null);
            }

            // 情況3：使用者有輸入num2，才按下operator按鈕
            clickOpAsEqual(data, op);
            return (data.equation, data.answer);
        }

        void firstTimeClickOp(CalculatorData data, string op)
        {

            // 更新num1
            data.num1 = (data.numberQueue == string.Empty) ?
                data.answer : float.Parse(data.numberQueue);
            data.recievedNum1 = true;
            data.resetNumberQueue();

            // 更新op
            data.op = op;
            data.recievedOp = true;

            // 寫算式
            data.equation = $"{data.num1} {data.op} ";
        }

        void repeatedlyClickOp(CalculatorData data, string op)
        {
            // 更新op
            data.op = op;
            data.recievedOp = true;

            // 寫算式
            data.equation = $"{data.num1}   {data.op} ";
        }

        void clickOpAsEqual(CalculatorData data, string op)
        {
            data.num2 = float.Parse(data.numberQueue);
            data.recievedNum2 = true;
            data.resetNumberQueue();

            // 計算結果
            AnswerCalculator.calculate(data);

            // 清除這一輪的輸入
            data.resetInput();

            // 下一輪要用的num1是本輪的計算結果
            data.num1 = data.answer;
            data.recievedNum1 = true;

            // 下一輪要用的operator是本次按下的按鈕
            data.op = op;
            data.recievedOp = true;

            // 寫算式
            data.equation = $"{data.num1} {data.op} ";
        }
    }
}
