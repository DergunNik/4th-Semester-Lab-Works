using Microsoft.Maui.Platform;
using Lab1.Model;
using System;
using Microsoft.Maui.Controls;
//using UIKit;

namespace Lab1
{
    public partial class Page1 : ContentPage
    {
        private const int MAX_NUM_LENGTH = 21;

        //HashSet<string> binary_operations = ["+", "-", "x", "/", "f"];
        //HashSet<string> unary_operations = ["1/x", "+/-", "sqrt", "sqr", "%"];
        Equation equation, saved_by_eval_equation;
        InputType last_input_type = InputType.Evaluate;
        double mem;

        public Page1()
        {
            InitializeComponent();
        }

        private async Task Reboot()
        {
            ClearClickHandler(null, null);
            await Shell.Current.DisplayAlert("Restart!", $"The calculator has returned to its initial state.", "OK");
        }

        private void ClearHistory()
        {
            HistoryLabel.Text = string.Empty;
        }

        private void SetMemBtnsInability(bool is_enable)
        {
            if (is_enable)
            {
                MC.IsEnabled = true;
                MR.IsEnabled = true;
                MC.TextColor = (Color)Application.Current.Resources["TextClr"];
                MR.TextColor = (Color)Application.Current.Resources["TextClr"];
            }
            else
            {
                MC.IsEnabled = false;
                MR.IsEnabled = false;
                MC.TextColor = (Color)Application.Current.Resources["InactiveTextClr"];
                MR.TextColor = (Color)Application.Current.Resources["InactiveTextClr"];
            }
        }

        private void AddDigit(ref string num, string digit)
        {
            if (num == "0")
            {
                num = digit;
            }
            else if (num.Length < MAX_NUM_LENGTH)
            {
                num += digit;
            }
        }

        private void DeleteLastDigit(ref string num) 
        {
            if (num.Length == 1)
            {
                num = "0";
                return;
            }
            num = num.Remove(num.Length - 1);
        }

        async private void NumberClickHandler(object sender, EventArgs e)
        {
            string digit = ((Button)sender).Text;

            switch (last_input_type)
            {
                case InputType.Number:
                    if (String.IsNullOrEmpty(equation.SecondNum))
                    {
                        AddDigit(ref equation.FirstNum, digit);
                        MainLabel.Text = equation.FirstNum;
                    } 
                    else
                    {
                        AddDigit(ref equation.SecondNum, digit);
                        MainLabel.Text = equation.SecondNum;
                    }
                    break;
                case InputType.BinaryOperation:
                    equation.SecondNum = digit;
                    MainLabel.Text = equation.SecondNum;
                    break;
                case InputType.UnaryOperation:
                    try
                    {
                        HistoryLabel.Text = Equations.EvaluateEquation(equation).ToString();
                    }
                    catch (Exception ex)
                    {
                        await Shell.Current.DisplayAlert("Error!",$"Error message: {ex.Message}", "OK");
                        await Reboot();
                        return;
                    }
                    equation = new Equation();
                    equation.FirstNum = digit;
                    MainLabel.Text = digit;
                    break;
                case InputType.Evaluate:
                    ClearHistory();
                    equation = new Equation();
                    equation.FirstNum = digit;
                    MainLabel.Text = digit;
                    break;
            }
            last_input_type = InputType.Number;
        }

        async private void BinaryOperationClickHandler(object sender, EventArgs e)
        {
            var op = ((Button)sender).Text;

            if (!String.IsNullOrEmpty(equation.BinaryOperation) && !String.IsNullOrEmpty(equation.SecondNum))
            {
                double num;
                try
                {
                    num = Equations.EvaluateEquation(equation);
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error!", $"Error message: {ex.Message}", "OK");
                    await Reboot();
                    return;
                }
                HistoryLabel.Text = Equations.ApplyOperationStr(equation.FirstNum, equation.FirstUnaryOperation) + " " + op;
                MainLabel.Text = num.ToString();
                equation = new Equation();
                equation.FirstNum = num.ToString();
            }
            else
            {
                HistoryLabel.Text = Equations.ApplyOperationStr(equation.FirstNum, equation.FirstUnaryOperation) + " " + op;
            }

            equation.BinaryOperation = op;
            last_input_type = InputType.BinaryOperation;
        }

        private void PercentClickHandler(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(equation.SecondNum))
            {
                equation = new Equation();
                ClearHistory();
                MainLabel.Text = equation.FirstNum;
                return;
            }

            double tmp = Double.Parse(equation.SecondNum);
            tmp *= Equations.ApplyOperationNum(equation.FirstNum, equation.FirstUnaryOperation) / 100;
            equation.SecondNum = tmp.ToString();
            MainLabel.Text = equation.SecondNum;
            HistoryLabel.Text = Equations.WriteEquation(equation);
            last_input_type = InputType.UnaryOperation;
        }

