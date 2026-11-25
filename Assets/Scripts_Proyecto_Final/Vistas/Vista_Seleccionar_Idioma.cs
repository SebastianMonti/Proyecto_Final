using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Vista_Seleccionar_Idioma : MonoBehaviour
{
    [Header("Panel y Botones")]
    [SerializeField] private GameObject panelIdioma;
    [SerializeField] private Button botonEspanol;
    [SerializeField] private Button botonIngles;
    [SerializeField] private Button botonSalir;

    [Header("Labels")]
    [SerializeField] private TMP_Text labelEspanol;
    [SerializeField] private TMP_Text labelIngles;
    [SerializeField] private TMP_Text labelSalir;

    public event Action OnVolverSolicitado;

    private void Awake()
    {
        botonEspanol.onClick.AddListener(AlPulsarEspanol);
        botonIngles.onClick.AddListener(AlPulsarIngles);
        botonSalir.onClick.AddListener(AlPulsarSalir);
    }

    private void AlPulsarEspanol()
    {
        SeleccionarIdioma(Idioma_Juego.Espanol);
    }
    private void AlPulsarIngles()
    {
        SeleccionarIdioma(Idioma_Juego.Ingles);
    }
    private void AlPulsarSalir()
    {
        if (OnVolverSolicitado != null)
        {
            OnVolverSolicitado.Invoke();
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

    public void Mostrar()
    {
        panelIdioma.SetActive(true);
        RefrescarTextos();
    }

    public void Ocultar()
    {
        panelIdioma.SetActive(false);
    }

    private void SeleccionarIdioma(Idioma_Juego idioma)
    {
        Debug.Log($"Click idioma {idioma} ");
        if (Servicio_Localizacion.Instancia != null)
        {
            Servicio_Localizacion.Instancia.CargarIdioma(idioma);
        }
        Debug.Log($"Idioma cargado: {idioma} ");
    }

    public void RefrescarTextos()
    {
        Servicio_Localizacion loc = Servicio_Localizacion.Instancia;
        if (loc == null) 
            return;

        labelEspanol.text = loc.Obtener("idioma.es");
        labelIngles.text = loc.Obtener("idioma.en");
        labelSalir.text = loc.Obtener("trivia.continuar");
    }
}