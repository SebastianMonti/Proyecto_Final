using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Vista_Impuestos : MonoBehaviour
{
    [Header("Panel y Botón")]
    [SerializeField] private GameObject panelImpuestos;
    [SerializeField] private Button botonPagar;

    [Header("Labels")]
    [SerializeField] private TMP_Text labelTitulo;
    [SerializeField] private TMP_Text labelMonto;
    [SerializeField] private TMP_Text labelPagar;

    public event Action OnPagarSolicitado;

    private int _montoActual;

    private void Awake()
    {
        if (botonPagar != null)
        {
            botonPagar.onClick.AddListener(() => OnPagarSolicitado?.Invoke());
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
        if (botonPagar != null)
        {
            botonPagar.onClick.RemoveAllListeners();
        }
    }

    public void Mostrar(int monto)
    {
        _montoActual = monto;
        if (panelImpuestos != null) panelImpuestos.SetActive(true);
        RefrescarTextos();
    }

    public void Ocultar()
    {
        if (panelImpuestos != null) panelImpuestos.SetActive(false);
    }

    public void RefrescarTextos()
    {
        Servicio_Localizacion loc = Servicio_Localizacion.Instancia;
        if (loc == null) return;

        if (labelTitulo != null) labelTitulo.text = loc.Obtener("impuestos.titulo");
        if (labelMonto != null) labelMonto.text = loc.Obtener("impuestos.monto", _montoActual);
        if (labelPagar != null) labelPagar.text = loc.Obtener("impuestos.pagar");
    }
}