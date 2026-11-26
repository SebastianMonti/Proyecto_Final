using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Vista_Fin_Juego : MonoBehaviour
{
    [Header("Panel y Botón")]
    [SerializeField] private GameObject panelFinJuego;
    [SerializeField] private Button botonReiniciar;

    [Header("Labels")]
    [SerializeField] private TMP_Text labelTitulo;
    [SerializeField] private TMP_Text labelDetalle;
    [SerializeField] private TMP_Text labelReiniciar;

    public event Action OnReiniciarSolicitado;

    private bool _esVictoria;

    private void Awake()
    {
        if (botonReiniciar != null)
        {
            botonReiniciar.onClick.AddListener(() => OnReiniciarSolicitado?.Invoke());
        }
    }

    private void OnEnable()
    {
        if (Servicio_Localizacion.Instancia != null)
        {
            Servicio_Localizacion.Instancia.OnIdiomaCambiado += RefrescarTextos;
        }

        RefrescarTextos();
    }

    private void OnDisable()
    {
        if (Servicio_Localizacion.Instancia != null)
        {
            Servicio_Localizacion.Instancia.OnIdiomaCambiado -= RefrescarTextos;
        }
    }

    private void OnDestroy()
    {
        if (botonReiniciar != null)
        {
            botonReiniciar.onClick.RemoveAllListeners();
        }
    }

    public void Mostrar(bool esVictoria)
    {
        _esVictoria = esVictoria;
        if (panelFinJuego != null) panelFinJuego.SetActive(true);
        RefrescarTextos();
    }

    public void Ocultar()
    {
        if (panelFinJuego != null) panelFinJuego.SetActive(false);
    }

    public void RefrescarTextos()
    {
        Servicio_Localizacion loc = Servicio_Localizacion.Instancia;
        if (loc == null) return;

        if (_esVictoria)
        {
            if (labelTitulo != null) labelTitulo.text = loc.Obtener("fin.victoria");
            if (labelDetalle != null) labelDetalle.text = loc.Obtener("fin.victoria_detalle");
        }
        else
        {
            if (labelTitulo != null) labelTitulo.text = loc.Obtener("fin.gameover");
            if (labelDetalle != null) labelDetalle.text = loc.Obtener("fin.bancarrota");
        }

        if (labelReiniciar != null) labelReiniciar.text = loc.Obtener("fin.reiniciar");
    }
}