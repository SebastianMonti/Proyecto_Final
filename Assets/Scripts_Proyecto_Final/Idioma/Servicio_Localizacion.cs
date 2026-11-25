using System.Collections.Generic;
using UnityEngine;
using System;

public class Servicio_Localizacion : MonoBehaviour
{
    public static Servicio_Localizacion Instancia { get; private set; }

    [SerializeField] private Idioma_Juego idiomaInicial = Idioma_Juego.Espanol;

    private readonly Dictionary<string, string> textos = new Dictionary<string, string>();
    public Idioma_Juego IdiomaActual { get; private set; }
    public event Action OnIdiomaCambiado;

    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        Instancia = this;
        CargarIdioma(idiomaInicial);
    }

    public void CargarIdioma(Idioma_Juego idioma)
    {
        string nombreArchivo = ObtenerNombreArchivo(idioma);
        TextAsset asset = Resources.Load<TextAsset>($"Localizacion/{nombreArchivo}");

        if (asset == null)
        {
            Debug.LogError($"No se encontró Resources/Localizacion/{nombreArchivo}.json");
            return;
        }

        Archivo_Localizacion archivo = JsonUtility.FromJson<Archivo_Localizacion>(asset.text);
        textos.Clear();

        if (archivo != null && archivo.entradas != null)
        {
            foreach (Entrada_Localizacion entrada in archivo.entradas)
            {
                if (!string.IsNullOrEmpty(entrada.clave))
                {
                    textos[entrada.clave] = entrada.texto;
                }
            }
        }

        IdiomaActual = idioma;
        OnIdiomaCambiado?.Invoke();
    }

    private string ObtenerNombreArchivo(Idioma_Juego idioma)
    {
        switch (idioma)
        {
            case Idioma_Juego.Ingles:
                return "en";
            case Idioma_Juego.Espanol:
            default:
                return "es";
        }
    }

    public string Obtener(string clave)
    {
        if (textos.TryGetValue(clave, out string valor))
        {
            return valor;
        }

        Debug.LogWarning($"Clave de localización no encontrada: '{clave}'");
        return clave;
    }

    public string Obtener(string clave, params object[] args)
    {
        string plantilla = Obtener(clave);
        return string.Format(plantilla, args);
    }
}