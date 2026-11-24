using UnityEngine;

public class Presentador_Personaje : Presentador_Entidades
{
    private readonly Controlador_Movimiento controladorMovimiento;

    public Presentador_Personaje(Personaje jugador)
    {
        GameObject go = new GameObject("Personaje_Obj");
        Vista_Personaje vistaPersonaje = go.AddComponent<Vista_Personaje>();
        vistaPersonaje.Conectar_Presentador(this);

        this.vista = vistaPersonaje;
        this.entidad = jugador;
        this.controladorMovimiento = jugador.Obtener_Controlador_Movimiento();

        ActualizarVista();
    }

    public void CambiarDireccion(Direccion direccion)
    {
        if (controladorMovimiento.Mover(direccion))
        {
            ActualizarVista();
        }
    }

    private void ActualizarVista()
    {
        Celda celdaActual = controladorMovimiento.Obtener_Celda_Personaje();
        dibujar(
            celdaActual.Obtener_Posicion_X(),
            celdaActual.Obtener_Posicion_Z());
    }

    public override void dibujar(int posX, int posZ)
    {
        vista.Mover(posX, posZ);
    }

    public override void EliminarEntidad()
    {
        vista.Eliminar();
    }
}


