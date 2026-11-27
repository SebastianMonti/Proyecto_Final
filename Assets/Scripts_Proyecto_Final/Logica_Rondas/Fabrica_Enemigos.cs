using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Fabrica_Enemigos
{
    readonly List<Func<Celda, Enemigo>> creadores;

    public int CantidadTipos => creadores.Count;

    public Fabrica_Enemigos(Tablero t, Generador_Monedas g, Controlador_Entidades c,Personaje p)
    {
        creadores = new List<Func<Celda, Enemigo>>
        {
            celda => new Enemigo_Alfil(celda,t,g,c),
            celda => new Enemigo_Basico(celda, t, g, c),
            celda => new Enemigo_Evasivo(celda, t, g, c, p),
            celda => new Enemigo_Avaro(celda, t, g, c),
            celda => new Enemigo_Tanque(celda, t, g, c),
            celda => new Enemigo_Fantasma(celda, t, g, c),
            celda => new Enemigo_Alfil(celda,t,g,c)

        };
    }

    public Enemigo Crear(int indiceTipo, Celda celda) => creadores[indiceTipo](celda);
}

