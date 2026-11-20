using UnityEngine;


public class Tecla_Abajo : Tecla
{
    public Tecla_Abajo()
    {
        teclaAsignada = KeyCode.S;
        teclaAlternativa = KeyCode.DownArrow;
    }

    public override void Ejecutar_Funcionalidad(Presentador_Personaje presentador)
    {
        presentador.CambiarDireccion(Direccion.Abajo);
    }
}
