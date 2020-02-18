using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algoritmos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("Original : {2, -65, 34, -23, 3}");
        int[] prueba = {2, -65, 34, -23, 3};
        
        print("Prueba del InsertSort");
        prueba = InsertSort(prueba);
        PrintArray(prueba);
        
        int[] prueba2 = {2, -65, 34, -23, 3};
        print("Prueba del BubbleSort");
        prueba = Bubble_Sort(prueba2);
        PrintArray(prueba);
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int[] Bubble_Sort(int[] array)
    {
        int length = array.Length;

        int temp = array[0];

        for (int i = 0; i < length; i++)
        {
            for (int j = i+1; j < length; j++)
            {
                if (array[i] > array[j])
                {
                    temp = array[i];

                    array[i] = array[j];

                    array[j] = temp;
                }
            }
        }
        return array;
    }
    
    int [] InsertSort(int[] arr) 
    { 
        int n = arr.Length; 
        for (int i = 1; i < n; ++i) { 
            int key = arr[i]; 
            int j = i - 1; 
  
            // Move elements of arr[0..i-1], 
            // that are greater than key, 
            // to one position ahead of 
            // their current position 
            while (j >= 0 && arr[j] > key) { 
                arr[j + 1] = arr[j]; 
                j = j - 1; 
            } 
            arr[j + 1] = key; 
        }

        return arr;
    } 
    

    void PrintArray(int[] array)
    {
        print("Inicio del Array ");
        for (int i = 0; i < array.Length; i++)
        {
            print(array[i]);       
        }
        print("Fin del Array ");
    }
    
}