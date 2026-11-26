using System;

public class Presentador_Fin_Juego
{
    private readonly Ronda_Manager manager;
    private readonly Vista_Fin_Juego vista;
    private readonly Action alReiniciar;

    private bool estabaEnFinJuego;

    public Presentador_Fin_Juego(Ronda_Manager manager, Vista_Fin_Juego vista, Action alReiniciar)
    {
        this.manager = manager ?? throw new ArgumentNullException(nameof(manager));
        this.vista = vista ?? throw new ArgumentNullException(nameof(vista));
        this.alReiniciar = alReiniciar;

        this.vista.OnReiniciarSolicitado += ManejarReiniciar;
        this.vista.Ocultar();
    }

    public void Actualizar()
    {
        bool enFinJuego = manager.Fase == Fase.FinJuego;

        if (enFinJuego && !estabaEnFinJuego)
        {
            estabaEnFinJuego = true;
            vista.Mostrar(manager.EsVictoria);
        }
        else if (!enFinJuego && estabaEnFinJuego)
        {
            estabaEnFinJuego = false;
            vista.Ocultar();
        }
    }

    private void ManejarReiniciar()
    {
        if (manager.Fase != Fase.FinJuego) return;

        estabaEnFinJuego = false;
        vista.Ocultar();
        alReiniciar?.Invoke();
    }

    public void Dispose()
    {
        if (vista != null)
        {
            vista.OnReiniciarSolicitado -= ManejarReiniciar;
        }
    }
}