        async private void UnaryOperationClickHandler(object sender, EventArgs e)
        {
            var op = ((Button)sender).Text;
            if (String.IsNullOrEmpty(op))
                op = "sqrt";
            if (op == "x^2")
                op = "sqr";
            if (op == "MF")
                op = "f";

            try
            {
                if (String.IsNullOrEmpty(equation.FirstUnaryOperation) && String.IsNullOrEmpty(equation.BinaryOperation))
                {
                    equation.FirstUnaryOperation = op;
                    HistoryLabel.Text = Equations.ApplyOperationStr(equation.FirstNum, op);
                    MainLabel.Text = Equations.ApplyOperationNum(equation.FirstNum, op).ToString();
                }
                else if (String.IsNullOrEmpty(equation.BinaryOperation))
                {
                    var num = Equations.EvaluateEquation(equation);
                    equation = new Equation()
                    {
                        FirstNum = num.ToString(),
                        FirstUnaryOperation = op
                    };
                    MainLabel.Text = Equations.ApplyOperationNum(num.ToString(), op).ToString();
                    HistoryLabel.Text = Equations.ApplyOperationStr(num.ToString(), op);
                }
                else if (String.IsNullOrEmpty(equation.SecondUnaryOperation))
                {
                    equation.SecondUnaryOperation = op;
                    HistoryLabel.Text = Equations.WriteEquation(equation);
                    string operand = String.IsNullOrEmpty(equation.SecondNum) ? equation.FirstNum : equation.SecondNum;
                    MainLabel.Text = Equations.ApplyOperationNum(operand, op).ToString();
                }
                else
                {
                    string operand = String.IsNullOrEmpty(equation.SecondNum) ? equation.FirstNum : equation.SecondNum;
                    var num = Equations.ApplyOperationNum(operand, equation.SecondUnaryOperation);
                    equation.SecondNum = num.ToString();
                    equation.SecondUnaryOperation = op;
                    HistoryLabel.Text = Equations.WriteEquation(equation);
                    MainLabel.Text = Equations.ApplyOperationNum(equation.SecondNum, equation.SecondUnaryOperation).ToString();
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Error message: {ex.Message}", "OK");
                await Reboot();
                return;
            }

            last_input_type = InputType.UnaryOperation;
        }

        private void BackspaceClickHandler(object sender, EventArgs e)
        {
            if (last_input_type == InputType.Number)
            {
                if (String.IsNullOrEmpty(equation.SecondNum))
                {
                    DeleteLastDigit(ref equation.FirstNum);
                    MainLabel.Text = equation.FirstNum;
                } 
                else
                {
                    DeleteLastDigit(ref equation.SecondNum);
                    MainLabel.Text = equation.SecondNum;
                }
            }
        }

        private void ClearClickHandler(object sender, EventArgs e)
        {
            equation = new Equation();
            last_input_type = InputType.Evaluate;
            MainLabel.Text = equation.FirstNum;
            MemClearClickHandler(null, null);
            ClearHistory();
        }

        private void ClearEnterClickHandler(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(equation.SecondNum))
            {
                equation.FirstNum = "0";
                equation.FirstUnaryOperation = null;
            }
            else
            {
                equation.SecondNum = "0";
                equation.SecondUnaryOperation = null;
            }
            HistoryLabel.Text = Equations.WriteEquation(equation);
            MainLabel.Text = "0";
        }

        private void MemClearClickHandler(object sender, EventArgs e)
        {
            mem = 0;
            SetMemBtnsInability(false);
        }

        private void MemReloadClickHandler(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(equation.SecondNum))
            {
                equation.FirstNum = mem.ToString();
                equation.FirstUnaryOperation = null;
            }
            else
            {
                equation.SecondNum = mem.ToString();
                equation.SecondUnaryOperation = null;
            }
            HistoryLabel.Text = Equations.WriteEquation(equation);
            MainLabel.Text = mem.ToString();
        }

        private void MemPlusClickHandler(object sender, EventArgs e)
        {
            mem += Double.Parse(MainLabel.Text);
            SetMemBtnsInability(true);
        }

        private void MemMinusClickHandler(object sender, EventArgs e)
        {
            mem -= Double.Parse(MainLabel.Text);
            SetMemBtnsInability(true);
        }

        private void MemSaveClickHandler(object sender, EventArgs e)
        {
            mem-= Double.Parse(MainLabel.Text);
            SetMemBtnsInability(true);
        }

        private void MyFunctionClickHandler(object sender, EventArgs e)
        {
            double degrees = 30; 
            double radians = degrees * (Math.PI / 180); 
            double sinValue = Math.Sin(radians);
        }

        private void DotClickHandler(object sender, EventArgs e)
        {
            if (last_input_type == InputType.Number)
            {
                if (String.IsNullOrEmpty(equation.SecondNum))
                {
                    if (!equation.FirstNum.Contains(','))
                        equation.FirstNum += ",";
                    MainLabel.Text = equation.FirstNum;
                }
                else
                {
                    if (!equation.SecondNum.Contains(','))
                        equation.SecondNum += ",";
                    MainLabel.Text = equation.SecondNum;
                }
                return;
            }

            EvaluateClickHandler(null, null);
            equation.FirstNum = "0,";
            MainLabel.Text = equation.FirstNum;
        }

        async private void EvaluateClickHandler(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(equation.BinaryOperation))
                {
                    saved_by_eval_equation.FirstUnaryOperation = equation.FirstUnaryOperation;
                    saved_by_eval_equation.FirstNum = equation.FirstNum;
                    HistoryLabel.Text = Equations.WriteEquation(saved_by_eval_equation);
                    var tmp_num = Equations.EvaluateEquation(saved_by_eval_equation).ToString();
                    MainLabel.Text = tmp_num;
                    equation.FirstNum = tmp_num;
                    equation.FirstUnaryOperation = null;
                    return;
                }
                HistoryLabel.Text = Equations.WriteEquation(equation);
                var num = Equations.EvaluateEquation(equation).ToString();
                MainLabel.Text = num;
                saved_by_eval_equation = equation;
                equation = new Equation()
                {
                    FirstNum = num
                };
                last_input_type = InputType.Evaluate;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Error message: {ex.Message}", "OK");
                await Reboot();
                return;
            }
        }
    }
}
