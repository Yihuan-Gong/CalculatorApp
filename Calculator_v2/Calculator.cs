using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Calculator_v2
{
    internal class Calculator
    {
        CalculatorData data;
        NumberSetter numberSetter;
        OperatorButton operatorButton;
        EqualButton equalButton;


        public Calculator()
        {
            data = new CalculatorData();
            numberSetter = new NumberSetter();
            operatorButton = new OperatorButton();
            equalButton = new EqualButton();
        }

        /// <summary>
        /// 按下計算機的數字鍵
        /// </summary>
        /// <param name="num">按下的數字</param>
        /// <returns>計算機螢幕顯示的值</returns>
        public string clickNumber(string num)
        {
            return numberSetter.clickNumber(data, num);
        }

        /// <summary>
        /// 按下小數點鍵
        /// </summary>
        /// <returns>計算機螢幕顯示的值</returns>
        public string clickPoint()
        {
            return numberSetter.clickPoint(data);
        }

        /// <summary>
        /// 按下刪除鍵
        /// </summary>
        /// <returns>計算機螢幕顯示的值</returns>
        public string clickBackspace()
        {
            return numberSetter.clickBackspace(data);
        }

        /// <summary>
        /// 按下正負號切換鍵+/-
        /// </summary>
        /// <returns>計算機螢幕顯示數值</returns>
        public string clickNegative()
        {
            return numberSetter.clickNegative(data);
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
            return operatorButton.clickOperator(data, op);
        }

        /// <summary>
        /// 按下計算機的等於鍵
        /// </summary>
        /// <returns>(string目前的算式, float計算結果)</returns>
        public (string, float) clickEqual()
        {
            return equalButton.clickEqual(data);
        }

        /// <summary>
        /// 按下計算機的清除鍵
        /// </summary>
        public void clickClear()
        {
            data.initialize();
        }       
    }
}
