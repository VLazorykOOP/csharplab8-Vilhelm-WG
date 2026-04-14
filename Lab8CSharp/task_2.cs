using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

class task_2
{
    public static void Run()
    {
        string inputPath = "input_2.txt";
        string outputPath = "result_2.txt";

        try
        {
            // 1. Читаємо весь текст із файлу
            string text = File.ReadAllText(inputPath);

            // 2. Регулярний вираз:
            // \b - межа слова
            // (?:[a-eA-E]\w*|\w) - шукаємо:
            //    або слово, що починається на a, b, c, d, e (будь-якого регістру)
            //    або будь-яке слово з однієї літери
            // \b - межа слова
            string pattern = @"\b([a-eA-E]\w*|\w)\b";

            // 3. Замінюємо знайдені слова на порожній рядок
            // Також очищуємо зайві пробіли, що могли залишитися
            string filteredText = Regex.Replace(text, pattern, "");
            filteredText = Regex.Replace(filteredText, @"\s+", " ").Trim();

            // 4. Записуємо результат у новий файл
            File.WriteAllText(outputPath, filteredText);

            Console.WriteLine($"Очищення завершено. Результат збережено у {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }
}