using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(tt("3+4 "));
            var input = Console.ReadLine();

            Console.WriteLine(Calculate(input));
            //Console.WriteLine(tt("7 / 8 "));
            Console.ReadLine();


        }

        static string Calculate(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "0";
            }

            input = input.Trim();
            var stack = new Stack<string>();
            var operators = new[] { '+', '-', '*', '/' };
            var lastNumberIndex = input.Length;
            for (var i = input.Length - 1; i >= 0; i--)
            {
                var c = input[i];
                if (c != ' ' && operators.Any(x => c == x))
                {
                    var @operator = c.ToString();
                    //var normalPush = false;
                    //var lastOperator = stack.Count > 0 ? stack.Pop() : null;

                    var lastOperator = stack.Count > 0 ? stack.Peek() : null;
                    if (lastOperator != null && (@operator == "+" || @operator == "-") && (lastOperator == "/" || lastOperator == "*"))
                    {
                        stack.Push(input.Substring((i + 1), lastNumberIndex - ((i + 1))));
                        Calculate(stack);
                        stack.Push(@operator);
                        lastNumberIndex = i;

                    }
                    else
                    {
                        stack.Push(input.Substring((i + 1), lastNumberIndex - ((i + 1))));
                        stack.Push(@operator);
                        lastNumberIndex = i;
                    }

                }
            }

            if (lastNumberIndex > 0)
                stack.Push(input.Substring(0, lastNumberIndex));

            if (stack.Count > 0)
                Calculate(stack);

            return stack.Pop();
        }

        static void Calculate(Stack<string> s)
         {
            if (s.Count == 1)
                return;

            if (s.Count < 3)
                throw new ArgumentException();

            var success1 = !decimal.TryParse(s.Pop(), out var firstOperand);
            var @operator = s.Pop();
            var success2 = !decimal.TryParse(s.Pop(), out var seondOperand);

            if (success1 || success2)
                throw new ArgumentException();

            var result = 0.0m;
            if (@operator == "+")
            {
                result = (firstOperand + seondOperand);
            }
            else if (@operator == "-")
            {
                result = (firstOperand - seondOperand);
            }
            else if (@operator == "*")
            {
                result = (firstOperand * seondOperand);
            }
            else if (@operator == "/")
            {
                result = (firstOperand / seondOperand);
            }

            var answer = result.ToString();
            answer = answer.Length > 5 ? answer.Substring(0, 5) : answer;
            s.Push(answer);

            if (s.Count > 1)
                Calculate(s);
        }
    }
}
