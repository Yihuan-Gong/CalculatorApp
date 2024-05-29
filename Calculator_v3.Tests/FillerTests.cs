using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event;
using FluentAssertions;

namespace Calculator_v3.Tests
{
    public class FillerTests
    {
        [Fact]
        public void FillEquation_Num1OpNum2Recieved_NoChange()
        {
            var currData = new Data
            {
                Num1 = "3",
                Num2 = "4",
                Oper = "+"
            };
            var prevData = new Data
            {
                Num1 = "5",
                Num2 = "6",
                Oper = "*",
                Ans = "30"
            };

            Filler.FillEquation(currData, prevData);

            currData.Num1.Should().Be("3");
            currData.Num2.Should().Be("4");
            currData.Oper.Should().Be("+");
        }

        [Fact]
        public void FillEquation_OpNum2Recieved_UsePrevAnsAsNum1()
        {
            var currData = new Data
            {
                Num2 = "4",
                Oper = "+"
            };
            var prevData = new Data
            {
                Num1 = "5",
                Num2 = "6",
                Oper = "*",
                Ans = "30"
            };

            Filler.FillEquation(currData, prevData);

            currData.Num1.Should().Be("30");
            currData.Num2.Should().Be("4");
            currData.Oper.Should().Be("+");
        }

        [Fact]
        public void FillEquation_Num1OpRecieved_CopyNum1ToNum2()
        {
            var currData = new Data
            {
                Num1 = "3",
                Oper = "+"
            };
            var prevData = new Data
            {
                Num1 = "5",
                Num2 = "6",
                Oper = "*",
                Ans = "30"
            };

            Filler.FillEquation(currData, prevData);

            currData.Num1.Should().Be("3");
            currData.Num2.Should().Be("3");
            currData.Oper.Should().Be("+");
        }

        [Fact]
        public void FillEquation_Num1Recieved_UsePrevNum2AndOper()
        {
            var currData = new Data
            {
                Num1 = "3",
            };
            var prevData = new Data
            {
                Num1 = "5",
                Num2 = "6",
                Oper = "*",
                Ans = "30"
            };

            Filler.FillEquation(currData, prevData);

            currData.Num1.Should().Be("3");
            currData.Num2.Should().Be("6");
            currData.Oper.Should().Be("*");
        }

        [Fact]
        public void FillEquation_OpRecieved_UsePrevAnsAsNum1AndNum2()
        {
            var currData = new Data
            {
                Oper = "+"
            };
            var prevData = new Data
            {
                Num1 = "5",
                Num2 = "6",
                Oper = "*",
                Ans = "30"
            };

            Filler.FillEquation(currData, prevData);

            currData.Num1.Should().Be("30");
            currData.Num2.Should().Be("30");
            currData.Oper.Should().Be("+");
        }

        [Fact]
        public void FillEquation_NothingRecieved_UsePrevAnsAsNum1AndUsePrevNum2Oper()
        {
            var currData = new Data
            {
            };
            var prevData = new Data
            {
                Num1 = "5",
                Num2 = "6",
                Oper = "*",
                Ans = "30"
            };

            Filler.FillEquation(currData, prevData);

            currData.Num1.Should().Be("30");
            currData.Num2.Should().Be("6");
            currData.Oper.Should().Be("*");
        }
    }
}