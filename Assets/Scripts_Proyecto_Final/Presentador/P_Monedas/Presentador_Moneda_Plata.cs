

using UnityEngine;

public class Presentador_Moneda_Plata : Presentador_Moneda
{
    private readonly Vista_Moneda_Plata vista1;

    public Presentador_Moneda_Plata()
    {
        GameObject go = new GameObject("Moneda_Plata_Obj");
        this.vista1 = go.AddComponent<Vista_Moneda_Plata>();
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

