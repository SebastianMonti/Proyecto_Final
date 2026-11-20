using UnityEngine;


public abstract class Tecla
{
    public KeyCode teclaAsignada;
    public KeyCode teclaAlternativa;

    public bool Verificar_Tecla_Ingresada()
    {
        return Input.GetKeyDown(teclaAsignada) ||
            (teclaAlternativa != KeyCode.None && Input.GetKeyDown(teclaAlternativa));
    }

    public abstract void Ejecutar_Funcionalidad(Presentador_Personaje presentador);
}
