public class Presentador_Personaje : Presentador_Entidades
{
    private readonly Controlador_Movimiento controladorMovimiento;
   

    public Presentador_Personaje(Personaje jugador)
    {
        vista = new Vista_Personaje(this);
        entidad = jugador;
        controladorMovimiento = jugador.Obtener_Controlador_Movimiento();
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
