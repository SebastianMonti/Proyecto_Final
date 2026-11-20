
using System;
using System.Collections.Generic;

public abstract class Enemigo : Entidad
{
    protected int Puntos_de_Vida;
    protected List<Moneda> Lista_Monedas;
    protected Celda celda_actual;
    protected Tablero tablero;
    protected Generador_Monedas generador_monedas;

    protected Controlador_Entidades controlador_entidades;
    protected Ronda ronda_manager;

    // Estático para que varios enemigos moviéndose en el mismo frame no elijan la misma dirección
    protected static Random rnd = new Random();

    public virtual Celda Mover()
    {
        List<Celda> vecinas = tablero.Obtener_Celdas_Vecinas(celda_actual);

        if (vecinas != null && vecinas.Count > 0)
        {
            Celda c = vecinas[rnd.Next(vecinas.Count)];

            if (!c.Hay_Enemigo())
            {
                celda_actual.Quitar_Entidad(this);
                c.Establecer_Entidad(this);
                celda_actual = c;

                controlador_entidades.Chequear_Colision(this, celda_actual);
            }
        }
        return celda_actual;
    }

    public virtual void Decrementar_Vida(int Daño_Recibido)
    {
        Puntos_de_Vida -= Daño_Recibido;
        if (Puntos_de_Vida <= 0)
        {
            Puntos_de_Vida = 0;
            celda_actual.Quitar_Entidad(this);
            ronda_manager?.Avisar_mori(celda_actual, this);
            generador_monedas.Generar_Monedas(celda_actual, this);
        }
    }

    public Celda Obtener_Celda_Actual()
    {
        return celda_actual;
    }

    public virtual List<Moneda> Obtener_Lista_Monedas()
    {
        return Lista_Monedas;
    }

    public virtual void Reaccionar_A_Moneda(Moneda m)
    {
        // Vacío por defecto; Enemigo_Avaro lo sobreescribe
    }
}