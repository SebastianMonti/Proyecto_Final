using System;
public abstract class Entidad
{   
    protected Celda celda;
    protected Presentador_Entidades entidade;



    // Obliga a cada entidad hija a definir cómo se dibuja en Unity
    public abstract void Dibujar(int Pos_X, int Pos_Z);

    // Retorna el tipo de la clase en tiempo de ejecución para el Type Dispatcher de colisiones
    public virtual Type Obtener_Tipo_Colision()
    {
        return this.GetType();
    }
}