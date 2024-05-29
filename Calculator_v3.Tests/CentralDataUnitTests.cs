using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event;
using FluentAssertions;

namespace Calculator_v3.Tests
{
    public class CentralDataUnitTests
    {
        private void IndicationUnit_ShowingAtAnswerScreen(object? sender, string e)
        {
        }

        private void IndicationUnit_ShowingAtEquationScreen(object? sender, string e)
        {
        }

        // *******************************
        // 輸入測試

        // *******************************
        [Fact]
        public void SetData_Num1Num2OpPressed_SetNum1OpNum2()
        {
            var dataUnit1 = new CentralDataUnit();
            var dataUnit2 = new CentralDataUnit();
            IndicationUnit.ShowingAtAnswerScreen += IndicationUnit_ShowingAtAnswerScreen;
            IndicationUnit.ShowingAtEquationScreen += IndicationUnit_ShowingAtEquationScreen;

            dataUnit1.SetData("3", ButtonType.Number);
            dataUnit1.SetData(".", ButtonType.Point);
            dataUnit1.SetData("1", ButtonType.Number);
            dataUnit1.SetData("+", ButtonType.Operator);
            dataUnit1.SetData("4", ButtonType.Number);
            dataUnit1.SetData("-", ButtonType.Negative);

            dataUnit2.SetData(".", ButtonType.Point);
            dataUnit2.SetData("-", ButtonType.Negative);
            dataUnit2.SetData("6", ButtonType.Number);
            dataUnit2.SetData(".", ButtonType.Point);
            dataUnit2.SetData("7", ButtonType.Number);
            dataUnit2.SetData("-", ButtonType.Negative);
            dataUnit2.SetData("/", ButtonType.Operator);
            dataUnit2.SetData("4", ButtonType.Number);
            dataUnit2.SetData(".", ButtonType.Point);
            dataUnit2.SetData("-", ButtonType.Negative);
            dataUnit2.SetData("3", ButtonType.Number);

            dataUnit1.GetPrevData().Should().BeEquivalentTo(new Data());
            dataUnit1.GetCurrData().Should().BeEquivalentTo(new Data
            {
                Num1 = "3.1",
                Oper = "+",
                Num2 = "-4",
            });

            dataUnit2.GetPrevData().Should().BeEquivalentTo(new Data());
            dataUnit2.GetCurrData().Should().BeEquivalentTo(new Data
            {
                Num1 = "0.67",
                Oper = "/",
                Num2 = "-4.3",
            });
        }

        // *******************************
        // 運算測試
        [Fact]
        public void SetData_Num1Num2OpEqualPressed_SetPrevData()
        {
            var dataUnit = new CentralDataUnit();
            IndicationUnit.ShowingAtAnswerScreen += IndicationUnit_ShowingAtAnswerScreen;
            IndicationUnit.ShowingAtEquationScreen += IndicationUnit_ShowingAtEquationScreen;

            dataUnit.SetData("3", ButtonType.Number);
            dataUnit.SetData("+", ButtonType.Operator);
            dataUnit.SetData("4", ButtonType.Number);
            dataUnit.SetData("=", ButtonType.Equal);

            dataUnit.GetPrevData().Should().BeEquivalentTo(new Data 
            { 
                Num1 = "3", 
                Oper = "+", 
                Num2 = "4", 
                Ans = "7"
            });
            dataUnit.GetCurrData().Should().BeEquivalentTo(new Data());
        }

        [Fact]
        public void SetData_EqualPressed_UsePrevAnsAsNum1AndUsePrevOpNum2()
        {
            var dataUnit = new CentralDataUnit();
            IndicationUnit.ShowingAtAnswerScreen += IndicationUnit_ShowingAtAnswerScreen;
            IndicationUnit.ShowingAtEquationScreen += IndicationUnit_ShowingAtEquationScreen;

            // 設定prevData
            dataUnit.SetData("3", ButtonType.Number);
            dataUnit.SetData("+", ButtonType.Operator);
            dataUnit.SetData("4", ButtonType.Number);
            dataUnit.SetData("=", ButtonType.Equal);

            // 要測的這一輪
            dataUnit.SetData("=", ButtonType.Equal);

            dataUnit.GetPrevData().Should().BeEquivalentTo(new Data
            {
                Num1 = "7",
                Oper = "+",
                Num2 = "4",
                Ans = "11"
            });
            dataUnit.GetCurrData().Should().BeEquivalentTo(new Data());
        }

        [Fact]
        public void SetData_Num1EqualPressed_CopyPrevOpNum2()
        {
            var dataUnit = new CentralDataUnit();
            IndicationUnit.ShowingAtAnswerScreen += IndicationUnit_ShowingAtAnswerScreen;
            IndicationUnit.ShowingAtEquationScreen += IndicationUnit_ShowingAtEquationScreen;

            // 設定prevData
            dataUnit.SetData("3", ButtonType.Number);
            dataUnit.SetData("+", ButtonType.Operator);
            dataUnit.SetData("4", ButtonType.Number);
            dataUnit.SetData("=", ButtonType.Equal);

            // 要測的這一輪
            dataUnit.SetData("66", ButtonType.Number);
            dataUnit.SetData("=", ButtonType.Equal);

            dataUnit.GetPrevData().Should().BeEquivalentTo(new Data
            {
                Num1 = "66",
                Oper = "+",
                Num2 = "4",
                Ans = "70"
            });
            dataUnit.GetCurrData().Should().BeEquivalentTo(new Data());
        }

