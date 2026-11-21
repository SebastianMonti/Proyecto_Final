using System.Collections.Generic;
using UnityEngine;

public class Enemigo_Fantasma : Enemigo
{
    protected Presentador_Enemigo_Fantasma presentador;

    public Enemigo_Fantasma(
        Celda celdaInicial,
        Tablero tablero,
        Generador_Monedas generadorMonedas,
        Controlador_Entidades controladorEntidades)
    {
        Puntos_de_Vida = 45; // Aparece a partir de ronda 20
        celda_actual = celdaInicial;
        celda_actual.Establecer_Entidad(this);

        this.tablero = tablero;
        this.generador_monedas = generadorMonedas;
        this.controlador_entidades = controladorEntidades;

        presentador = new Presentador_Enemigo_Fantasma(this);
        presentador.dibujar(celda_actual.Obtener_Posicion_X(), celda_actual.Obtener_Posicion_Z());

        Lista_Monedas = new List<Moneda>();
        for (int i = 0; i < 6; i++)
        {
            Lista_Monedas.Add(new Moneda_Cobre());
        }
    }

    public override void Decrementar_Vida(int Daño_Recibido)
    {
        base.Decrementar_Vida(Daño_Recibido);
        Debug.Log($"Enemigo_Fantasma recibió {Daño_Recibido} de daño. Vida actual: {Puntos_de_Vida}");

        if (Puntos_de_Vida <= 0)
        {
            presentador.EliminarEntidad();
        }
    }

    public override void Dibujar(int Pos_X, int Pos_Z)
    {
        presentador.dibujar(Pos_X, Pos_Z);
    }
}