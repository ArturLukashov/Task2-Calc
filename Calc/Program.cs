using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    class Program
    {
        static void Main(string[] args)
        {        
            string result = String.Empty;
            do
            {
                try
                {
                    Console.Clear();
                    Console.Write("Введите первое число:");
                    string first = Console.ReadLine();
                    Console.Write("Введите второе число:");
                    string second = Console.ReadLine();
                    Console.Write("Введите операцию(*,/,+,-):");
                    string number = Console.ReadLine();
                    switch (number)
                    {
                        case "+":
                            result = ownMath.Sum(first, second);
                            Console.WriteLine($"Результат:{result}");
                            break;
                        case "-":
                            result = ownMath.Minus(first, second);
                            Console.WriteLine($"Результат:{result}");
                            break;
                        case "*":
                            result = ownMath.Multiplication(first, second);
                            Console.WriteLine($"Результат:{result}");
                            break;
                        case "/":
                            result = ownMath.Divide(first,second);
                            Console.WriteLine($"Результат:{result}");
                            break;
                        default: Console.WriteLine("Неверная операция"); break;
                    }

                    Console.WriteLine("Для продолжения нажмите любую кнопку, для выхода ESC");
                }
                catch (System.FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Для продолжения нажмите любую кнопку, для выхода ESC");
                }
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);
            Console.ReadLine();         
        }
    }

    public class ownMath
    {
        private static bool checkSign(char sign)
        {
            if (sign == '-')
            {
                return true; 
            }
            else
            {
                return false;
            }
        }
      
        private static bool maxOfTwo(string first, string second)
        {
            if (first.Length < second.Length)
            {
                return true;
            }
            else
            {
                if (first.Length == second.Length)
                {
                    for (int i = 0; i < first.Length; i++)
                    {
                        if (first[i] == second[i])
                        {
                            continue;
                        }
                        if (second[i] > first[i])
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public static string Sum(string first, string second)
        {           
            string final = String.Empty;
            int div = 0;           
            bool firstSign = false;
            bool secondSign = false;
            string temp = String.Empty;
            if (ownMath.checkSign(first[0]) == true)
            {
                first = first.Remove(0, 1);
                firstSign = true;
            }
            if (ownMath.checkSign(second[0]) == true)
            {
                second = second.Remove(0, 1);
                secondSign = true;
            }
            if((firstSign==true && secondSign==false) || (firstSign == false && secondSign == true))
            {
                return ownMath.Minus(first, second);                 
            }
            if (second.Length > first.Length)
            {
                temp = first;
                first = second;
                second = temp;              
            }
            else
            {
                if (first.Length == second.Length)
                {
                    for (int i = 0; i < first.Length; i++)
                    {
                        if (first[i] == second[i])
                        {
                            continue;
                        }
                        if (second[i] > first[i])
                        {
                            temp = first;
                            first = second;
                            second = temp;                      
                            break;
                        }
                    }
                }
            }           
            for (int i = first.Length - 1, j = second.Length - 1; i >= 0 && j >= 0; i--, j--)
            {
                int chislo = Int32.Parse(second[j].ToString()) + Int32.Parse(first[i].ToString());
                if (div > 0)
                {
                    chislo += div;
                }
                int mod = chislo % 10;
                div = chislo / 10;
                final = mod + final;
            }                                              
            int razn = first.Length - second.Length;
            if (razn > 0)         
            {
                for (int i = razn - 1; i >= 0; i--)
                {
                    if (div > 0)
                    {
                        if ((Int32.Parse(first[i].ToString()) + 1) == 10)
                        {
                            final = "0" + final;
                        }
                        else
                        {
                            final = (Int32.Parse(first[i].ToString()) + div) + final;
                            div = 0;                          
                        }
                    }
                    else
                    {
                        final = first[i] + final;
                    }
                }
            }
            if (div > 0)
            {
                final = div + final;
            }
            if (firstSign == true && secondSign == true)
            {
                final = "-" + final; 
            }
            return final;
        }  

        public static string Minus(string first, string second)
        {
            string final = String.Empty;
            int chislo = 0;
            bool flag = false;
            if (second.Length > first.Length)
            {
                string temp = first;
                first = second;
                second = temp;
                flag = true;
            }
            else
            {
                if (first.Length == second.Length)
                {
                    for (int i = 0; i < first.Length; i++)
                    {
                        if (first[i] == second[i])
                        {
                            continue;
                        }
                        if (second[i] > first[i])
                        {
                            string temp = first;
                            first = second;
                            second = temp;
                            flag = true;
                            break;
                        }
                    }
                }
            }
            for (int i = first.Length - 1, j = second.Length - 1; i >= 0 && j >= 0; i--, j--)
            {
                chislo = Int32.Parse(first[i].ToString()) - Int32.Parse(second[j].ToString());
                if (chislo < 0)
                {
                    chislo += 10;
                    int prom = Int32.Parse(first[i-1].ToString()) - 1;
                    if (prom >= 0)
                    {
                        first = first.Remove(i-1, 1).Insert(i-1, prom.ToString());
                    }
                    else
                    {
                        for (int k = i-1; k >= 0; k--)
                        {
                            prom = Int32.Parse(first[k].ToString()) - 1;
                            if (prom >= 0)
                            {
                                first = first.Remove(k, 1).Insert(k, prom.ToString());
                                break;
                            }
                            else
                            {
                                first = first.Remove(k, 1).Insert(k, "9");
                            }
                        }
                    }                  
                }
                final = (chislo % 10) + final;
            }
            int razn = first.Length - second.Length;
            if (razn > 0)
            {
                string temp = String.Empty; 
                for (int i = razn - 1; i >= 0; i--)
                {
                    temp = first[i] + temp; 
                }
                final = temp + final; 
            }
            if (final[0] == '0' && final.Length!=1)
            {
                final = final.Remove(0, 1); 
            }
            if (flag)
            {
                final = "-" + final;
            }            
            return final;
        }

        public static string Multiplication (string first, string second)
        {
            string temp = String.Empty;
            int div = 0;
            int count = 1;                 
            string prom = "0";
            bool firstSign = false;
            bool secondSign = false;
            if (ownMath.checkSign(first[0]) == true)
            {
                first = first.Remove(0, 1);
                firstSign = true;
            }
            if (ownMath.checkSign(second[0]) == true)
            {
                second = second.Remove(0, 1);
                secondSign = true;
            }
            
            for (int i = second.Length - 1; i >= 0; i--)
            {
                for (int j = first.Length - 1; j >= 0; j--)
                {
                    int chislo = Int32.Parse(second[i].ToString()) * Int32.Parse(first[j].ToString());
                    if (div > 0)
                    {                        
                        chislo += div;
                    }
                    int mod = chislo % 10;
                    div = chislo / 10;
                    temp = mod + temp;                   
                }
                for (int k = 0; k < count-1; k++)
                {
                    temp += "0";
                }
                if (div > 0)
                {
                    temp = div + temp;
                    div = 0; 
                }
                prom = ownMath.Sum(temp, prom);
                temp = String.Empty;
                count++;
            }
            if ((firstSign !=secondSign)!=false)
            {
                prom = "-" + prom;
            }
            return prom;           
        }

        public static string Divide(string first,string second)
        {
            if(second.Length > first.Length)
            {
                return "0";
            }          
            string prom = ownMath.Minus(first, second);
            string temp = "1"; 
            if (prom[0] == '-')
            {
                temp = "0";
                return temp;
            }

            while (ownMath.maxOfTwo(prom, second) == false)
            {
                prom = ownMath.Minus(prom, second);
                temp = ownMath.Sum(temp, "1");
            }
            return temp; 

        }
    }
}
