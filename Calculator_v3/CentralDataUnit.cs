using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event
{
    public class CentralDataUnit
    {
        Data prevData;
        Data currData;
        IndicationUnit indicationUnit;

        delegate void Action<T>(ref T data);

        // 這邊想用一個pointer指向目前要被update的數字(Num1或Num2)
        // 這樣可以省下底下很多的if (String.IsNullOrEmpty(currData.Oper))判斷

        public CentralDataUnit()
        {
            prevData = new Data();
            currData = new Data();
            indicationUnit = new IndicationUnit();
        }

        public Data GetCurrData()
        {
            return currData;
        }

        public Data GetPrevData()
        {
            return prevData;
        }

        public IndicationUnit getIndicationUnit()
        {
            return indicationUnit;
        }

        public void SetData(string button, ButtonType buttonType)
        {
            switch (buttonType)
            {
                case ButtonType.Number:
                    setNumber(button);
                    break;

                case ButtonType.Negative:
                    setNegative();
                    break;

                case ButtonType.Point:
                    setPoint();
                    break;

                case ButtonType.Operator:
                    setOperator(button);
                    break;

                case ButtonType.Clear:
                    clear();
                    break;

                case ButtonType.Backspace:
                    backspace();
                    break;

                case ButtonType.Equal:
                    equal();
                    break;
            }
        }

        void setNumber(string button)
        {
            if (string.IsNullOrEmpty(currData.Oper))
            {
                currData.Num1 += button;
                indicationUnit.ShowAtAnswerScreen(currData.Num1);
            }
            else
            {
                currData.Num2 += button;
                indicationUnit.ShowAtAnswerScreen(currData.Num2);
            }
        }

        void setNegative()
        {
            updateNum((ref string num) =>
            {
                if (string.IsNullOrEmpty(num))
                    num = "0";
                else
                    num = (num.Contains("-")) ? num.Split('-')[1] : "-" + num;

                indicationUnit.ShowAtAnswerScreen(num);
            });
        }

        void setPoint()
        {
            updateNum((ref string num) =>
            {
                if (string.IsNullOrEmpty(num))
                    num = "0.";

                if (!num.Contains("."))
                    num += ".";

                indicationUnit.ShowAtAnswerScreen(num);
            });
        }

        void setOperator(string button)
        {
            // 如果輸入完Num2後按下Oper，則需要計算一個結果
            if (!string.IsNullOrEmpty(currData.Num2))
            {
                Filler.FillEquation(currData, prevData);
                indicationUnit.ShowAtAnswerScreen(currData.Ans);

                prevData = currData;
                currData = new Data
                {
                    Num1 = prevData.Ans,
                    Oper = button
                };
            }
            else
                currData.Oper = button;

            indicationUnit.ShowAtEquationScreen(currData);
        }

        void clear()
        {
            currData = new Data();
            prevData = new Data();

            indicationUnit.ShowAtEquationScreen(currData);
            indicationUnit.ShowAtAnswerScreen(string.Empty);
        }

        void backspace()
        {
            updateNum((ref string num) =>
            {
                if (num.Length <= 1)
                    num = "0";
                else
                    num = num.Remove(num.Length - 1);

                indicationUnit.ShowAtAnswerScreen(num);
            });
        }

        void equal()
        {
            Filler.FillEquation(currData, prevData);
            indicationUnit.ShowAtEquationScreen(currData);
            indicationUnit.ShowAtAnswerScreen(currData.Ans);

            prevData = currData;
            currData = new Data();
        }

        void updateNum(Action<string> action)
        {
            if (string.IsNullOrEmpty(currData.Oper))
            {
                action(ref currData.Num1);
            }
            else
            {
                action(ref currData.Num2);
            }
        }
    }
}
