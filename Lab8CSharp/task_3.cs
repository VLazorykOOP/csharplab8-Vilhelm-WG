using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SymmetricWordsFinder
{
    class task_3
    {
        public static void Run()
        {
            string inputFile = "input_3.txt";
            string outputFile = "output_3.txt";

            // Створюємо тестовий файл, якщо його не існує
            CreateSampleFile(inputFile);

            try
            {
                // 1. Читаємо текст із файлу
                string text = File.ReadAllText(inputFile);
                Console.WriteLine("--- Вхідний текст ---");
                Console.WriteLine(text);
                Console.WriteLine("---------------------\n");

                // 2. Витягуємо всі слова з тексту
                // \p{L}+ означає будь-яку послідовність літер (підтримує українську мову)
                var words = Regex.Matches(text, @"\p{L}+")
                                 .Cast<Match>()
                                 .Select(m => m.Value);

                // 3. Відбираємо симетричні слова
                // Додано умову Length > 1, щоб не вважати однобуквені слова (наприклад, 'і', 'в', 'а') симетричними
                var symmetricWords = words.Where(w => w.Length > 1 && IsSymmetric(w))
                                          .Distinct(StringComparer.OrdinalIgnoreCase) // Відкидаємо дублікати
                                          .ToList();

                // 4. Виводимо результат на екран
                Console.WriteLine("Знайдені симетричні слова:");
                if (symmetricWords.Count > 0)
                {
                    foreach (var word in symmetricWords)
                    {
                        Console.WriteLine("- " + word);
                    }
                }
                else
                {
                    Console.WriteLine("Симетричних слів не знайдено.");
                }

                // 5. Записуємо результат у вихідний файл
                File.WriteAllLines(outputFile, symmetricWords);
                Console.WriteLine($"\nРезультат успішно записано у файл: {outputFile}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Сталася помилка: {ex.Message}");
            }
        }

        // Метод для перевірки, чи є слово симетричним
        static bool IsSymmetric(string word)
        {
            string lowerWord = word.ToLower(); // Переводимо в нижній регістр для коректного порівняння
            int left = 0;
            int right = lowerWord.Length - 1;

            while (left < right)
            {
                if (lowerWord[left] != lowerWord[right])
                {
                    return false; // Якщо літери не збігаються - слово не симетричне
                }
                left++;
                right--;
            }
            return true;
        }

        // Допоміжний метод для створення файлу з тестовими даними
        static void CreateSampleFile(string path)
        {
            if (!File.Exists(path))
            {
                string sampleText = "Сьогодні був сильний потоп. Мій дід сказав, що його око бачило багато такого. " +
                                    "Радар показав наближення бурі. До речі, слово абввба — це просто вигаданий набір літер, " +
                                    "але воно симетричне! Шалаш також підходить.";
                File.WriteAllText(path, sampleText);
            }
        }
    }
}