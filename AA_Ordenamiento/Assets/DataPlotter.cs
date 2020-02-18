using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DataPlotter : MonoBehaviour
{
// The prefab for the data points that will be instantiated
    public GameObject PointPrefab;
    public GameObject gEjeY;
    public TextMesh text;
    public TextMesh textEjeY;

    // Use this for initialization
    void Start()
    {
        // Graficar rangos del eje x
        for (int i = 1; i <= 10; i++)
        {
            var ob = Instantiate(text, new Vector3((i * 10), -3, -5), Quaternion.identity);
            ob.text = (i * 1000).ToString();

        }

        int[,] graphInsertSort = generarDatos();

        int corX;
        int corY;
        for (int i = 0; i < graphInsertSort.GetLength(1); i++)
        {
            corY = graphInsertSort[0, i];
            corX = graphInsertSort[1, i] / 100;
            Debug.Log($"X = {graphInsertSort[1, i]} - Y = {corY}");
            Instantiate(PointPrefab, new Vector3(corX, corY, 0), Quaternion.identity);
        }

        // Graficar rangos del eje y
        int maxTime = findMaxTime(graphInsertSort);
        maxTime = (int)Math.Round((double)maxTime);
        var obEjeY = Instantiate(gEjeY);
        obEjeY.transform.localScale = new Vector3((float) 0.25, maxTime, 1);
        obEjeY.transform.localPosition = new Vector3((float)-0.25,maxTime/2, 0);

        for (int i = 1; i <= maxTime/10; i++)
        {
            var obT = Instantiate(textEjeY, new Vector3(-3, i * 10, 5), Quaternion.identity);
            obT.text = (i * 10).ToString();
        }

    }

    int findMaxTime(int[,] matrix)
    {
        int max = matrix[0,0];
        for (int i = 1; i < matrix.GetLength(1); i++)
        {
            if (matrix[0,i] > max)
            {
                max = matrix[0, i];
            }
        }

        return max;
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
        int pruebas = 100;
        int[] tempArray;
        int[,] results = new int[2,pruebas];
        
        for (int i = 0; i < pruebas; i++)
        {
            // se crea un array con un numero aleatorio de elementos
            int[] elementos = new[] {1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000};
            int n = Random.Range(0,10);
            Debug.Log($"Cant elementos = {elementos[n]}");
            tempArray = new int[elementos[n]];
            
            // se rellena el array temporal
            for (int j = 0; j < elementos[n]; j++)
            {
                tempArray[j] = Random.Range(0, 100000);
            }
            
            // Se medira el tiempo que dura en ordenar la lista
            var watch = System.Diagnostics.Stopwatch.StartNew();
        
            // the code that you want to measure comes here
            sort(tempArray);
            watch.Stop();
            
            var elapsedMs = watch.ElapsedMilliseconds;
            Debug.Log($"Tiempo duro = {elapsedMs}");
            results[0,i] = int.Parse(elapsedMs.ToString());
            results[1, i] = elementos[n];
        }
        Debug.Log($"N = {results.GetLength(0)} M = {results.GetLength(1)}");
        return results;
    }
}
