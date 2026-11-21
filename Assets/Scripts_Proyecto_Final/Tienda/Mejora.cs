using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public abstract class Mejora
{
    public abstract string Nombre { get; }
    protected abstract int Precio_Base { get; }

    public int Nivel { get; private set; }

    // Precio exponencial: sube 50% con cada compra de ESTA mejora
    public int Precio => (int)Math.Round(Precio_Base * Math.Pow(1.5, Nivel));

    public void Subir_Nivel(Estadisticas stats)
    {
        Nivel++;
        Aplicar_Efecto(stats);
    }

    // Para cargar una partida guardada: reaplica los niveles comprados
    public void Restaurar(int nivel, Estadisticas stats)
    {
        for (int i = 0; i < nivel; i++) Subir_Nivel(stats);
    }

    // Lo que cambia UN nivel de esta mejora
    protected abstract void Aplicar_Efecto(Estadisticas stats);
}