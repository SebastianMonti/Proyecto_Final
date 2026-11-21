using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public class Pregunta
{
    public string texto;
    public string[] opciones;
    public int correcta;      // índice dentro de opciones
    public int dificultad;    // 1 a 50
}

[Serializable]
public class Lista_Preguntas
{
    public Pregunta[] preguntas;
}

