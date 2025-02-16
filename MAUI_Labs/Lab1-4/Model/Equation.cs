using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Model
{
    public struct Equation
    {
        public string FirstNum = "0";
        public string FirstUnaryOperation;
        public string BinaryOperation;
        public string SecondNum;
        public string SecondUnaryOperation;

        public Equation()
        {
        }
    }

    public enum InputType
    {
        Number,
        BinaryOperation,
        UnaryOperation,
        Evaluate
    }

    static public class Equations
    {
        static public string ApplyOperationStr(string num, string op)
        {
            switch (op)
            {
                case "1/x":
                    return $"1/{num}";
                case "+/-":
                    if (num.First() == '-')
                    {
                        num.Remove(0, 1);
                    }
                    else
                    {
                        num = $"-{num}";
                    }
                    return num;
                case "sqrt":
                    return $"√({num})";
                case "sqr":
                    return $"{num}^2";
                case "f":
                    return $"sin({num})";
            }
            return num;
        }

        static public double ApplyOperationNum(string num_str, string op) // exceptions!!!
        {
            var num = Double.Parse(num_str);
            switch (op)
            {
                case "1/x":
                    if (num == 0)
                        throw  new DivideByZeroException("Division by zero is not allowed.");
                    return 1 / num;
                case "+/-":
                    num = 0 - num;
                    return num;
                case "sqrt":
                    if (num < 0)
                        throw new ArgumentException("Cannot calculate the square root of a negative number.");
                    return Math.Sqrt(num);
                case "sqr":
                    return num * num;
                case "f":
                    var radians = num * (Math.PI / 180); 
                    return Math.Sin(radians);
            }
            return num;
        }

        static public string ApplyOperationStr(string num1, string op, string num2)
        {
            switch (op)
            {
                case "+":
                    return $"{num1} + {num2}";
                case "-":
                    return $"{num1} - {num2}";
                case "/":
                    return $"{num1} / {num2}";
                default:
                    return $"{num1} x {num2}";
            }
        }

        static public double ApplyOperationNum(string num1_str, string op, string num2_str)
        {
            var num1 = Double.Parse(num1_str);
            var num2 = Double.Parse(num2_str);
            switch (op)
            {
                case "+":
                    return num1 + num2;
                case "-":
                    return num1 - num2;
                case "/":
                    if (num2 == 0)
                        throw new DivideByZeroException("Division by zero is not allowed.");
                    return num1 / num2;
                default:
                    return num1 * num2;
            }
        }

        static public double EvaluateEquation(Equation equation)
        {
            if (String.IsNullOrEmpty(equation.BinaryOperation))
            {
                return ApplyOperationNum(equation.FirstNum, equation.FirstUnaryOperation);
            }
            if (String.IsNullOrEmpty(equation.SecondNum))
            {
                return ApplyOperationNum(ApplyOperationNum(equation.FirstNum, equation.FirstUnaryOperation).ToString(),
                                         equation.BinaryOperation,
                                         ApplyOperationNum(equation.FirstNum, equation.SecondUnaryOperation).ToString());
            }
            return ApplyOperationNum(ApplyOperationNum(equation.FirstNum, equation.FirstUnaryOperation).ToString(),
                                         equation.BinaryOperation,
                                         ApplyOperationNum(equation.SecondNum, equation.SecondUnaryOperation).ToString());
        }

        static public string WriteEquation(Equation equation)
        {
            if (String.IsNullOrEmpty(equation.BinaryOperation))
            {
                return ApplyOperationStr(equation.FirstNum, equation.FirstUnaryOperation);
            }
            if (String.IsNullOrEmpty(equation.SecondNum) && String.IsNullOrEmpty(equation.SecondUnaryOperation))
            {
                return ApplyOperationStr(equation.FirstNum, equation.FirstUnaryOperation) + " " + equation.BinaryOperation;
            }
            if (String.IsNullOrEmpty(equation.SecondNum))
            {
                return ApplyOperationStr(ApplyOperationStr(equation.FirstNum, equation.FirstUnaryOperation),
                                         equation.BinaryOperation,
                                         ApplyOperationStr(equation.FirstNum, equation.SecondUnaryOperation));
            }
            return ApplyOperationStr(ApplyOperationStr(equation.FirstNum, equation.FirstUnaryOperation),
                                         equation.BinaryOperation,
                                         ApplyOperationStr(equation.SecondNum, equation.SecondUnaryOperation));
        }
    }
}
