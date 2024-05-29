using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Calculator
{
    internal class Calculator
    {
        // 算式
        // num1 (operator) num2 = answer

        float num1;
        float num2;
        float answer;
        string op; // operator
        string equation;
        string numberQueue; // 使用者輸入的數字會暫存於此

        bool recievedOp;
        bool recievedNum1;
        bool recievedNum2;


        public Calculator()
        {
            initialize();
        }

        /// <summary>
        /// 按下計算機的數字鍵
        /// </summary>
        /// <param name="num">按下的數字</param>
        /// <returns>計算機螢幕顯示的值</returns>
        public string clickNumber(string num)
        {
            this.numberQueue += num;
            return this.numberQueue;
        }

        /// <summary>
        /// 按下小數點鍵
        /// </summary>
        /// <returns>計算機螢幕顯示的值</returns>
        public string clickPoint()
        {
            if (!this.numberQueue.Contains("."))
                this.numberQueue += ".";

            return this.numberQueue;
        }

        /// <summary>
        /// 按下刪除鍵
        /// </summary>
        /// <returns>計算機螢幕顯示的值</returns>
        public string clickBackspace()
        {
            if (this.numberQueue.Length <= 1)
                this.numberQueue = "0";
            else
                this.numberQueue = this.numberQueue.Remove(numberQueue.Length - 1);

            return this.numberQueue;
        }

        /// <summary>
        /// 按下計算機的運算元鍵
        /// </summary>
        /// <param name="op">運算元(+-*/)</param>
        /// <returns>
        /// (string目前的算式, float?計算結果)
        /// 如果沒做計算，則float?計算結果 = null
        /// </returns>
        public (string, float?) clickOperator(string op)
        {
            // 情況1：使用者在這一輪第一次按下operator按鈕
            if (!this.recievedOp)
            {
                firstTimeClickOp(op);
                return (this.equation, null);
            }

            // 情況2：使用者沒有輸入num2，就重複按了operator按鈕
            if (this.numberQueue == string.Empty)
            {
                repeatedlyClickOp(op);
                return (this.equation, null);
            }

            // 情況3：使用者有輸入num2，才按下operator按鈕
            clickOpAsEqual(op);
            return (this.equation, this.answer);
        }

        /// <summary>
        /// 按下計算機的等於鍵
        /// </summary>
        /// <returns>(string目前的算式, float計算結果)</returns>
        public (string, float) clickEqual()
        {
            // 更新目前接收到使用者輸入的數字
            updateNumber();

            // 如果使用者沒有完整輸入num1, op, num2，則自動補齊
            completeEquation();

            // 計算結果
            calculate();

            // 寫算式
            if (this.op == string.Empty)
                this.equation = $"{this.num1} = ";
            else
                this.equation = $"{this.num1} {this.op} {this.num2} = ";

            // 清除這一輪的輸入
            resetInput();

            return (this.equation, this.answer);
        }

        /// <summary>
        /// 按下正負號切換鍵+/-
        /// </summary>
        /// <returns>計算機螢幕顯示數值</returns>
        public string clickNegative()
        {
            if (!this.recievedOp)
            {
                // 情況1：輸入num1之前按下+/-
                if (this.numberQueue == string.Empty)
                {
                    clickNegativeBeforeNum1();
                    return this.numberQueue;
                }

                // 情況2：輸入num1過程中按下+/-
                clickNegativeDuringNum1();
                return this.numberQueue;
            }

            // 情況3：輸入num2之前按下+/-
            if (this.numberQueue == string.Empty)
            {
                clickNegativeBeforeNum2();
                return this.numberQueue;
            }

            // 情況4：輸入num2過程中按下+/-
            clickNegativeDuringNum2();
            return this.numberQueue;
        }


        void clickNegativeBeforeNum1()
        {
            this.numberQueue = ((-1) * this.answer).ToString();
        }

        void clickNegativeDuringNum1()
        {
            clickNegativeDuringEnteringNumber();
        }

        void clickNegativeBeforeNum2()
        {
            this.numberQueue = ((-1) * this.num1).ToString();
        }

        void clickNegativeDuringNum2()
        {
            clickNegativeDuringEnteringNumber();
        }

        void clickNegativeDuringEnteringNumber()
        {
            if (this.numberQueue[0] != '-')
                this.numberQueue = "-" + this.numberQueue;
            else
                this.numberQueue = this.numberQueue.Remove(0, 1);
        }

        /// <summary>
        /// 按下計算機的清除鍵
        /// </summary>
        public void clickClear()
        {
            initialize();
        }


        void initialize()
        {
            this.num1 = 0;
            this.num2 = 0;
            this.answer = 0;
            this.op = string.Empty;
            this.equation = string.Empty;

            this.numberQueue = string.Empty;

            this.recievedOp = false;
            this.recievedNum1 = false;
            this.recievedNum2 = false;
        }

        void firstTimeClickOp(string op)
        {

            // 更新num1
            this.num1 = (this.numberQueue == string.Empty) ?
                this.answer : float.Parse(this.numberQueue);
            this.recievedNum1 = true;
            resetNumberQueue();

            // 更新op
            this.op = op;
            this.recievedOp = true;

            // 寫算式
            this.equation = $"{this.num1} {this.op} ";
        }

        void repeatedlyClickOp(string op)
        {
            // 更新op
            this.op = op;
            this.recievedOp = true;

            // 寫算式
            this.equation = $"{this.num1} {this.op} ";
        }

        void clickOpAsEqual(string op)
        {
            this.num2 = float.Parse(this.numberQueue);
            resetNumberQueue();

            // 計算結果
            calculate();

            // 清除這一輪的輸入
            resetInput();

            // 下一輪要用的num1是本輪的計算結果
            this.num1 = this.answer;
            this.recievedNum1 = true;

            // 下一輪要用的operator是本次按下的按鈕
            this.op = op;
            this.recievedOp = true;

            // 寫算式
            this.equation = $"{this.num1} {this.op} ";
        }

        void completeEquation()
        {
            if (this.recievedNum1 == false)
                this.num1 = this.answer;

            if (this.recievedNum2 == false)
                this.num2 = (this.recievedOp) ? this.num1 : this.num2;
        }

        void calculate()
        {
            if (this.op == string.Empty)
                this.answer = this.num1;
            else
                this.answer = operation(this.num1, this.num2, this.op);
        }

        void resetInput()
        {
            this.recievedNum1 = false;
            this.recievedNum2 = false;
            this.recievedOp = false;
        }

        float operation(float num1, float num2, string op)
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

        void updateNumber()
        {
            if (this.recievedOp)
            {
                retrieveNumberFromQueue(ref this.num2, ref this.recievedNum2);
                resetNumberQueue();
            }
            else
            {
                retrieveNumberFromQueue(ref this.num1, ref this.recievedNum1);
                resetNumberQueue();
            }
        }

        /// <summary>
        /// 根據使用者數字輸入暫存(numberQueue)來更新數字。如果numberQueue是空的，則數字不更新
        /// </summary>
        /// <param name="number">要被更新的數字</param>
        /// <param name="recieved">是否更新成功</param>
        void retrieveNumberFromQueue(ref float number, ref bool recieved)
        {
            // 如果numberQueue是空的，則不做update
            if (this.numberQueue == string.Empty)
                return;

            number = float.Parse(this.numberQueue);
            recieved = true;
        }

        void resetNumberQueue()
        {
            this.numberQueue = string.Empty;
        }
    }
}
