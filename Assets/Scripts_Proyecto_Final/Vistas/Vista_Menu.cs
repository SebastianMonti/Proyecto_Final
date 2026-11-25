using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Vista_Menu : MonoBehaviour
{
    [Header("Panel y Botones")]
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private Button botonJugar;
    [SerializeField] private Button botonSeleccionarIdioma;

    [Header("Labels")]
    [SerializeField] private TMP_Text labelJugar;
    [SerializeField] private TMP_Text labelSeleccionarIdioma;

    public event Action OnJugarSolicitado;
    public event Action OnSeleccionarIdiomaSolicitado;

    private void Awake()
    {
        botonJugar.onClick.AddListener(() =>
        {
            Debug.Log("Click directo en botonJugar");
            OnJugarSolicitado?.Invoke();
        });
        botonSeleccionarIdioma.onClick.AddListener(() => OnSeleccionarIdiomaSolicitado?.Invoke());
    }

    private void OnDestroy()
    {
        botonJugar.onClick.RemoveAllListeners();
        botonSeleccionarIdioma.onClick.RemoveAllListeners();
    }

    public void Mostrar()
    {
        panelMenu.SetActive(true);
    }
    public void Ocultar()
    {
        panelMenu.SetActive(false);
    }

    public void EstablecerTextos(string textoJugar, string textoSeleccionarIdioma)
    {
        labelJugar.text = textoJugar;
        labelSeleccionarIdioma.text = textoSeleccionarIdioma;
    }
}