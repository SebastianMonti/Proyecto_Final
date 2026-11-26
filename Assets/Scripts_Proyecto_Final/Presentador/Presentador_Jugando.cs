using System;
using UnityEngine;

public class Presentador_Jugando 
{
    private readonly Ronda_Manager manager;
    private readonly Estadisticas estadisticas;
    private readonly Vista_Tiempo_Ronda vista;

    private int ultimaPlata = int.MinValue;
    private int ultimaRonda = int.MinValue;
    private int ultimoTiempo = int.MinValue;
    private bool ultimaVisibilidad = false;

    public Presentador_Jugando(Ronda_Manager manager, Estadisticas estadisticas, Vista_Tiempo_Ronda vista)
    {
        this.manager = manager ?? throw new ArgumentNullException(nameof(manager));
        this.estadisticas = estadisticas ?? throw new ArgumentNullException(nameof(estadisticas));
        this.vista = vista != null ? vista : throw new ArgumentNullException(nameof(vista));

        vista.Mostrar(false);
    }

    public void Actualizar()
    {
        if (vista == null)
            return;

        bool visible = manager.Fase == Fase.Batalla && manager.Ronda_Actual != null;

        if (visible != ultimaVisibilidad)
        {
            ultimaVisibilidad = visible;
            vista.Mostrar(visible);

            if (!visible)
            {
                ReiniciarCache();
                return;
            }
        }

        if (!visible)
            return;

        int plata = estadisticas.Obtener_Monedas();
        if (plata != ultimaPlata)
        {
            ultimaPlata = plata;
            vista.ActualizarPlata(plata);
        }

        int rondaNumero = manager.Numero;
        if (rondaNumero != ultimaRonda)
        {
            ultimaRonda = rondaNumero;
            vista.ActualizarRonda(rondaNumero);
        }

        int tiempo = Mathf.CeilToInt(manager.Ronda_Actual.TiempoRestante);
        if (tiempo != ultimoTiempo)
        {
            ultimoTiempo = tiempo;
            vista.ActualizarTiempo(tiempo);
        }
    }

    private void ReiniciarCache()
    {
        ultimaPlata = int.MinValue;
        ultimaRonda = int.MinValue;
        ultimoTiempo = int.MinValue;
    }

    public void Dispose()
    {
        if (vista != null)
        {
            vista.Mostrar(false);
        }
    }
}