

class MergeSort
{
    static void Main(string[] args)
    {
        int size = 10000; // Dimensiunea vectorului
        int[] arr = new int[size];

        Random rand = new Random();

        for (int i = 0; i < size; i++)
        {
            arr[i] = rand.Next(10000); // Numere întregi aleatorii în intervalul [0, 999999]
        }

        Console.WriteLine("Vectorul generat aleatoriu: ");
        PrintArray(arr);

        SequentialMergeSort(arr, 0, arr.Length - 1);

        Console.WriteLine("Vectorul sortat: ");
        PrintArray(arr);
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

    static void PrintArray(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write("{0} ", arr[i]);
        }
        Console.WriteLine();
    }
}