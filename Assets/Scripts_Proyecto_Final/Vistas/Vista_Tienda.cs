using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct Datos_Mejora_Tienda
{
    public string idMejora; // "radio", "tiempo", "velocidad", "danio", "suerte"
    public string claveNombre; // "tienda.radio", etc.
    public int nivel;
    public int precio;
    public bool puedeComprar;
}

public class Vista_Tienda : MonoBehaviour
{
    [Header("Panel y Botones")]
    [SerializeField] private GameObject panelTienda;
    [SerializeField] private Button botonContinuar;

    [Header("Labels Generales")]
    [SerializeField] private TMP_Text labelTitulo;
    [SerializeField] private TMP_Text labelProximoImpuesto;
    [SerializeField] private TMP_Text labelRondasRestantes;
    [SerializeField] private TMP_Text labelContinuar;

    [Header("Items de Mejora (Radio, Tiempo, Velocidad, Daño, Suerte)")]
    [SerializeField] private Item_Tienda_UI[] itemsMejora;

    public event Action<string> OnComprarMejoraSolicitada;
    public event Action OnContinuarSolicitado;

    private Datos_Mejora_Tienda[] _mejorasActuales = Array.Empty<Datos_Mejora_Tienda>();
    private int _proximoImpuesto;
    private int _rondasRestantes;

    [Serializable]
    private class Item_Tienda_UI
    {
        public string idMejora;
        public TMP_Text labelNombre;
        public TMP_Text labelNivel;
        public TMP_Text labelPrecio;
        public TMP_Text labelBotonComprar;
        public Button botonComprar;
    }

    private void Awake()
    {
        botonContinuar.onClick.AddListener(() => OnContinuarSolicitado?.Invoke());

        foreach (Item_Tienda_UI item in itemsMejora)
        {
            string id = item.idMejora;
            item.botonComprar.onClick.AddListener(() => OnComprarMejoraSolicitada?.Invoke(id));
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

    public void Mostrar(Datos_Mejora_Tienda[] mejoras, int proximoImpuesto, int rondasRestantes)
    {
        _mejorasActuales = mejoras ?? Array.Empty<Datos_Mejora_Tienda>();
        _proximoImpuesto = proximoImpuesto;
        _rondasRestantes = rondasRestantes;
        panelTienda.SetActive(true);
        RefrescarTextos();
    }

    public void ActualizarDatos(Datos_Mejora_Tienda[] mejoras, int proximoImpuesto, int rondasRestantes)
    {
        _mejorasActuales = mejoras ?? Array.Empty<Datos_Mejora_Tienda>();
        _proximoImpuesto = proximoImpuesto;
        _rondasRestantes = rondasRestantes;
        RefrescarTextos();
    }

    public void Ocultar()
    {
        panelTienda.SetActive(false);
    }

    public void RefrescarTextos()
    {
        Servicio_Localizacion loc = Servicio_Localizacion.Instancia;
        if (loc == null) return;

        labelTitulo.text = loc.Obtener("tienda.titulo");
        labelProximoImpuesto.text = loc.Obtener("tienda.proximo_impuesto", _proximoImpuesto);
        labelRondasRestantes.text = loc.Obtener("tienda.rondas_restantes", _rondasRestantes);
        labelContinuar.text = loc.Obtener("tienda.continuar");

        foreach (Item_Tienda_UI itemUI in itemsMejora)
        {
            Datos_Mejora_Tienda datos = BuscarDatos(itemUI.idMejora);

            if (!string.IsNullOrEmpty(datos.claveNombre))
            {
                itemUI.labelNombre.text = loc.Obtener(datos.claveNombre);
            }

            itemUI.labelNivel.text = loc.Obtener("tienda.nivel", datos.nivel);
            itemUI.labelPrecio.text = loc.Obtener("tienda.precio", datos.precio);
            itemUI.labelBotonComprar.text = loc.Obtener("tienda.comprar");
            itemUI.botonComprar.interactable = datos.puedeComprar;
        }
    }

    private Datos_Mejora_Tienda BuscarDatos(string idMejora)
    {
        foreach (Datos_Mejora_Tienda d in _mejorasActuales)
        {
            if (d.idMejora == idMejora)
            {
                return d;
            }
        }

        return new Datos_Mejora_Tienda { idMejora = idMejora };
    }
}