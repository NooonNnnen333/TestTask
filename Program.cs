using System.Drawing;
using System.Globalization;

namespace TestTask;

class Program
{
    static void Main(string[] args)
    {
        Control c = new Control();
    }
}

class Control
{
    public Control()
    {
        bool flag = true;
        while (flag)
        {
            Console.WriteLine("Введите номер команды" +
                              "\n1. Ввелите числовой массив с клавиауры" +
                              "\n2. Автогенерация массива" +
                              "\n3. Выход");
            try
            {
                short vibor = Convert.ToInt16(Console.ReadLine());

                switch (vibor)
                {
                    case 1:
                        ReadeFromConsole();
                        Console.Clear();
                        break;
                    case 2:
                        RandomMassive();
                        Console.Clear();
                        break;
                    case 3:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Нет такого варианта.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
            catch (FormatException) // Если введён недоступный символ
            {
                Console.WriteLine("Ввод содержит недопустимые символы.");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }


    // Для ввода с консоли
    private void ReadeFromConsole()
    {
        string textMassive = "";
        while (textMassive == null || textMassive == " " || textMassive == "")
        {
            Console.Write("\nВведите массив через запятую (пример: 1, 2, -11)." +
                          "\nЕсли нужны дробные, то между целой частью и дробной ставьте точку (пример: -33.5, -11.2, 1.5): ");
            textMassive = Console.ReadLine();
            if (textMassive == null || textMassive == " " || textMassive == "")
            {
                Console.WriteLine("Массив пуст. попробуйе ввести ещё раз.");
                Console.ReadLine();
                Console.Clear();
            }
        }

        string result = "";
        if (textMassive.Contains('.'))
        {
            result = SlidDouble(textMassive);
        }
        else
        {
            result = SlidInt(textMassive);
        }
        
        if (result != "")
        {
            Console.WriteLine("Сумма минимальных чисел массива: " + result);
            Console.ReadKey();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Произошла ошибка при подсчёте. Введите данные правильно и попробуйте ещё раз.");
            Console.ResetColor();
            Console.Clear();
        }

    }

    // Для проверки программы на рандомно сгенерированном массиве
    private void RandomMassive()
    {
        int ln = 0;
        string typeM = "";
        bool flag = true;
        while (flag = true)
        {
            try
            {
                Console.Clear();
                Console.Write("\nВведите количество элементов: ");
                ln = Convert.ToInt32(Console.ReadLine());

                Console.Write("\nКакого типа сгенерировать массив? (double/int): ");
                typeM= Console.ReadLine();
                if (typeM != "double" && typeM != "int")
                {
                    Console.WriteLine("Такого типа нет. Введите данные правельно.");
                    Console.ReadKey();
                    continue;
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Неправельный ввод");
                Console.ReadKey();
                Console.ResetColor();
                continue;
            }
            break;
        }

        Console.WriteLine("Сгенерированный массив: ");
        if (typeM == "double")
        {
            double[] doubles = new double[ln];
            
            Random random = new Random();
            double min = -1000.0;
            double max = 1000.0;
            for (int i = 0; i < doubles.Length; i++)
            {
                doubles[i] = random.NextDouble() * (max - min) + min;
                Console.Write($"{doubles[i]} ");
            }

            string result = SummMinElements.sumMin(doubles).ToString();
            Console.WriteLine("Результат: " + result);
            Console.ReadKey();
        }
        
        else if (typeM == "int")
        {
            int[] doubles = new int[ln];
            Random random = new Random();
            for (int i = 0; i < doubles.Length; i++)
            {
                doubles[i] = random.Next(int.MinValue/2, int.MaxValue/2);
                Console.Write($"{doubles[i]} ");
            }

            string result = SummMinElements.sumMin(doubles).ToString();
            Console.WriteLine("Результат: " + result);
            Console.ReadKey();

        }
    }
    
    // Перевод в целочисленный массив
    private string SlidInt(string textMassive)
    {
        char[] separators = new char[] { ',', ' ' }; // Ввёл ' ' на случай, если пользователь перечислил числа через пробел
        string[] stringMassive = textMassive.Split(separators, StringSplitOptions.RemoveEmptyEntries); // Разделение целостной строки ввода на массив

        int[] integerMassive = new int[stringMassive.Length];

        for (int i = 0; i < stringMassive.Length; i++) // Преобразования (в тип инт) и запись в массв с целочисленными числами
        {
            integerMassive[i] = Convert.ToInt32(stringMassive[i]);
        }

        string result = SummMinElements.sumMin(integerMassive).ToString();
        return result;
    }
    
    // Перевод в массив с дробями
    private string SlidDouble(string textMassive)
    {
        char[] separators = new char[] { ',', ' ' }; // Ввёл ' ' на случай, если пользователь перечислил числа через пробел
        string[] stringMassive = textMassive.Split(separators, StringSplitOptions.RemoveEmptyEntries); // Разделение целостной строки ввода на массив
        
        double[] integerMassive = new Double[stringMassive.Length];

        
        for (int i = 0; i < stringMassive.Length; i++) // Преобразования (в тип дабл) и запись в массв с дробными числами
        {
            integerMassive[i] = Double.Parse(stringMassive[i], CultureInfo.InvariantCulture);
        }

        string result = SummMinElements.sumMin(integerMassive).ToString();
        return result;
        
    }
    
    
    
}

static class SummMinElements // Статический класс с методами для нахож-я целочисленных и дробных массивов
{
    

    public static int sumMin(int[] array) // Метод для нахождения суммы минимальных цифр в массиве чисел типа int
    {
        if(array.Length == 1)
        {
            Console.WriteLine("Был введен только один элемент. ");
            return array[0];
        }
        if (array.Length > 0)
        {
            int[] minsArray = {int.MaxValue, int.MaxValue}; // иницилизируем массив с двумя числами, которые м 


            for(int i = 0; i < array.Length; i++)
            {
                
                    if (minsArray[0] > array[i] && array[i] != null)
                    {
                        minsArray[1] = minsArray[0];
                        minsArray[0] = array[i];
                    }
                    else if (minsArray[1] > array[i] && array[i] != null)
                    { 
                        minsArray[1] = array[i];
                    }
                
            }

            return minsArray[0] + minsArray[1];


        }

        
        Console.Error.WriteLine("В массиве нет символов");
        return 0;
    }

    public static double sumMin(double[] array) // Метод для нахождения суммы минимальных цифр в массиве чисел типа double
    {
        if(array.Length == 1)
        {
            Console.WriteLine("Был введен только один элемент. ");
            return array[0];
        }
        if (array.Length > 0)
        {
            double[] minsArray = {double.MaxValue, double.MaxValue}; // иницилизируем массив с двумя числами, которые м 


            for(int i = 0; i < array.Length; i++)
            {
                
                if (minsArray[0] > array[i] && array[i] != null)
                {
                    minsArray[1] = minsArray[0];
                    minsArray[0] = array[i];
                }
                else if (minsArray[1] > array[i] && array[i] != null)
                { 
                    minsArray[1] = array[i];
                }
                
            }

            return minsArray.Sum();


        }
        
        
        Console.Error.WriteLine("В массиве нет символов");
        return 0;
    }
    
}

