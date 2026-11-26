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
        if (botonJugar != null)
        {
            botonJugar.onClick.AddListener(() =>
            {
                Debug.Log("Click directo en botonJugar");
                OnJugarSolicitado?.Invoke();
            });
        }

        if (botonSeleccionarIdioma != null)
        {
            botonSeleccionarIdioma.onClick.AddListener(() => OnSeleccionarIdiomaSolicitado?.Invoke());
        }
    }

    private void OnDestroy()
    {
        if (botonJugar != null) botonJugar.onClick.RemoveAllListeners();
        if (botonSeleccionarIdioma != null) botonSeleccionarIdioma.onClick.RemoveAllListeners();
    }

    public void Mostrar()
    {
        if (panelMenu != null) panelMenu.SetActive(true);
    }

    public void Ocultar()
    {
        if (panelMenu != null) panelMenu.SetActive(false);
    }

    public void EstablecerTextos(string textoJugar, string textoSeleccionarIdioma)
    {
        if (labelJugar != null) labelJugar.text = textoJugar;
        if (labelSeleccionarIdioma != null) labelSeleccionarIdioma.text = textoSeleccionarIdioma;
    }
}

/*using System;
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
    private void OnEnable()
    {
        if (Servicio_Localizacion.Instancia != null)
            Servicio_Localizacion.Instancia.OnIdiomaCambiado += Actualizar_Idioma;

        Actualizar_Idioma();
    }

    private void OnDisable()
    {
        if (Servicio_Localizacion.Instancia != null)
            Servicio_Localizacion.Instancia.OnIdiomaCambiado -= Actualizar_Idioma;
    }

    private void Actualizar_Idioma()
    {
        presentador?.Actualizar_Vista();
    }
    public void EstablecerTextos(string textoJugar, string textoSeleccionarIdioma)
    {
        labelJugar.text = textoJugar;
        labelSeleccionarIdioma.text = textoSeleccionarIdioma;
    }
}*/