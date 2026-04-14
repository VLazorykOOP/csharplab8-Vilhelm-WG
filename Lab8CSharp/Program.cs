using BinaryFibonacci;
using FileManipulation;
using HexIpProcessor;
using SymmetricWordsFinder;

namespace ConsoleApp2;
using System;

internal class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Lab 8 - меню");
            Console.WriteLine("5 - Task 5 (FileCombiner)");
            Console.WriteLine("4 - Task 4 (BinaryFibonacci)");
            Console.WriteLine("3 - Task 3 (SymmetricWordsFinder)");
            Console.WriteLine("2 - Task 2 (WordFilter)");
            Console.WriteLine("1 - Task 1 (HexIpProcessor)");
            Console.WriteLine("0 - Вихід");
            Console.Write("Ваш вибір: ");

            string? choice = Console.ReadLine();

            Console.WriteLine();

            switch (choice)
            {
                case "5":
                    task_5.Run();
                    break;
                case "4":
                    task_4.Run();
                    break;
                
                case "3":
                    task_3.Run();
                    break;

                case "2":
                    task_2.Run();
                    break;

                case "1":
                    task_1.Run(args);
                    break;
                

                case "0":
                    return;

                default:
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }
}