using UnityEngine;

public class Presentador_Explosion : Presentador_Entidades
{
    private readonly Vista_Explosion vista;

    public Presentador_Explosion()
    {
        GameObject go = new GameObject("Explosion_Obj");
        this.vista = go.AddComponent<Vista_Explosion>();
    }

    public override void dibujar(int posX, int posZ)
    {
        vista.Dibujar(posX, posZ);
    }

    public override void EliminarEntidad()
    {
        vista.Eliminar();
    }
}




/*public class Presentador_Explosion : Presentador_Entidades
{
    private readonly Vista_Explosion vista1;

    public Presentador_Explosion()
    {
        this.vista1 = new Vista_Explosion();
    }

    public override void dibujar(int posX, int posZ)
    {
        vista1.Dibujar(posX, posZ);
    }

    public override void EliminarEntidad()
    {
        vista1.Eliminar();
    }
}*/