using System;
using System.IO;

namespace extinguishers
{
    internal class Program
    {
        static void Main()
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            Console.Title = "Огнетушители";

            string path_current = Environment.CurrentDirectory;
            string path = $"{path_current}\\Огнетушители.txt";

            string[] words;

            try
            {
                using (FileStream fstream = File.OpenRead(path))
                {
                    // преобразуем строку в байты
                    byte[] array = new byte[fstream.Length];
                    // считываем данные
                    fstream.Read(array, 0, array.Length);
                    // декодируем байты в строку
                    string textFromFile = System.Text.Encoding.UTF8.GetString(array);

                    words = textFromFile.Split(new char[] { ';' });
                }
            }
            catch (Exception)
            {
                throw;
            }

            DateTime dateNow = DateTime.Now;
            TimeSpan diff;
            double years;

            for (int i = 0; i < words.Length; i += 2)
            {
                DateTime date;
                try
                {
                    date = DateTime.Parse(words[i].Trim());
                }
                catch (Exception)
                {
                    break;
                }
                diff = dateNow.Subtract(date);
                years = diff.TotalDays / 365.0;

                if (years >= 0 && years <= 1)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else if (years < 5)
                    Console.ForegroundColor = ConsoleColor.Green;
                else  // years > 5
                    Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine(words[i + 1].Trim());
            }

            Console.ForegroundColor = prevColor;
            Console.ReadKey();
        }
    }
}
