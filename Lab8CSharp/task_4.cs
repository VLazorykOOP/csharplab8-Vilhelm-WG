using System;
using System.IO;

namespace BinaryFibonacci
{
    class task_4
    {
        public static void Run()
        {
            Console.WriteLine("=== Робота з двійковим файлом: Послідовність Фібоначчі ===");
            
            // Запитуємо у користувача кількість чисел
            Console.Write("Введіть кількість чисел Фібоначчі (n): ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                Console.WriteLine("Помилка: Будь ласка, введіть додатне ціле число.");
                return;
            }

            string filePath = "fibonacci.dat";

            // 1. Формування послідовності та запис у двійковий файл
            // Використовуємо long, оскільки числа Фібоначчі дуже швидко зростають
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                long a = 0;
                long b = 1;

                for (int i = 0; i < n; i++)
                {
                    writer.Write(a); // Записуємо число у двійковому форматі (8 байтів для long)
                    
                    // Обчислюємо наступне число Фібоначчі
                    long next = a + b;
                    a = b;
                    b = next;
                }
            }
            Console.WriteLine($"\n[Успішно] {n} перших чисел Фібоначчі записано у файл '{filePath}'.");

            // 2. Читання з двійкового файлу та фільтрація
            Console.WriteLine("\n--- Компоненти файлу (порядковий номер НЕ кратний 3) ---");
            
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не знайдено.");
                return;
            }

            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                int position = 1; // Порядковий номер (починаємо лічбу з 1)

                // Читаємо файл, поки поточна позиція вказівника не досягне кінця файлу
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    // Зчитуємо 8 байтів і перетворюємо їх назад у число типу long
                    long number = reader.ReadInt64();

                    // Перевіряємо умову: порядковий номер не кратний 3
                    if (position % 3 != 0)
                    {
                        Console.WriteLine($"Позиція {position}: {number}");
                    }
                    
                    position++;
                }
            }
            
            Console.WriteLine("--------------------------------------------------------");
        }
    }
}