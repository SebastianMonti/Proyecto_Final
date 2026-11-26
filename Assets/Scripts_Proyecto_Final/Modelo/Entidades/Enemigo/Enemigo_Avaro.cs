using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemigo_Avaro : Enemigo
{
    protected Presentador_Enemigo_Avaro presentador;

    public Enemigo_Avaro(
        Celda celdaInicial,
        Tablero tablero,
        Generador_Monedas generadorMonedas,
        Controlador_Entidades controladorEntidades)
    {
        Puntos_de_Vida = 35; // Más resistente al aparecer recién en ronda 11
        celda_actual = celdaInicial;
        celda_actual.Establecer_Entidad(this);

        this.tablero = tablero;
        this.generador_monedas = generadorMonedas;
        this.controlador_entidades = controladorEntidades;

        presentador = new Presentador_Enemigo_Avaro(this);
        presentador.dibujar(celda_actual.Obtener_Posicion_X(), celda_actual.Obtener_Posicion_Z());

        Lista_Monedas = new List<Moneda>();
        for (int i = 0; i < 3; i++)
        {
            Lista_Monedas.Add(new Moneda_Cobre());
            Lista_Monedas.Add(new Moneda_Plata());
        }
    }

    public override Celda Mover()
    {
        Celda celdaMoneda = tablero.Obtener_Celda_Moneda_Mas_Cercana(celda_actual);

        // Si no hay monedas disponibles en el mapa, se mueve aleatorio como el básico
        if (celdaMoneda == null)
        {
            return base.Mover();
        }

        List<Celda> vecinas = tablero.Obtener_Celdas_Vecinas(celda_actual);

        if (vecinas != null && vecinas.Count > 0)
        {
            int mx = celdaMoneda.Obtener_Posicion_X();
            int mz = celdaMoneda.Obtener_Posicion_Z();

            int menorDistancia = int.MaxValue;
            List<Celda> mejoresCeldas = new List<Celda>();

            foreach (Celda candidata in vecinas)
            {
                if (candidata.Hay_Enemigo())
                {
                    continue;
                }

                int distancia = Math.Abs(candidata.Obtener_Posicion_X() - mx)
                              + Math.Abs(candidata.Obtener_Posicion_Z() - mz);

                if (distancia < menorDistancia)
                {
                    menorDistancia = distancia;
                    mejoresCeldas.Clear();
                    mejoresCeldas.Add(candidata);
                }
                else if (distancia == menorDistancia)
                {
                    mejoresCeldas.Add(candidata);
                }
            }

            if (mejoresCeldas.Count > 0)
            {
                Celda destino = mejoresCeldas[rnd.Next(mejoresCeldas.Count)];

                celda_actual.Quitar_Entidad(this);
                destino.Establecer_Entidad(this);
                celda_actual = destino;

                controlador_entidades.Chequear_Colision(this, celda_actual);
            }
        }

        return celda_actual;
    }

    public override void Reaccionar_A_Moneda(Moneda m)
    {
        if (m == null) return;

        m.Atrapada();
        Debug.Log("Enemigo_Avaro eliminó una moneda del tablero.");
    }

    public override void Decrementar_Vida(int Daño_Recibido)
    {
        base.Decrementar_Vida(Daño_Recibido);
        Debug.Log($"Enemigo_Avaro recibió {Daño_Recibido} de daño. Vida actual: {Puntos_de_Vida}");

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