using System;
public abstract class Moneda : Entidad
{
    protected int valor_moneda;

    public void Ubicar_En_Celda(Celda nuevaCelda)
    {
        this.celda = nuevaCelda ?? throw new System.ArgumentNullException(nameof(nuevaCelda));
        this.celda.Establecer_Entidad(this);
    }

    public int Obtener_Valor()
    {
        return valor_moneda;
    }

    public abstract void Atrapada();
}