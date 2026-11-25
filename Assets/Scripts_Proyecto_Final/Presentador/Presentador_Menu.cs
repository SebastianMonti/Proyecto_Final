using System;
using UnityEngine;

public class Presentador_Menu 
{
    private readonly Vista_Menu _vistaMenu;
    private readonly Vista_Seleccionar_Idioma _vistaIdioma;
    private readonly Ronda_Manager _rondaManager;

    public Presentador_Menu(
        Vista_Menu vistaMenu,
        Vista_Seleccionar_Idioma vistaIdioma,
        Ronda_Manager rondaManager)
    {
        _vistaMenu = vistaMenu;
        _vistaIdioma = vistaIdioma;
        _rondaManager = rondaManager;

        SuscribirEventos();
        ActualizarTextosMenu();
    }

    private void SuscribirEventos()
    {
        if (_vistaMenu != null)
        {
            _vistaMenu.OnJugarSolicitado += ManejarJugar;
            _vistaMenu.OnSeleccionarIdiomaSolicitado += ManejarSeleccionarIdioma;
        }

        if (_vistaIdioma != null)
        {
            _vistaIdioma.OnVolverSolicitado += ManejarVolverAlMenu;
        }

        if (Servicio_Localizacion.Instancia != null)
        {
            Servicio_Localizacion.Instancia.OnIdiomaCambiado += ActualizarTextosMenu;
        }
    }

    private void ManejarJugar()
    {
        Debug.Log("Presentador_Menu: Iniciar_Partida");
        _vistaMenu?.Ocultar();

        if (_rondaManager != null)
        {
            _rondaManager.Iniciar_Partida();
        }
        else
        {
            Debug.LogWarning("Presentador_Menu: no está asignado Ronda_Manager.");
        }
    }

    private void ManejarSeleccionarIdioma()
    {   Debug.Log("Presentador_Menu: Mostrar Vista_Seleccionar_Idioma");
        _vistaMenu?.Ocultar();

        if (_vistaIdioma != null)
        {
            _vistaIdioma.Mostrar();
        }
        else
        {
            Debug.LogWarning("Presentador_Menu: no está asignada Vista_Seleccionar_Idioma.");
        }
    }

    private void ManejarVolverAlMenu()
    {
        _vistaIdioma?.Ocultar();
        ActualizarTextosMenu();
        _vistaMenu?.Mostrar();
    }

    public void ActualizarTextosMenu()
    {
        if (_vistaMenu == null || Servicio_Localizacion.Instancia == null) return;

        string textoJugar = Servicio_Localizacion.Instancia.Obtener("menu.nueva_partida");
        string textoIdioma = Servicio_Localizacion.Instancia.Obtener("menu.idioma");
        _vistaMenu.EstablecerTextos(textoJugar, textoIdioma);
    }

    public void Dispose()
    {
        if (_vistaMenu != null)
        {
            _vistaMenu.OnJugarSolicitado -= ManejarJugar;
            _vistaMenu.OnSeleccionarIdiomaSolicitado -= ManejarSeleccionarIdioma;
        }

        if (_vistaIdioma != null)
        {
            _vistaIdioma.OnVolverSolicitado -= ManejarVolverAlMenu;
        }

        if (Servicio_Localizacion.Instancia != null)
        {
            Servicio_Localizacion.Instancia.OnIdiomaCambiado -= ActualizarTextosMenu;
        }
    }
}