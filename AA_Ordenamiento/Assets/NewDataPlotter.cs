﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NewDataPlotter : MonoBehaviour
{
    // Objetos para graficar
    public GameObject PointPrefab;
    public GameObject gEjeY;        // g es de graficar
    public TextMesh textEjeX;
    public TextMesh textEjeY;
    public Material BlueMaterial;
    public Material RedMaterial;
    public int escala;
    public int rangoY;
    public GameObject cuadricula;
    private int[][] listas;
    


    /*
    Booleanos para determinar que algoritmo de ordenamiento se graficara
    Para este caso, graph1 es el insert sort y graph2 es el bubble sort
    */
    public Boolean graph1;
    public Boolean graph2; 
    
    // atributo que permite establecer la cantidad de pruebas que se realizan
    public int cantPruebas;
    
    // Start is called before the first frame update
    void Start()
    {
        Graph();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    //******************************************* Funciones de generacion de datos *************************************
    
    /*
     * Funcion que retorna un matriz 2xcantPruebas, donde fila superior son los tiempos y la inferior la cantidad
     * de elementos en la lista evaluada
     */ 
    int[,] generarDatosInsertSort()
    {
        int[,] results = new int[2,cantPruebas];  // cantPruebas es el atributo
        int[] tempArray;
        int[] cantElemList = new[] {1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000};

        listas = new int[cantPruebas][];
        
        for (int i = 0; i < cantPruebas; i++)
        {
            int n = Random.Range(0, 10);
            tempArray = generarLista(cantElemList[n]);
            listas[i] = (int[]) tempArray.Clone();
            results[0, i] = getDuracionInsertSort(tempArray);
            results[1, i] = cantElemList[n];
        }

        return results;
    }

    // Funcion que retorna una lista desordenada con la cantidad de elementos ingresada
    int[] generarLista(int cantElem)
    {
        int[] lista = new int[cantElem];
        
        for (int i = 0; i < cantElem; i++)
        {
            lista[i] = Random.Range(0, 100000);
        }

        return lista;
    }
    
    // Funcion que devuelve el tiempo en milisegundos que tarda en ordenar lista ingresada con insert sort
    int getDuracionInsertSort(int[] lista)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        
        // the code that you want to measure comes here
        insertSort(lista);
        watch.Stop();
            
        var elapsedMs = watch.ElapsedMilliseconds;
        
        return int.Parse(elapsedMs.ToString());
    }
    
    // insert sort de GeeksForGeeks
    void insertSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 1; i < n; ++i)
        {
            int key = arr[i];
            int j = i - 1;

            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j = j - 1;
            }

            arr[j + 1] = key;
        }
    }
    
    // Retorna el tiempo mayor de los datos ingresados
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
    
    
    //********************************************* Funciones de graficar **********************************************

    void graphRangeEjeX()
    {
        for (int i = 1; i <= 10; i++)
        {
            var ob = Instantiate(textEjeX, new Vector3((i * 10), -3, -5), Quaternion.identity);
            ob.text = (i * 1000).ToString();

        }
    }

    void graphRangeEjeY(int[,] data)
    {
        int maxTime = findMaxTime(data);
        maxTime = (int)Math.Round((double)maxTime);
        
        // Se grafica la linea del eje Y
        var obEjeY = Instantiate(gEjeY);
        obEjeY.transform.localScale = new Vector3((float) 0.25, (maxTime/escala)+rangoY, 1);
        obEjeY.transform.localPosition = new Vector3((float)-0.25,((maxTime/escala)+rangoY)/2, 0);

        // Se grafica los numeros del eje Y
        for (int i = 1; i <= (maxTime/escala)+rangoY; i++)
        {
            if ((i * escala) % rangoY == 0)
            {
                var obT = Instantiate(textEjeY, new Vector3(-3, i, 5), Quaternion.identity);
                obT.text = ((i * escala)).ToString();
                Instantiate(cuadricula, new Vector3(55, i, 0),Quaternion.identity);
            }
        }
        var nameY = Instantiate(textEjeY, new Vector3(-3, ((maxTime/escala))+(rangoY*2), 5), Quaternion.identity);
        nameY.text = "Ms";
    }

    // Graficar los puntos (bolas) segun la data ingresada
    void graphData(int[,] data,bool color)
    {
        int corX;
        int corY;
        
        for (int i = 0; i < data.GetLength(1); i++)
        {
            corY = data[0, i] /escala;
            corX = data[1, i] / 100;
            var bolita =Instantiate(PointPrefab, new Vector3(corX, corY, 0), Quaternion.identity);
            if (color)
            {
                bolita.GetComponent<MeshRenderer>().material = RedMaterial;
            }
            else
            {
                bolita.GetComponent<MeshRenderer>().material = BlueMaterial;
            }
        }
    }

    // graficar datos segun resultados obtenidos con insert sort
    void graphInsertSort()
    {
        int[,] data = generarDatosInsertSort();
        graphRangeEjeX();
        graphRangeEjeY(data);
        graphData(data, true);
    }

    void Graph()
    {
        if (graph1)
        {
            graphInsertSort();            
        }

        if (graph2)
        {
            graphBubbleSort();
            // aqui debera graficarse data obtenida con bubbleSort
        }
    }
    
    
    // graficar datos segun resultados obtenidos con BubbleSort
    
    void graphBubbleSort()
    {
        int[,] data = generarDatosBubbleSort();
        graphRangeEjeX();
        graphRangeEjeY(data);
        graphData(data,false);
    }
    
    
    // Genera los Datos para el BubbleSort
    
    int[,] generarDatosBubbleSort()
    {
        int[,] results = new int[2,cantPruebas];  // cantPruebas es el atributo
        int[] tempArray;
        int[] cantElemList = new[] {1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000};

        for (int i = 0; i < cantPruebas; i++)
        {
            //int n = Random.Range(0, 10);
            tempArray = listas[i];

            results[0, i] = getDuracionBubbleSort(tempArray);
            results[1, i] = listas[i].Length;
        }

        return results;
    }

    // Funcion que devuelve el tiempo en milisegundos que tarda en ordenar lista ingresada con insert sort
    int getDuracionBubbleSort(int[] lista)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        
        // the code that you want to measure comes here
        BubbleSort(lista);
        watch.Stop();
            
        var elapsedMs = watch.ElapsedMilliseconds;
        
        return int.Parse(elapsedMs.ToString());
    }
    
    //BubbleSort de Geeksforgeeks
     void BubbleSort(int []arr) 
    { 
        int n = arr.Length; 
        for (int i = 0; i < n - 1; i++) 
        for (int j = 0; j < n - i - 1; j++) 
            if (arr[j] > arr[j + 1]) 
            { 
                // swap temp and arr[i] 
                int temp = arr[j]; 
                arr[j] = arr[j + 1]; 
                arr[j + 1] = temp; 
            } 
    } 
    
    
}
