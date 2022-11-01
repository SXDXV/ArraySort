using System;
using System.IO;
using System.Text;

class consoleUtility
{
    static string filesPath = Environment.CurrentDirectory + "/txts/File";
    static List<int> nums = new List<int>();

    static void Main(string[] args)
    {
        int randomIndicator, countOfLines, rangeOfNumbers, 
            whileIndicator = 0, countOfFiles = 1;

        

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

                    if (randomIndicator == 0)
                    {
                        Console.WriteLine("Введите число от 100 до 1000: ");
                        try
                        {
                            countOfLines = Convert.ToInt32(Console.ReadLine());

                            Random randRange = new Random();
                            rangeOfNumbers = randRange.Next(0, 1000);

                            generate(countOfLines, rangeOfNumbers, countOfFiles);

                            whileIndicator = 1;
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine("Вероятно, вы ввели не число. Попробуйте еще раз.");
                        }

                    }

                    else if (randomIndicator == 1)
                    {
                        Random randCount = new Random();
                        Random randRange = new Random();

                        countOfLines = randCount.Next(100, 1000);
                        rangeOfNumbers = randRange.Next(0, 1000);
                        generate(countOfLines, rangeOfNumbers, countOfFiles);
                        Sorting(nums);
                        for (int i = nums.Count; i>0; i--) Console.WriteLine(nums[i-1]);

                        whileIndicator = 1;
                    }
                }

                catch (FormatException e)
                {
                    Console.WriteLine("Вероятно, вы ввели не число. Попробуйте еще раз.");
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("Вероятно, вы ввели не число. Попробуйте еще раз.");
            }
        }
        Console.ReadLine();
    }

    static void generate(int countOfLin, int range, int countOfFil)
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


            if (File.Exists(filesPath + "demoResult.txt")) 
                File.Delete(filesPath + "demoResult.txt");


            for (int i = 1; i <= countOfFil; i++)
            {
                // Открытие потока и прочтение информации.
                using (StreamReader sr = File.OpenText(filesPath + i + ".txt"))
                {
                    string s = "";

                    if (File.Exists(filesPath + "demoResult.txt"))
                    {
                        while ((s = sr.ReadLine()) != null)
                        {
                            nums.Add(Convert.ToInt32(s));
                            File.AppendAllText(filesPath + "demoResult.txt", s.ToString() + "\n"); 
                        }
                    }
                    else
                    {
                        using (FileStream fs = File.Create(filesPath + "demoResult.txt"))
                        {
                            while ((s = sr.ReadLine()) != null)
                            {
                                nums.Add(Convert.ToInt32(s));
                                byte[] info = new UTF8Encoding(true).GetBytes(s + "\n");
                                fs.Write(info, 0, info.Length);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        Sorting(nums);
    }


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
}
