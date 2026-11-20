public abstract class Presentador_Entidades 
{
    protected Vista_Entidades vista;
    protected Entidad entidad;
    public abstract void dibujar(int Pos_X, int Pos_Z);
    public abstract void EliminarEntidad();
}
