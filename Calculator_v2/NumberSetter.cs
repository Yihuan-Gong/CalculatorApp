using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_v2
{
    internal class NumberSetter
    {
        /// <summary>
        /// 按下計算機的數字鍵
        /// </summary>
        /// <param name="data">計算機目前儲存的資料</param>
        /// <param name="num">按下的數字</param>
        /// <returns>計算機螢幕顯示的值</returns>
        public string clickNumber(CalculatorData data, string num)
        {
            data.numberQueue += num;
            return data.numberQueue;
        }

        /// <summary>
        /// 按下小數點鍵
        /// </summary>
        /// <param name="data">計算機目前儲存的資料</param>
        /// <returns>計算機螢幕顯示的值</returns>
        public string clickPoint(CalculatorData data)
        {
            if (!data.numberQueue.Contains("."))
                data.numberQueue += ".";

            return data.numberQueue;
        }

        /// <summary>
        /// 按下刪除鍵
        /// </summary>
        /// <param name="data">計算機目前儲存的資料</param>
        /// <returns>計算機螢幕顯示的值</returns>
        public string clickBackspace(CalculatorData data)
        {
            if (data.numberQueue.Length <= 1)
                data.numberQueue = "0";
            else
                data.numberQueue = data.numberQueue.Remove(data.numberQueue.Length - 1);

            return data.numberQueue;
        }

        /// <summary>
        /// 按下正負號切換鍵+/-
        /// </summary>
        /// <param name="data">計算機目前儲存的資料</param>
        /// <returns>計算機螢幕顯示數值</returns>
        public string clickNegative(CalculatorData data)
        {
            if (!data.recievedOp)
            {
                // 情況1：輸入num1之前按下+/-
                if (data.numberQueue == string.Empty)
                {
                    clickNegativeBeforeNum1(data);
                    return data.numberQueue;
                }

                // 情況2：輸入num1過程中按下+/-
                clickNegativeDuringNum1(data);
                return data.numberQueue;
            }

            // 情況3：輸入num2之前按下+/-
            if (data.numberQueue == string.Empty)
            {
                clickNegativeBeforeNum2(data);
                return data.numberQueue;
            }

            // 情況4：輸入num2過程中按下+/-
            clickNegativeDuringNum2(data);
            return data.numberQueue;
        }


        void clickNegativeBeforeNum1(CalculatorData data)
        {
            data.numberQueue = ((-1) * data.answer).ToString();
        }

        void clickNegativeDuringNum1(CalculatorData data)
        {
            clickNegativeDuringEnteringNumber(data);
        }

        void clickNegativeBeforeNum2(CalculatorData data)
        {
            data.numberQueue = ((-1) * data.num1).ToString();
        }

        void clickNegativeDuringNum2(CalculatorData data)
        {
            clickNegativeDuringEnteringNumber(data);
        }

        void clickNegativeDuringEnteringNumber(CalculatorData data)
        {
            if (data.numberQueue[0] != '-')
                data.numberQueue = "-" + data.numberQueue;
            else
                data.numberQueue = data.numberQueue.Remove(0, 1);
        }
    }
}
