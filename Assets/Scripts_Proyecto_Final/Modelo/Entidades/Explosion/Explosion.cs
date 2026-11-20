using System;

public class Explosion : Entidad
{
    private int daño;

    public Explosion(int daño)
    {
        this.daño = daño;
        //this.entidade = new Presentador_Explosion(new Vista_Explosion());
        this.entidade = new Presentador_Explosion();

    }

    public int Obtener_Daño()
    {
        return daño;
    }

    public bool Verificar_Tipo_Entidad(Celda c)
    {
        return c.TieneExplosion();
    }

    public void Ejecutar_Funcionalidad(Controlador_Entidades c)
    {
        // Reservado si necesitás ejecutar lógica extra en el controlador
    }

    public  override void Dibujar( int Pos_X, int Pos_Z)
    {
       
          entidade.dibujar(Pos_X, Pos_Z);
        
    }
}