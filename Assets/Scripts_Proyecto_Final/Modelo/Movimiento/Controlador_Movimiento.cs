using System;

public class Controlador_Movimiento
{
    private readonly Tablero tablero;
    private readonly Personaje personaje;
    private readonly Controlador_Entidades controladorEntidades;

    public Controlador_Movimiento(
        Tablero tablero,
        Personaje personaje,
        Controlador_Entidades controladorEntidades)
    {
        this.tablero = tablero ?? throw new ArgumentNullException(nameof(tablero));
        this.personaje = personaje ?? throw new ArgumentNullException(nameof(personaje));
        this.controladorEntidades = controladorEntidades
            ?? throw new ArgumentNullException(nameof(controladorEntidades));
    }
    //actualizar_posicion
    public bool Mover(Direccion direccion)
    {
        if (direccion == null)
        {
            return false;
        }   

        Celda celdaActual = personaje.Obtener_Celda_Actual();
        if (celdaActual == null)
        {
            return false;
        }

        int x = celdaActual.Obtener_Posicion_X() + direccion.Obtener_Delta_X();
        int z = celdaActual.Obtener_Posicion_Z() + direccion.Obtener_Delta_Z();
        Celda celdaSiguiente = tablero.Obtener_Celda(x, z);

        if (celdaSiguiente == null)
        {
            return false;
        }

        controladorEntidades.Chequear_Colision(personaje, celdaSiguiente);
        personaje.Actualizar_Posicion(celdaSiguiente);

        return true;
    }
    public Celda Obtener_Celda_Personaje()
    {
        return personaje.Obtener_Celda_Actual();
    }
}