        [Fact]
        public void SetData_Num1OpEqualPressed_CopyNum1ToNum2()
        {
            var dataUnit = new CentralDataUnit();
            IndicationUnit.ShowingAtAnswerScreen += IndicationUnit_ShowingAtAnswerScreen;
            IndicationUnit.ShowingAtEquationScreen += IndicationUnit_ShowingAtEquationScreen;

            // 設定prevData
            dataUnit.SetData("3", ButtonType.Number);
            dataUnit.SetData("+", ButtonType.Operator);
            dataUnit.SetData("4", ButtonType.Number);
            dataUnit.SetData("=", ButtonType.Equal);

            // 要測的這一輪
            dataUnit.SetData("6", ButtonType.Number);
            dataUnit.SetData("*", ButtonType.Operator);
            dataUnit.SetData("=", ButtonType.Equal);

            dataUnit.GetPrevData().Should().BeEquivalentTo(new Data
            {
                Num1 = "6",
                Oper = "*",
                Num2 = "6",
                Ans = "36"
            });
            dataUnit.GetCurrData().Should().BeEquivalentTo(new Data());
        }

        [Fact]
        public void SetData_OpEqualPressed_UsePrevAnsAsNum1AndNum2()
        {
            var dataUnit = new CentralDataUnit();
            IndicationUnit.ShowingAtAnswerScreen += IndicationUnit_ShowingAtAnswerScreen;
            IndicationUnit.ShowingAtEquationScreen += IndicationUnit_ShowingAtEquationScreen;

            // 設定prevData
            dataUnit.SetData("3", ButtonType.Number);
            dataUnit.SetData("+", ButtonType.Operator);
            dataUnit.SetData("4", ButtonType.Number);
            dataUnit.SetData("=", ButtonType.Equal);

            // 要測的這一輪
            dataUnit.SetData("*", ButtonType.Operator);
            dataUnit.SetData("=", ButtonType.Equal);

            dataUnit.GetPrevData().Should().BeEquivalentTo(new Data
            {
                Num1 = "7",
                Oper = "*",
                Num2 = "7",
                Ans = "49"
            });
            dataUnit.GetCurrData().Should().BeEquivalentTo(new Data());
        }

        [Fact]
        public void SetData_OpNum2EqualPressed_UsePrevAnsAsNum1()
        {
            var dataUnit = new CentralDataUnit();
            IndicationUnit.ShowingAtAnswerScreen += IndicationUnit_ShowingAtAnswerScreen;
            IndicationUnit.ShowingAtEquationScreen += IndicationUnit_ShowingAtEquationScreen;

            // 設定prevData
            dataUnit.SetData("3", ButtonType.Number);
            dataUnit.SetData("+", ButtonType.Operator);
            dataUnit.SetData("4", ButtonType.Number);
            dataUnit.SetData("=", ButtonType.Equal);

            // 要測的這一輪
            dataUnit.SetData("*", ButtonType.Operator);
            dataUnit.SetData("2", ButtonType.Number);
            dataUnit.SetData("=", ButtonType.Equal);

            dataUnit.GetPrevData().Should().BeEquivalentTo(new Data
            {
                Num1 = "7",
                Oper = "*",
                Num2 = "2",
                Ans = "14"
            });
            dataUnit.GetCurrData().Should().BeEquivalentTo(new Data());
        }
        // ******************************


        // ******************************
        // 按下多次Operator測試
        [Fact]
        public void SetData_Num1OpNum2OpNum2Pressed_CalculateAnsAndUseItAsNum1()
        {
            var dataUnit = new CentralDataUnit();
            IndicationUnit.ShowingAtAnswerScreen += IndicationUnit_ShowingAtAnswerScreen;
            IndicationUnit.ShowingAtEquationScreen += IndicationUnit_ShowingAtEquationScreen;

            dataUnit.SetData("3", ButtonType.Number);
            dataUnit.SetData("+", ButtonType.Operator);
            dataUnit.SetData("4", ButtonType.Number);
            dataUnit.SetData("*", ButtonType.Operator);
            dataUnit.SetData("2", ButtonType.Number);

            dataUnit.GetPrevData().Should().BeEquivalentTo(new Data
            {
                Num1 = "3",
                Oper = "+",
                Num2 = "4",
                Ans = "7"
            });
            dataUnit.GetCurrData().Should().BeEquivalentTo(new Data
            {
                Num1 = "7",
                Oper = "*",
                Num2 = "2",
            });
        }

        [Fact]
        public void SetData_OpPressedContiniously_OverwriteOp()
        {
            var dataUnit = new CentralDataUnit();
            IndicationUnit.ShowingAtAnswerScreen += IndicationUnit_ShowingAtAnswerScreen;
            IndicationUnit.ShowingAtEquationScreen += IndicationUnit_ShowingAtEquationScreen;

            dataUnit.SetData("3", ButtonType.Number);
            dataUnit.SetData("+", ButtonType.Operator);
            dataUnit.SetData("*", ButtonType.Operator);
            dataUnit.SetData("/", ButtonType.Operator);
            dataUnit.SetData("2", ButtonType.Number);

            dataUnit.GetPrevData().Should().BeEquivalentTo(new Data());
            dataUnit.GetCurrData().Should().BeEquivalentTo(new Data
            {
                Num1 = "3",
                Oper = "/",
                Num2 = "2",
            });
        }
        // ***************************************
    }
}
