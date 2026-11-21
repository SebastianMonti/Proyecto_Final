using UnityEngine;

public class Presentador_Enemigo_Tanque : Presentador_Enemigo
{
    public Presentador_Enemigo_Tanque(Enemigo ene) : base(ene)
    {
        GameObject go = new GameObject("Enemigo_Tanque_Obj");
        Vista_Enemigo_Tanque vista = go.AddComponent<Vista_Enemigo_Tanque>();
        vista.Conectar_Presentador(this);
        this.vista_enemigo = vista;
    }

    public override void Mover()
    {
        Celda nuevaCelda = enemigo.Mover();

        if (nuevaCelda != null && vista_enemigo != null)
        {
            vista_enemigo.Mover(nuevaCelda.Obtener_Posicion_X(), nuevaCelda.Obtener_Posicion_Z());
        }
    }

    public override void dibujar(int PosX, int PosZ)
    {
        Celda celdaActual = enemigo.Obtener_Celda_Actual();

        if (celdaActual != null && vista_enemigo != null)
        {
            vista_enemigo.Dibujar(celdaActual.Obtener_Posicion_X(), celdaActual.Obtener_Posicion_Z());
        }
    }

    public override void EliminarEntidad()
    {
        if (vista_enemigo != null)
        {
            vista_enemigo.Eliminar();
        }
    }
}