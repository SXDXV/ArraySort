using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class consoleUtility
{
    // Универсальный путь для создания и чтения файлов.
    static string filesPath = Environment.CurrentDirectory + "/txts/File";

    // Три листа, хранящие специализированные данные.
    static List<int> nums = new List<int>();
    static List<int> numsUnique = new List<int>();
    static List<int> numsDivision = new List<int>();

    // Финальный стринг, хранящий в себе
    // полный текст для финальной записи в файл.
    static string finalText;


    // Основная Main-функция, точка входа в программу.
    static void Main(string[] args)
    {
        int randomIndicator, countOfLines, rangeOfNumbers,
            whileIndicator = 0, countOfFiles = 1;


        // Создание и поддержка проверок и обработок исключений.
        while (whileIndicator == 0)
        {
            Console.WriteLine("Сколько файлов вы хотите создать: ");
            try
            {
                countOfFiles = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Вы хотите сгенерировать данные случайно? 0 - нет, 1 - да: ");

                try
                {
                    randomIndicator = Convert.ToInt32(Console.ReadLine());

                    // Срабатывает в случае, когда пользователь хочет
                    // кастомизировать настройки заполнения файлов.
                    if (randomIndicator == 0)
                    {
                        Console.WriteLine("Введите число от 100 до 1000: ");
                        try
                        {
                            countOfLines = Convert.ToInt32(Console.ReadLine());

                            Random randRange = new Random();
                            rangeOfNumbers = randRange.Next(0, 1000);

                            // Объявление методов:
                            // генерация каждого из файлов + сортировка;
                            // определение уникальности элементов списка;
                            // поиск элементов, при делении которых на 4 остаток = 3.
                            Generate(countOfLines, rangeOfNumbers, countOfFiles);
                            nums.Reverse();
                            finalText += "\nИсходный список:\n";
                            for (int i = 0; i < nums.Count; i++)
                                finalText += nums[i] + "\n";

                            Unique(nums, numsUnique);
                            finalText += "\n\nУникальные:\n";
                            for (int i = 0; i < numsUnique.Count; i++)
                                finalText += numsUnique[i] + "\n";

                            Division(numsUnique, numsDivision);
                            finalText += "\n\nДелятся на 4 с остатком 3:\n";
                            for (int i = 0; i < numsDivision.Count; i++)
                                finalText += numsDivision[i] + "\n";

                            whileIndicator = 1;
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine("Вероятно, вы ввели не число. Попробуйте еще раз.");
                        }

                    }

                    // Срабатывает в случае, когда пользователь не хочет
                    // кастомизировать настройки заполнения файлов и
                    // доверяется рандомному заполнению даных.
                    else if (randomIndicator == 1)
                    {
                        Random randCount = new Random();
                        Random randRange = new Random();

                        countOfLines = randCount.Next(100, 1000);
                        rangeOfNumbers = randRange.Next(0, 1000);

                        // Объявление методов:
                        // генерация каждого из файлов + сортировка;
                        // определение уникальности элементов списка;
                        // поиск элементов, при делении которых на 4 остаток = 3.
                        Generate(countOfLines, rangeOfNumbers, countOfFiles);
                        nums.Reverse();
                        finalText += "\nИсходный список:\n";
                        for (int i = 0; i < nums.Count; i++)
                            finalText += nums[i] + "\n";

                        Unique(nums, numsUnique);
                        finalText += "\n\nУникальные:\n";
                        for (int i = 0; i < numsUnique.Count; i++)
                            finalText += numsUnique[i] + "\n";

                        Division(numsUnique, numsDivision);
                        finalText += "\n\nДелятся на 4 с остатком 3:\n";
                        for (int i = 0; i < numsDivision.Count; i++)
                            finalText += numsDivision[i] + "\n";

                        whileIndicator = 1;
                    }
                }

                catch (FormatException e)
                {
                    Console.WriteLine("Вероятно, вы ввели не число. Попробуйте еще раз.");
                }

                // Запись в файл выходных данных по итогу работы программы.
                try
                {
                    using (FileStream fs = File.Create(filesPath + "Result.txt"))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(finalText.ToString() + "\n");
                        // Добавление информации в файл.
                        fs.Write(info, 0, info.Length);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("Вероятно, вы ввели не число. Попробуйте еще раз.");
            }
        }
        Console.ReadLine();
    }


    // Метод генерации случайных значений
    static void Generate(int countOfLin, int range, int countOfFil)
    {
        int lineCheker, number;

        try
        {
            for (int i = 1; i <= countOfFil; i++)
            {
                lineCheker = 0;

                // Создание файла и его перезапись в случае существования.
                using (FileStream fs = File.Create(filesPath + i + ".txt"))
                {
                    while (lineCheker < countOfLin)
                    {
                        Random rand = new Random();
                        number = rand.Next(range + 1);

                        byte[] info = new UTF8Encoding(true).GetBytes(number.ToString() + "\n");

                        // Добавление информации в файл.
                        fs.Write(info, 0, info.Length);

                        lineCheker++;
                    }
                }
            }


            for (int i = 1; i <= countOfFil; i++)
            {
                // Открытие потока и прочтение информации.
                using (StreamReader sr = File.OpenText(filesPath + i + ".txt"))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        nums.Add(Convert.ToInt32(s));
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        // Автоматическая сортировка элементов (по возрастанию)
        Sorting(nums);
    }


    // Метод сортировки полученных элементов (по дефолту - возрастание)
    public static List<int> Sorting(List<int> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            int min = i;
            for (int j = i + 1; j < list.Count; j++)
            {
                if (list[j] < list[min])
                {
                    min = j;
                }
            }
            int dummy = list[i];
            list[i] = list[min];
            list[min] = dummy;
        }
        return list;
    }


    // Метод поиска уникальных значений в общей массе
    public static List<int> Unique(List<int> list, List<int> listNew)
    {
        try
        {
            IEnumerable<int> distinctList = list.AsQueryable().Distinct();
            foreach (int num in distinctList)
                listNew.Add(num);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return listNew;
    }


    // Поиск элементов, способных при делении на 4 оставить в остатке 3
    public static List<int> Division(List<int> list, List<int> listNew)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i]%4==3) listNew.Add(list[i]);
        }

        return listNew;
    }
}