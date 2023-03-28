using System;
using System.Diagnostics;

class MergeSort
{
    static string directoryPath_Inputs = @"../../../ExperimentalData/Inputs";
    static string directoryPath_Outputs = @"../../../ExperimentalData/Outputs";
    static void Main(string[] args)
    {
        string[] inputFiles = { "input_100.txt", "input_1000.txt", "input_10000.txt", "input_100000.txt", "input_1000000.txt" };
        string[] outputFiles = { "output_100.txt", "output_1000.txt", "output_10000.txt", "output_100000.txt", "output_1000000.txt" };

        SortFiles(inputFiles, outputFiles);
    }

    static void SortFiles(string[] inputFiles, string[] outputFiles)
    {
        Console.WriteLine("Logs:");
        Console.WriteLine("File\tData Set\tDuration (ms)");

        int[][] arrs = new int[inputFiles.Length][];
        long[] durations = new long[inputFiles.Length];

        for (int i = 0; i < inputFiles.Length; i++)
        {
            string inputFile = Path.Combine(directoryPath_Inputs, inputFiles[i]);
            string outputFile = Path.Combine(directoryPath_Outputs, outputFiles[i]);

            if (!File.Exists(inputFile) || !File.Exists(outputFile))
            {
                // Fisierele nu exista, deci le generam
                DataGenerator dataGenerator = new DataGenerator();
                dataGenerator.Generate();
            }

            // Citim datele din fisierul de intrare
            arrs[i] = File.ReadAllLines(inputFile).Select(int.Parse).ToArray();

            // Sortam datele si masuram timpul de sortare
            Stopwatch stopwatch = Stopwatch.StartNew();
            SequentialMergeSort(arrs[i], 0, arrs[i].Length - 1);
            stopwatch.Stop();
            durations[i] = stopwatch.ElapsedMilliseconds;

            // Scriem datele sortate in fisierul de iesire
            File.WriteAllLines(outputFile, arrs[i].Select(n => n.ToString()).ToArray());

            Console.WriteLine($"{i + 1}\t{inputFiles[i].Replace("input_", "").Replace(".txt", "")}\t{durations[i]}");
        }

        Console.WriteLine("\nComparison Table:");
        Console.WriteLine("DataSet\tAscending (ms)\tDescending (ms)\tRandom (ms)");

        int[] dataSets = { 100, 1000, 10000, 100000, 1000000 };

        for (int i = 0; i < dataSets.Length; i++)
        {
            Console.Write($"{dataSets[i]}\t");

            for (int j = 0; j < 4; j++)
            {
                int index = Array.IndexOf(inputFiles, $"input_{dataSets[i]}.txt");
                Console.Write($"{durations[index]}\t");
            }

            Console.WriteLine();
        }
    }

    static void SequentialMergeSort(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int mid = (left + right) / 2;
            SequentialMergeSort(arr, left, mid);
            SequentialMergeSort(arr, mid + 1, right);
            Merge(arr, left, mid, right);
        }
    }

    static void Merge(int[] arr, int left, int mid, int right)
    {
        int[] temp = new int[arr.Length];
        int i, j, k;

        i = left;
        j = mid + 1;
        k = left;

        while (i <= mid && j <= right)
        {
            if (arr[i] <= arr[j])
            {
                temp[k++] = arr[i++];
            }
            else
            {
                temp[k++] = arr[j++];
            }
        }

        while (i <= mid)
        {
            temp[k++] = arr[i++];
        }

        while (j <= right)
        {
            temp[k++] = arr[j++];
        }

        for (i = left; i <= right; i++)
        {
            arr[i] = temp[i];
        }
    }
}