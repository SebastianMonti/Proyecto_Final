public class Moneda_Cobre : Moneda
{
    
    private bool atrapada;

    // Constructor limpio: solo define el valor lógico cuando el enemigo la crea al nacer
    public Moneda_Cobre()
    {
        valor_moneda = 10;
        atrapada = false;
        entidade = new Presentador_Moneda_Cobre();
        //bujar(posX, posZ);
    }

    public override void Dibujar( int Pos_X, int Pos_Z)
    {
        // Si usás el presentador individual de la moneda para dibujarla:
        entidade.dibujar(Pos_X, Pos_Z);
    }

    public override void Atrapada()
    {
        if (atrapada)
        {
            return;
        }

        atrapada = true;
        if (celda != null)
        {
            celda.Quitar_Entidad(this);
            celda = null;
        }

        entidade.EliminarEntidad();
    }
}