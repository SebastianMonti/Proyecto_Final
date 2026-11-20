public class Ronda
{
    private readonly Personaje personaje;
    private readonly Controlador_Movimiento controladorMovimiento;

    public Ronda(Personaje personaje, Controlador_Movimiento controladorMovimiento)
    {
        this.personaje = personaje
            ?? throw new System.ArgumentNullException(nameof(personaje));
        this.controladorMovimiento = controladorMovimiento
            ?? throw new System.ArgumentNullException(nameof(controladorMovimiento));
    }

    public bool Mover(Direccion direccion)
    {
        return controladorMovimiento.Mover(direccion);
    }

    public Celda Obtener_Celda_Personaje()
    {
        return personaje.Obtener_Celda_Actual();
    }

    public void Avisar_mori(Celda celda, Entidad entidad)
    {
        if (celda != null && entidad != null)
        {
            celda.Quitar_Entidad(entidad);
        }
    }
}
