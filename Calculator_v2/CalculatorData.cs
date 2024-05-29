using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_v2
{
    internal class CalculatorData
    {
        // 算式
        // num1 (operator) num2 = answer

        public float num1;
        public float num2;
        public float answer;
        public string op; // operator
        public string equation;
        public string numberQueue; // 使用者輸入的數字會暫存於此

        public bool recievedOp;
        public bool recievedNum1;
        public bool recievedNum2;


        public CalculatorData()
        {
            initialize();
        }

        public void initialize()
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

        /// <summary>
        /// 根據使用者數字輸入暫存(numberQueue)來更新數字。如果numberQueue是空的，則數字不更新
        /// </summary>
        /// <param name="number">要被更新的數字</param>
        /// <param name="recieved">是否更新成功</param>
        public void retrieveNumberFromQueue(ref float number, ref bool recieved)
        {
            // 如果numberQueue是空的，則不做update
            if (this.numberQueue == string.Empty)
                return;

            number = float.Parse(this.numberQueue);
            recieved = true;
        }

        public void resetNumberQueue()
        {
            this.numberQueue = string.Empty;
        }

        public void resetInput()
        {
            this.recievedNum1 = false;
            this.recievedNum2 = false;
            this.recievedOp = false;
        }
    }
}
