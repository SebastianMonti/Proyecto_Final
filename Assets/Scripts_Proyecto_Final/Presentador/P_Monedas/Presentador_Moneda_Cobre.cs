

using UnityEngine;

public class Presentador_Moneda_Cobre : Presentador_Moneda
{
    private readonly Vista_Moneda_Cobre vista1;

    public Presentador_Moneda_Cobre()
    {
        GameObject go = new GameObject("Moneda_Cobre_Obj");
        this.vista1 = go.AddComponent<Vista_Moneda_Cobre>();
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
/*public class Presentador_Moneda_Cobre : Presentador_Moneda
{
    private  Vista_Moneda_Cobre vista1;

    public Presentador_Moneda_Cobre()
    {
        this.vista1 = new Vista_Moneda_Cobre();
    }

    public override void dibujar(int posX, int posZ)
    {
        vista1.Dibujar(posX, posZ);
    }

    public override void EliminarEntidad()
    {
        vista.Eliminar();
    }
}*/
