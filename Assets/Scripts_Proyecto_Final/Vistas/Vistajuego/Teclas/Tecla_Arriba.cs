using UnityEngine;


    public class Tecla_Arriba : Tecla
    {
        public Tecla_Arriba()
        {
            teclaAsignada = KeyCode.W;
            teclaAlternativa = KeyCode.UpArrow;
        }

        public override void Ejecutar_Funcionalidad(Presentador_Personaje presentador)
        {
            presentador.CambiarDireccion(Direccion.Arriba);
        }
    }
