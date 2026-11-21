using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Cargador_Rondas
{
    const int RONDA_FINAL = 50;
    const int MAX_HORDA = 25;

    readonly Fabrica_Enemigos fabrica;
    readonly Tablero tablero;

    public Cargador_Rondas(Fabrica_Enemigos fabrica, Tablero tablero)
    {
        this.fabrica = fabrica;
        this.tablero = tablero;
    }

    public DatosRonda ObtenerDatosRonda(int n)
    {
        int cantidad = Math.Min(MAX_HORDA, 3 + n / 2);
        int tiposDisponibles = Math.Min(fabrica.CantidadTipos, 1 + (n - 1) / 5);
        float multVida = 1f + 0.05f * (n - 1);

        var horda = new List<Enemigo>();
        for (int i = 0; i < cantidad; i++)
        {
            List <Celda>  lista = tablero.Obtener_Celdas_Libres();

            if (lista == null) break;   // tablero lleno
            System.Random rnd = new System.Random();
            int indiceAleatorio = rnd.Next(lista.Count);
            Celda celda_random = lista[indiceAleatorio];

            Enemigo e = fabrica.Crear(i % tiposDisponibles, celda_random);
            horda.Add(e);
        }

        bool impuesto = n % 5 == 0;
        return new DatosRonda
        {
            Numero = n,
            CantidadHorda = horda.Count,
            EsRondaDeImpuesto = impuesto,
            Impuesto = impuesto ? CalcularImpuesto(n / 5) : 0,
            horda = horda
        };
    }

    public static int CalcularImpuesto(int k) =>
        (int)Math.Round(100 * Math.Pow(1.6, k - 1));

    public bool EsUltimaRonda(int n) => n >= RONDA_FINAL;
}

