using System;

public class Manejador_Colision<TA, TB> : IManejador_Colision
        where TA : Entidad
        where TB : Entidad
{
    private Action<TA, TB> action;

    public Manejador_Colision(Action<TA, TB> accion)
    {
        this.action = accion;
    }

    public void Manejar_Colision(Entidad a, Entidad b)
    {
        action((TA)a, (TB)b);
    }
}
