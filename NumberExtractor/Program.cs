using System;
using System.IO;
using NumberExtractor.Entities;
namespace SeuNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            ////C:\Users\crist\Downloads\contacts.txt
            ////C:\Users\crist\OneDrive\Área de Trabalho\txtTeste.txt


            Console.Write("Enter the file name and extension: ");
            string name = Console.ReadLine();
            Console.Write("Enter the source path and file name(with extension): ");
            string source = Console.ReadLine();
            Console.Write("Enter the path where file will be created: ");
            string localDestination = Console.ReadLine() + "/" + name;
            try
            {
                string[] lines = File.ReadAllLines(source);

                string[] allNumbers = Extract.FindNumber(lines);

                if (File.Exists(localDestination))
                {
                    Console.WriteLine("\nTHE FILE ALREADY EXISTS!!!");
                    string content = File.ReadAllText(localDestination);
                    Console.WriteLine("FILE CONTENT: \n" + content);
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(localDestination))
                    {
                        int indice = 1;
                        foreach (string number in allNumbers)
                        {
                            sw.WriteLine($"CONTATO {indice.ToString("D2")}:, {number}");
                            indice++;
                        }
                    }
                    Console.WriteLine("\nFILE CREATED SUCCESSFULLY");
                }
            }
            catch (SystemException e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
            Console.WriteLine("Press the enter button to finish... ");

            ConsoleKeyInfo keyinfo = Console.ReadKey(intercept: true);
            while (keyinfo.Key != ConsoleKey.Enter)
            {
                keyinfo = Console.ReadKey(intercept: true);
            }

        }
    }
}
