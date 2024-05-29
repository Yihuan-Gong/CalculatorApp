using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        Calculator calculator;

        public Form1()
        {
            InitializeComponent();
            calculator = new Calculator();
        }

        private void NumberClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            // 讀取使用者輸入的數字，並顯示在螢幕上
            this.screen.Text = this.calculator.clickNumber(button.Text);
        }

        private void OperatorClick(object sender, EventArgs e)
        {
            string equation;
            float? answer;
            Button button = (Button)sender;

            // 讀取使用者輸入的operator
            (equation, answer) = this.calculator.clickOperator(button.Text);

            // 顯示算式
            this.equationScreen.Text = equation;

            // 顯示計算結果
            if (answer != null)
                screen.Text = answer.ToString();
        }

        private void ClearClick(object sender, EventArgs e)
        {
            this.calculator.clickClear();

            this.screen.Text = "0";
            this.equationScreen.Text = "";
        }

        private void EqualClick(object sender, EventArgs e)
        {
            string equation;
            float answer;

            (equation, answer) = this.calculator.clickEqual();

            // 顯示算式
            this.equationScreen.Text = equation;

            // 顯示計算結果
            this.screen.Text = answer.ToString();
        }

        private void NegativeClick(object sender, EventArgs e)
        {
            this.screen.Text = this.calculator.clickNegative();
        }

        private void PointClick(object sender, EventArgs e)
        {
            this.screen.Text = this.calculator.clickPoint();
        }

        private void BackspaceClick(object sender, EventArgs e)
        {
            this.screen.Text = this.calculator.clickBackspace();
        }
    }
}
