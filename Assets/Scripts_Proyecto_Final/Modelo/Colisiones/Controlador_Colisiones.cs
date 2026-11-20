using System;
using System.Collections.Generic;

public class Controlador_Colisiones
{
    private Dictionary<(Type, Type), IManejador_Colision> manejadores;

    public Controlador_Colisiones()
    {
        manejadores = new Dictionary<(Type, Type), IManejador_Colision>();
    }

    public void Registrar_Colision<TA, TB>(Action<TA, TB> accion)
        where TA : Entidad
        where TB : Entidad
    {
        // Registra la colisión. Se recomienda también registrar la inversa.
        manejadores[(typeof(TA), typeof(TB))] = new Manejador_Colision<TA, TB>(accion);
        manejadores[(typeof(TB), typeof(TA))] = new Manejador_Colision<TB, TA>((b, a) => accion(a, b));
    }

    public void Manejar_Colision(Entidad a, Entidad b)
    {
        // Obtiene la clave en base a los tipos en tiempo de ejecución
        var clave = (a.GetType(), b.GetType());

        if (manejadores.TryGetValue(clave, out IManejador_Colision manejador))
        {
            manejador.Manejar_Colision(a, b);
        }
    }
}