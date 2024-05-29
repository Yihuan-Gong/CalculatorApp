using Event;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;
using FluentAssertions.Specialized;

namespace Calculator_v3.Tests
{
    public class CalculationUnitTests
    {
        [Fact]
        public void Calculate_NormalCase_GetAns()
        {
            var data1 = new Data
            {
                Num1 = "2",
                Num2 = "3",
                Oper = "+"
            };
            var data2 = new Data
            {
                Num1 = "2",
                Num2 = "3",
                Oper = "-",
            };
            var data3 = new Data
            {
                Num1 = "2",
                Num2 = "3",
                Oper = "*"
            };
            var data4 = new Data
            {
                Num1 = "3",
                Num2 = "2",
                Oper = "/"
            };

            CalculationUnit.Calculate(data1);
            CalculationUnit.Calculate(data2);
            CalculationUnit.Calculate(data3);
            CalculationUnit.Calculate(data4);

            data1.Ans.Should().Be("5");
            data2.Ans.Should().Be("-1");
            data3.Ans.Should().Be("6");
            data4.Ans.Should().Be("1.5");
        }

        [Fact]
        public void Calculate_Num1OrNum2IsEmpty_ThrowException()
        {
            var data = new Data
            {
                Num1 = "2",
                Num2 = "",
                Oper = "+"
            };

            Action test = () => CalculationUnit.Calculate(data);

            test.Should().Throw<Exception>("Num1 or num2 should not be empty");
        }

        [Fact]
        public void Calculate_Num1OrNum2IsNotNumber_ThrowException()
        {
            var data = new Data
            {
                Num1 = "2",
                Num2 = "abc",
                Oper = "+"
            };

            Action test = () => CalculationUnit.Calculate(data);

            test.Should().Throw<Exception>();
        }

        [Fact]
        public void Calculate_OperatorIsNotCorrect_ThrowException()
        {
            var data = new Data
            {
                Num1 = "2",
                Num2 = "3",
                Oper = "%"
            };

            Action test = () => CalculationUnit.Calculate(data);

            test.Should().Throw<Exception>("Operator need to be +, - ,*, or /");
        }
    }
}
