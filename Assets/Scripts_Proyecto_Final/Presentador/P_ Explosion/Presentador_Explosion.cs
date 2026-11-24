public class Presentador_Explosion : Presentador_Entidades
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
}

