using System;
using System.Linq;
using UnityEngine;

public class Banco_Preguntas_Local : IProveedor_Preguntas
{
    readonly Pregunta[] preguntas;
    readonly System.Random rnd = new System.Random();

    public Banco_Preguntas_Local()
    {
        TextAsset json = Resources.Load<TextAsset>("preguntas");
        preguntas = JsonUtility.FromJson<Lista_Preguntas>(json.text).preguntas;
    }

    public void Obtener(int dificultad, Action<Pregunta> alTerminar)
    {
        
        int mejor = preguntas.Min(p => Math.Abs(p.dificultad - dificultad));
        var candidatas = preguntas
            .Where(p => Math.Abs(p.dificultad - dificultad) <= mejor + 3)
            .ToList();

        alTerminar(candidatas[rnd.Next(candidatas.Count)]);
    }
}