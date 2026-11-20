using UnityEngine;


public class Tecla_Derecha : Tecla
{
    public Tecla_Derecha()
    {
        teclaAsignada = KeyCode.D;
        teclaAlternativa = KeyCode.RightArrow;
    }

    public override void Ejecutar_Funcionalidad(Presentador_Personaje presentador)
    {
        presentador.CambiarDireccion(Direccion.Derecha);
    }
}
