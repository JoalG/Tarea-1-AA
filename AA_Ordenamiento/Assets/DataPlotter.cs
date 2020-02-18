using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlotter : MonoBehaviour
{
// The prefab for the data points that will be instantiated
    public GameObject PointPrefab;
    public GameObject gEjeY;
    public GameObject gEjeX;

    // Use this for initialization
    void Start()
    {

        
        int[,] graphInsertSort = generarDatos();

        int corX;
        int corY;
        for (int i = 0; i < graphInsertSort.GetLength(1); i++)
        {
            corX = graphInsertSort[0, i];
            corY = Random.Range(1, 11);
            Instantiate(PointPrefab, new Vector3(corX, corY, 0), Quaternion.identity);
        }

    }

    static void printArray(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n; ++i)
            Debug.Log(arr[i] + " ");

        Debug.Log("\n");
    }

    void sort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 1; i < n; ++i)
        {
            int key = arr[i];
            int j = i - 1;

            // Move elements of arr[0..i-1], 
            // that are greater than key, 
            // to one position ahead of 
            // their current position 
            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j = j - 1;
            }

            arr[j + 1] = key;
        }
    }

    int[,] generarDatos()
    {
        int pruebas = 10;
        int[] tempArray;
        int[,] results = new int[2,pruebas];
        
            for (int i = 0; i < pruebas; i++)
        {
            // se crea un array con un numero aleatorio de elementos
            int n = Random.Range(1000, 10000);
            tempArray = new int[n];
            
            // se rellena el array temporal
            for (int j = 0; j < n; j++)
            {
                tempArray[j] = Random.Range(0, 100000);
            }
            
            // Se medira el tiempo que dura en ordenar la lista
            var watch = System.Diagnostics.Stopwatch.StartNew();
        
            // the code that you want to measure comes here
            sort(tempArray);
            watch.Stop();
            
            var elapsedMs = watch.ElapsedMilliseconds;
            results[0,i] = int.Parse(elapsedMs.ToString());
            results[1, i] = n;
        }

        return results;
    }
}
