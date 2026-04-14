using System;
using System.IO;

namespace FileManipulation
{
    class task_5
    {
        public static void Run()
        {
            // Назва папки згідно з вашим прізвищем
            string folderName = "Гайсюк_2";
            string t1Path = "output.txt";
            string t2Path = "result_2.txt";
            string t3Path = Path.Combine(folderName, "t3.txt");

            try
            {
                // 1. Створюємо папку, якщо її не існує
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                    Console.WriteLine($"Папку '{folderName}' створено.");
                }

                // 2. Перевіряємо наявність вхідних файлів
                // Створимо їх для прикладу, якщо вони відсутні
                if (!File.Exists(t1Path)) File.WriteAllText(t1Path, "Контент із файлу t1.\n");
                if (!File.Exists(t2Path)) File.WriteAllText(t2Path, "Контент із файлу t2.\n");

                // 3. Зчитуємо дані з обох файлів
                string content1 = File.ReadAllText(t1Path);
                string content2 = File.ReadAllText(t2Path);

                // 4. Записуємо спочатку перший контент, а потім додаємо другий
                // File.WriteAllText перезаписує файл (створює новий)
                File.WriteAllText(t3Path, content1);
                
                // File.AppendAllText додає текст у кінець існуючого файлу
                File.AppendAllText(t3Path, content2);

                Console.WriteLine($"Дані успішно переписані у файл: {t3Path}");
                
                // Виведемо результат на екран для перевірки
                Console.WriteLine("\nВміст файлу t3.txt:");
                Console.WriteLine(File.ReadAllText(t3Path));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Сталася помилка: {ex.Message}");
            }
        }
    }
}