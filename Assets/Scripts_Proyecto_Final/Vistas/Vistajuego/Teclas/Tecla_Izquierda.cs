using UnityEngine;


public class Tecla_Izquierda : Tecla
{
    public Tecla_Izquierda()
    {
        teclaAsignada = KeyCode.A;
        teclaAlternativa = KeyCode.LeftArrow;
    }

    public override void Ejecutar_Funcionalidad(Presentador_Personaje presentador)
    {
        presentador.CambiarDireccion(Direccion.Izquierda);
    }
}
