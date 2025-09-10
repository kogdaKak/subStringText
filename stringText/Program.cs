using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace stringText
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] text = {"Пример:\r\n" +
                    "( Пгор || Ппик ) && ( (Gпол < 9000 && NY) > Ny_max_9000+0.1 || ( 9000 <= Gпол || Gпол < 10000 ) && NY > Ny_max_10000+0.1 || 10000 <= Gпол && NY > Ny_max_11100+0.1)\r\n" +
                    "Пгор || Ппик\r\n" +
                    "Gпол < 9000 && NY\r\n" +
                    "9000 <= Gпол || Gпол < 10000\r\n" +
                    "(Gпол < 9000 && NY) > Ny_max_9000+0.1 || ( 9000 <= Gпол || Gпол < 10000 ) && NY > Ny_max_10000+0.1 || 10000 <= Gпол && NY > Ny_max_11100+0.1\r\n"};

            TwoResult(text);
            //FirstResult(text);
        }
        /// <summary>
        /// У меня 2 реализованых метода. Зачем? Я своими силами (в основном) написал решение, однако после теста понял что не решил проблему со вложенными "( ) ".
        /// Поискал решение в интернете. Реализовал 2-ой метод. По времени заняло чуть меньше часа.
        /// </summary>
        public static void TwoResult(string[] text)
        {
            List<string> result = new List<string>();
            Stack<StringBuilder> stack = new Stack<StringBuilder>();

            for(int numberLine = 0; numberLine < text.Length; numberLine++)
            {
                if (text == null || string.IsNullOrEmpty(text[numberLine]))
                {
                    Console.WriteLine("Массив пустой");
                    break;
                }

                string line = text[numberLine];
                foreach(char cha in line)
                {
                    if( cha == '(')
                    {
                        if(stack.Count > 0)
                        {
                            stack.Peek().Append('(');
                        }
                        stack.Push(new StringBuilder());
                    }
                    else if( cha == ')')
                    {
                        if(stack.Count > 0)
                        {
                            var inner = stack.Pop();
                            result.Add(inner.ToString());
                            if(stack.Count > 0)
                            {
                                stack.Peek().Append(inner);
                                stack.Peek().Append(')');
                            }
                        }
                    }
                    else
                    {
                        if (stack.Count > 0)
                            stack.Peek().Append(cha);
                    }
                }
            }
            foreach (var res in result)
            {
                Console.WriteLine(res);
            }
        }

        private static void FirstResult(string[] text)
        {
            List<string> result = new List<string>();
            bool _isCharDetected = false;
            List<char> current = new List<char>();
            Stack<StringBuilder> stack = new Stack<StringBuilder>();
            for (int numberLine = 0; numberLine < text.Length; numberLine++)
            {
                if (text == null || string.IsNullOrEmpty(text[numberLine]))
                {
                    Console.WriteLine("Массив пустой");
                    break;
                }
                string oneLine = text[numberLine];
                foreach (char cha in oneLine)
                {
                    if (cha == '(')
                    {
                        _isCharDetected = true;
                        current.Clear();
                    }
                    else if (cha == ')' && _isCharDetected)
                    {
                        result.Add(new string(current.ToArray()));
                        _isCharDetected = false;
                    }
                    else if (_isCharDetected)
                    {
                        current.Add(cha);
                    }
                }

                foreach (var tx in result)
                {
                    Console.WriteLine(tx);
                }
            }
        }
    }
}
