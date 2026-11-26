using TMPro;
using UnityEngine;

public class Vista_Tiempo_Ronda : MonoBehaviour

{
    [SerializeField] private GameObject panelHud;
    [SerializeField] private TMP_Text textoPlata;
    [SerializeField] private TMP_Text textoRonda;
    [SerializeField] private TMP_Text textoTiempo;

    public void Mostrar(bool visible)
    {
        if (panelHud != null)
        {
            panelHud.SetActive(visible);
        }
        else
        {
            gameObject.SetActive(visible);
        }
    }

    public void ActualizarPlata(int monedas)
    {
        if (textoPlata != null)
        {
            textoPlata.text = $"${monedas}";
        }
    }

    public void ActualizarRonda(int numeroRonda)
    {
        if (textoRonda != null)
        {
            textoRonda.text = $"RONDA:{numeroRonda}";
        }
    }

    public void ActualizarTiempo(int segundosRestantes)
    {
        if (textoTiempo != null)
        {
            textoTiempo.text = $"T:{Mathf.Max(0, segundosRestantes)}";
        }
    }
}