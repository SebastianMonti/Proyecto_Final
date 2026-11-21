using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_Evasivo : Enemigo
{
    protected Presentador_Enemigo_Evasivo presentador;
    private Personaje jugador;

    public Enemigo_Evasivo(
        Celda celdaInicial,
        Tablero tablero,
        Generador_Monedas generadorMonedas,
        Controlador_Entidades controladorEntidades,
        Personaje jugador)
    {
        Puntos_de_Vida = 40; // Más vida que el básico (20)
        celda_actual = celdaInicial;
        celda_actual.Establecer_Entidad(this);

        this.tablero = tablero;
        this.generador_monedas = generadorMonedas;
        this.controlador_entidades = controladorEntidades;
        this.jugador = jugador;

        presentador = new Presentador_Enemigo_Evasivo(this);
        presentador.dibujar(celda_actual.Obtener_Posicion_X(), celda_actual.Obtener_Posicion_Z());

        // Recompensa un poco mayor al tener más vida (o podés cambiarlo a Moneda_Plata si ya la tenés)
        Lista_Monedas = new List<Moneda>();
        for (int i = 0; i < 5; i++)
        {
            Lista_Monedas.Add(new Moneda_Cobre());
        }
    }

    public override Celda Mover()
    {
        if (jugador == null)
        {
            return base.Mover();
        }

        List<Celda> vecinas = tablero.Obtener_Celdas_Vecinas(celda_actual);
        Celda celdaJugador = jugador.Obtener_Celda_Actual();

        if (vecinas != null && vecinas.Count > 0 && celdaJugador != null)
        {
            int jx = celdaJugador.Obtener_Posicion_X();
            int jz = celdaJugador.Obtener_Posicion_Z();

            int maxDistancia = -1;
            List<Celda> mejoresCeldas = new List<Celda>();

            foreach (Celda candidata in vecinas)
            {
                // Evita pisar otro enemigo o meterse encima del jugador
                if (candidata.Hay_Enemigo() || candidata == celdaJugador)
                {
                    continue;
                }

                int distancia = Math.Abs(candidata.Obtener_Posicion_X() - jx)
                              + Math.Abs(candidata.Obtener_Posicion_Z() - jz);

                if (distancia > maxDistancia)
                {
                    maxDistancia = distancia;
                    mejoresCeldas.Clear();
                    mejoresCeldas.Add(candidata);
                }
                else if (distancia == maxDistancia)
                {
                    mejoresCeldas.Add(candidata);
                }
            }

            // Si hay empate entre dos direcciones que lo alejan igual, elige una al azar
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

    public override void Decrementar_Vida(int Daño_Recibido)
    {
        base.Decrementar_Vida(Daño_Recibido);
        Debug.Log($"Enemigo_Evasivo recibió {Daño_Recibido} de daño. Vida actual: {Puntos_de_Vida}");

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