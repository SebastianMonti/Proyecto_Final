using System.Collections.Generic;

public class Ronda
{
    private readonly Personaje personaje;
    private readonly Controlador_Movimiento controladorMovimiento;

    public Ronda(Personaje personaje, Controlador_Movimiento controladorMovimiento)
    {
        this.personaje = personaje
            ?? throw new System.ArgumentNullException(nameof(personaje));
        this.controladorMovimiento = controladorMovimiento
            ?? throw new System.ArgumentNullException(nameof(controladorMovimiento));
    }

    public bool Mover(Direccion direccion)
    {
        return controladorMovimiento.Mover(direccion);
    }

    public Celda Obtener_Celda_Personaje()
    {
        return personaje.Obtener_Celda_Actual();
    }

    public void Avisar_mori(Celda celda, Entidad entidad)
    {
        if (celda != null && entidad != null)
        {
            celda.Quitar_Entidad(entidad);
        }
    }



    public DatosRonda Datos { get; }
    public float TiempoRestante { get; private set; }
    public int GananciasRonda { get; private set; }

    readonly List<Enemigo> enemigos;

    public bool Terminada => TiempoRestante <= 0;
    public bool SinEnemigos => enemigos.Count == 0;

    public Ronda(DatosRonda datos, float duracion)
    {
        Datos = datos;
        TiempoRestante = duracion;
        enemigos = new List<Enemigo>(datos.horda);
    }

    public void Agregar_Horda(List<Enemigo> nueva) => enemigos.AddRange(nueva);
    public void Quitar(Enemigo e) => enemigos.Remove(e);
    public void Sumar_Ganancias(int v) => GananciasRonda += v;


    public void Actualizar_Tiempo(float dt)
    {
        TiempoRestante -= dt;
        enemigos.RemoveAll(e => !e.Esta_Vivo);   // saca a los muertos de la lista
    }

    public int EnemigosVivos => enemigos.Count;

    public void Limpiar_Enemigos()
    {
        foreach (var e in new List<Enemigo>(enemigos))
        {
            e.Obtener_Lista_Monedas().Clear();   // sin monedas para soltar
            e.Decrementar_Vida(int.MaxValue);    // muerte normal: libera la celda y destruye el dibujo
        }
        enemigos.Clear();
    }
}

