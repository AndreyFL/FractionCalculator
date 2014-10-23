using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractionCalculator
{
    class SimpleFraction
    {
        int numerator;
        int denominator;

        public SimpleFraction(int numerator = 1, int denominator = 1)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }

        public override string ToString()
        {
            int integerPart = 0;
            int numerator = this.numerator;
            int denominator = this.denominator;

            // Проверяю, если в результате деления в знаменателе отрицательное число, привожу его к положительному
            if (denominator < 0)
            {
                denominator *= -1;
                numerator *= -1;
            }

            // Проверяю есть ли целая составляющая
            if (Math.Abs(this.numerator) >= Math.Abs(this.denominator))
            {
                integerPart = this.numerator / this.denominator;
                numerator = Math.Abs(this.numerator % this.denominator);
            }

            // Сокращаю дробь, если это возможно
            for (int i = Math.Abs(numerator); i > 1; i--)
            {
                if (numerator % i == 0 && denominator % i == 0)
                {
                    numerator /= i;
                    denominator /= i;
                    break;
                }
            }


            // Формирую строку для вывода
            string tempStr = "";
            if (integerPart != 0)
                tempStr = string.Format("{0} ", integerPart);
            if (numerator != 0)
                tempStr += string.Format("{0}/{1}", numerator, denominator);
            else if (integerPart == 0 && numerator == 0)
                tempStr = "0";
            return tempStr;
        }

        // Метод выполняющий суммирование 2-х дробных чисел, результат будет в объекте, для которого этот метод вызывается.
        public void FractionSumm(SimpleFraction addedFrac)
        {
            // если знаменатели не равны, привожу к общему знаменателю
            if (this.denominator != addedFrac.denominator)
            {
                this.numerator = this.denominator * addedFrac.numerator + this.numerator * addedFrac.denominator;
                this.denominator = this.denominator * addedFrac.denominator;
            }
            else
                this.numerator = this.numerator + addedFrac.numerator;
        }

        // Метод вычисляющий разность 2-х дробных чисел, результат будет в объекте, для которого этот метод вызывается.
        public void FractionSubtraction(SimpleFraction subtractedFrac)
        {
            if (this.denominator == subtractedFrac.denominator)
                this.numerator -= subtractedFrac.numerator;
            else
            {
                this.numerator = this.numerator * subtractedFrac.denominator - this.denominator * subtractedFrac.numerator;
                this.denominator = this.denominator * subtractedFrac.denominator;
            }
        }

        // Метод производящий умножение дроби которая хранится в объекте, на дробь которая передается в виде параметра,
        // результат будет записан в объект для которого этот метод вызван.
        public void FractionMultipl(SimpleFraction multFrac)
        {
            this.numerator *= multFrac.numerator;
            this.denominator *= multFrac.denominator;
        }

        public void FractionDivision(SimpleFraction divFrac)
        {
            this.numerator *= divFrac.denominator;
            this.denominator *= divFrac.numerator;
        }
    }

    class Program
    {
        // Генерация нового объекта дроби на основании введенных данных в консоли.
        static SimpleFraction GetNewFraction()
        {
            int numerator;
            int denominator;
            bool successFlag = true;
            do
            {
                try
                {
                    Console.WriteLine("Введите числитель:");
                    numerator = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введите знаменатель:");
                    denominator = Convert.ToInt32(Console.ReadLine());
                    if (denominator == 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        successFlag = true;
                        return new SimpleFraction(numerator, denominator);
                    }
                }
                catch
                {
                    Console.WriteLine("Ошибочное выражение!");
                    successFlag = false;
                }
            } while (successFlag != true);

            return new SimpleFraction();
        }

        static void Main(string[] args)
        {
            char key;

            SimpleFraction fraction1 = GetNewFraction();

            do
            {
                Console.WriteLine("\nВыберите варант:\n'+' - суммировать\n'-' - вычесть\n'*' - умножить\n'/' - разделить\n'q' - выход");
                try
                {
                    key = Convert.ToChar(Console.ReadLine());
                }
                catch
                {
                    key = 'q';
                }
                switch (key)
                {
                    case '+':
                        {
                            SimpleFraction fraction2 = GetNewFraction();
                            Console.Write("{0} + {1} = ", fraction1, fraction2);
                            fraction1.FractionSumm(fraction2);
                            Console.WriteLine(fraction1);
                            break;
                        }
                    case '-':
                        {
                            SimpleFraction fraction2 = GetNewFraction();
                            Console.Write("{0} - {1} = ", fraction1, fraction2);
                            fraction1.FractionSubtraction(fraction2);
                            Console.WriteLine(fraction1);
                            break;
                        }

                    case '*':
                        {
                            SimpleFraction fraction2 = GetNewFraction();
                            Console.Write("{0} * {1} = ", fraction1, fraction2);
                            fraction1.FractionMultipl(fraction2);
                            Console.WriteLine(fraction1);
                            break;
                        }
                    case '/':
                        {
                            SimpleFraction fraction2 = GetNewFraction();
                            Console.Write("{0} : {1} = ", fraction1, fraction2);
                            fraction1.FractionDivision(fraction2);
                            Console.WriteLine(fraction1);
                            break;
                        }
                    default:
                        key = 'q';
                        break;
                }
            } while (key != 'q' && key != 'Q');
            Console.ReadKey();
        }
    }
}