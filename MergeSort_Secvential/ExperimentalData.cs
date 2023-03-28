using System;
using System.IO;

class DataGenerator
{

    static string directoryPath_Inputs = @"../../../ExperimentalData/Inputs";
    static string directoryPath_Outputs = @"../../../ExperimentalData/Outputs";

    public void Generate()
    {
        // Dimensiunile vectorilor și limitele pentru numerele aleatoare
        int[] sizes = { 100, 1000, 10000, 100000, 1000000 };
        int[] limits = { 100, 1000, 10000, 100000, 1000000};

        // Generează fișierele de intrare
        for (int i = 0; i < sizes.Length; i++)
        {
            int[] arr = new int[sizes[i]];
            Random rand = new Random();
            for (int j = 0; j < sizes[i]; j++)
            {
                arr[j] = rand.Next(limits[i]) + (i > 0 ? limits[i - 1] : 0);
            }
            string inputFilePath = Path.Combine(directoryPath_Inputs, $"input_{sizes[i]}.txt");
            using (StreamWriter writer = new StreamWriter(inputFilePath))
            {
                foreach (int num in arr)
                {
                    writer.WriteLine(num);
                }
            }

            Console.WriteLine($"Fisierul {inputFilePath} a fost generat.");
        }

        // Generează fișierele de ieșire (goale)
        for (int i = 0; i < sizes.Length; i++)
        {
            string outputFilePath = Path.Combine(directoryPath_Outputs, $"output_{sizes[i]}.txt");
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                writer.Write("");
            }

            Console.WriteLine($"Fisierul {outputFilePath} a fost generat.");
        }
    }
}