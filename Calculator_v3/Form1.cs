using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Event
{
    public partial class Form1 : Form
    {
        EventHandler eventHandler = new EventHandler();

        public Form1()
        {
            InitializeComponent();

            IndicationUnit.ShowingAtEquationScreen += ShowEquationScreen;
            IndicationUnit.ShowingAtAnswerScreen += ShowAnswerScreen;
        }

        private void ShowEquationScreen(object sender, string e)
        {
            EquationScreen.Text = e;
        }

        private void ShowAnswerScreen(object sender, string e)
        {
            AnswerScreen.Text = e;
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string text = button.Text;

            switch (text)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    eventHandler.AssignButton(text, ButtonType.Number);
                    break;

                case ".":
                    eventHandler.AssignButton(text, ButtonType.Point);
                    break;

                case "+":
                case "-":
                case "*":
                case "/":
                    eventHandler.AssignButton(text, ButtonType.Operator);
                    break;

                case "+/-":
                    eventHandler.AssignButton(text, ButtonType.Negative);
                    break;

                case "=":
                    eventHandler.AssignButton(text, ButtonType.Equal);
                    break;

                case "Backspace":
                    eventHandler.AssignButton(text, ButtonType.Backspace);
                    break;

                case "C":
                    eventHandler.AssignButton(text, ButtonType.Clear);
                    break;

            }
        }
    }
}
