

using UnityEngine;

public class Presentador_Moneda_Oro : Presentador_Moneda
{
    private readonly Vista_Moneda_Oro vista1;

    public Presentador_Moneda_Oro()
    {
        GameObject go = new GameObject("Moneda_Oro_Obj");
        this.vista1 = go.AddComponent<Vista_Moneda_Oro>();
    }

    public override void dibujar(int posX, int posZ)
    {
        vista1.Dibujar(posX, posZ);
    }

    public override void EliminarEntidad()
    {
        vista1.Eliminar();
    }
}

