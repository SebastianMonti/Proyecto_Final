using System.Collections.Generic;

public class Controlador_Explosiones
{
    private float acumulador_tiempo;
    private Estadisticas estadisticas;
    private Personaje personaje;
    private Tablero tablero;
    private Controlador_Entidades controlador_entidades;

    public bool Pausado { get; set; }

    public Controlador_Explosiones(
        Estadisticas estadisticas,
        Personaje personaje,
        Tablero tablero,
        Controlador_Entidades controlador_entidades)
    {
        this.estadisticas = estadisticas;
        this.personaje = personaje;
        this.tablero = tablero;
        this.controlador_entidades = controlador_entidades;
        this.acumulador_tiempo = 0f;
        this.Pausado = false;
    }

    public void Actualizar(float delta)
    {
        if (Pausado) return;

        acumulador_tiempo += delta;

        if (acumulador_tiempo >= estadisticas.Obtener_Tiempo())
        {
            acumulador_tiempo = 0f;
            Explotar();
        }
    }

    public void Explotar()
    {
        Celda centro = personaje.Obtener_Celda_Actual();
        List<Celda> celdas = tablero.Obtener_Celdas_En_Cruz(centro, estadisticas.Obtener_Radio());

        foreach (Celda celda in celdas)
        {
            Explosion x = new Explosion(estadisticas.Obtener_Daño());
            celda.Establecer_Entidad(x);
            x.Dibujar(celda.Obtener_Posicion_X(), celda.Obtener_Posicion_Z());
            controlador_entidades.Chequear_Colision(x, celda);
            celda.Quitar_Entidad(x);
        }
    }
}

/*using System.Collections.Generic;


public class Controlador_Explosiones
{
    private float acumulador_tiempo;
    private Estadisticas estadisticas;
    private Personaje personaje;
    private Tablero tablero;
    private Controlador_Entidades controlador_entidades;

    public Controlador_Explosiones(
        Estadisticas estadisticas,
        Personaje personaje,
        Tablero tablero,
        Controlador_Entidades controlador_entidades)
    {
        this.estadisticas = estadisticas;
        this.personaje = personaje;
        this.tablero = tablero;
        this.controlador_entidades = controlador_entidades;
        this.acumulador_tiempo = 0f;
    }

    public void Actualizar(float delta)
    {
        acumulador_tiempo += delta;

        if (acumulador_tiempo >= estadisticas.Obtener_Tiempo())
        {
            acumulador_tiempo = 0f;
            Explotar();
        }
    }

    public void Explotar()
    {
        Celda centro = personaje.Obtener_Celda_Actual();
        List<Celda> celdas = tablero.Obtener_Celdas_En_Cruz(centro, estadisticas.Obtener_Radio());

        foreach (Celda celda in celdas)
        {
            Explosion x = new Explosion(estadisticas.Obtener_Daño());
            celda.Establecer_Entidad(x);
            x.Dibujar(celda.Obtener_Posicion_X(), celda.Obtener_Posicion_Z());
            controlador_entidades.Chequear_Colision(x, celda);
            celda.Quitar_Entidad(x);
        }
    }
}*/