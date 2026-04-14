using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace HexIpProcessor
{
    class task_1
    {
        public static void Run(string[] args)
        {
            // Назви файлів
            string inputFile = "input.txt";
            string extractedIpsFile = "extracted_ips.txt";
            string outputFile = "output.txt";

            // Створюємо тестовий файл, якщо його не існує (для зручності перевірки)
            CreateSampleFileIfNotExists(inputFile);

            Console.WriteLine("=== Програма обробки шістнадцяткових IP-адрес ===\n");

            // 1. Зчитування тексту з файлу
            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"Помилка: Файл {inputFile} не знайдено.");
                return;
            }

            string text = File.ReadAllText(inputFile);
            Console.WriteLine($"--- Початковий текст з {inputFile} ---");
            Console.WriteLine(text);
            Console.WriteLine("--------------------------------------\n");

            // 2. Пошук IP-адрес за допомогою Regex
            // Шаблон: від 1 до 2 hex-символів, крапка (3 рази), і ще 1-2 hex-символи. \b - межі слова.
            string pattern = @"\b(?:[0-9A-Fa-f]{1,2}\.){3}[0-9A-Fa-f]{1,2}\b";
            MatchCollection matches = Regex.Matches(text, pattern);

            // 3. Підрахунок кількості
            int ipCount = matches.Count;
            Console.WriteLine($"Знайдено шістнадцяткових IP-адрес: {ipCount}");

            if (ipCount == 0)
            {
                Console.WriteLine("IP-адрес не знайдено. Програма завершує роботу.");
                return;
            }

            // 4. Запис знайдених IP у новий файл
            var extractedIps = matches.Cast<Match>().Select(m => m.Value).Distinct();
            File.WriteAllLines(extractedIpsFile, extractedIps);
            Console.WriteLine($"Унікальні IP-адреси збережено у файл: {extractedIpsFile}\n");

            // 5. Запит параметрів у користувача для заміни/вилучення
            Console.WriteLine("--- Налаштування заміни/вилучення ---");
            Console.Write("Введіть IP-адресу зі знайдених, яку потрібно замінити або вилучити: ");
            string targetIp = Console.ReadLine();

            Console.Write("Введіть текст для заміни (залиште порожнім, щоб просто вилучити): ");
            string replacementText = Console.ReadLine();

            // 6. Вилучення/заміна
            // Використовуємо Regex.Escape для безпечної заміни тексту, який містить крапки
            string updatedText = Regex.Replace(text, $@"\b{Regex.Escape(targetIp)}\b", replacementText);

            // 7. Запис оновленого тексту у новий файл
            File.WriteAllText(outputFile, updatedText);
            Console.WriteLine($"\nГотово! Оновлений текст збережено у файл: {outputFile}");
            Console.WriteLine("\n--- Результат ---");
            Console.WriteLine(updatedText);
        }

        // Допоміжний метод для створення тестового файлу
        static void CreateSampleFileIfNotExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                string sampleContent = "Це тестове повідомлення. Сервер має адресу A.1B.C3.FF, " +
                                       "а резервний комп'ютер доступний за 0.0.0.0. " +
                                       "Некоректні формати типу GG.1.1.1 або 1.1.1 пропускаються. " +
                                       "Ще одна адреса: FF.FF.FF.FF в кінці речення.";
                File.WriteAllText(filePath, sampleContent);
            }
        }
    }
}