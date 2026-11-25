using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Vista_Trivia : MonoBehaviour
{
    private enum Estado_Visual_Trivia
    {
        Decision,
        Cargando,
        Pregunta,
        Resultado
    }

    [Header("Paneles y Botones")]
    [SerializeField] private GameObject panelTrivia;
    [SerializeField] private GameObject panelDecision;
    [SerializeField] private GameObject panelPregunta;
    [SerializeField] private GameObject panelResultado;

    [SerializeField] private Button botonParticipar;
    [SerializeField] private Button botonNoParticipar;
    [SerializeField] private Button botonContinuar;
    [SerializeField] private Button[] botonesOpciones;

    [Header("Labels")]
    [SerializeField] private TMP_Text labelTitulo;
    [SerializeField] private TMP_Text labelDetalle;
    [SerializeField] private TMP_Text labelPregunta;
    [SerializeField] private TMP_Text[] labelsOpciones;
    [SerializeField] private TMP_Text labelParticipar;
    [SerializeField] private TMP_Text labelNoParticipar;
    [SerializeField] private TMP_Text labelContinuar;

    private Presentador_Trivia _presentador;
    private Estado_Visual_Trivia _estadoActual = Estado_Visual_Trivia.Decision;

    private int _gananciaRonda;
    private int _gananciaAnterior;
    private int _gananciaNueva;
    private string _respuestaCorrecta = string.Empty;
    private bool _esResultadoSinRiesgo;
    private bool _esResultadoCorrecto;

    private void Awake()
    {
        if (botonParticipar != null)
            botonParticipar.onClick.AddListener(() => _presentador?.Al_Participar());

        if (botonNoParticipar != null)
            botonNoParticipar.onClick.AddListener(() => _presentador?.Al_No_Participar());

        if (botonContinuar != null)
            botonContinuar.onClick.AddListener(() => _presentador?.Al_Continuar());

        if (botonesOpciones != null)
        {
            for (int i = 0; i < botonesOpciones.Length; i++)
            {
                int indice = i;
                if (botonesOpciones[i] != null)
                {
                    botonesOpciones[i].onClick.AddListener(() => _presentador?.Al_Responder(indice));
                }
            }
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

    public void Establecer_Presentador(Presentador_Trivia presentador)
    {
        _presentador = presentador;
    }

    public void MostrarDecision(int gananciaRonda)
    {
        _gananciaRonda = gananciaRonda;
        _estadoActual = Estado_Visual_Trivia.Decision;

        panelTrivia.SetActive(true);
        ActivarSubpaneles(decision: true, pregunta: false, resultado: false);
        ConfigurarBotonesAccion(decision: true, continuar: false);
        RefrescarTextos();
    }

    public void MostrarCargando()
    {
        _estadoActual = Estado_Visual_Trivia.Cargando;

        panelTrivia.SetActive(true);
        ActivarSubpaneles(decision: true, pregunta: false, resultado: false);
        ConfigurarBotonesAccion(decision: false, continuar: false);
        RefrescarTextos();
    }

    public void MostrarPregunta(Pregunta pregunta)
    {
        _estadoActual = Estado_Visual_Trivia.Pregunta;

        panelTrivia.SetActive(true);
        ActivarSubpaneles(decision: false, pregunta: true, resultado: false);
        ConfigurarBotonesAccion(decision: false, continuar: false);

        if (pregunta != null)
        {
            if (labelPregunta != null)
                labelPregunta.text = pregunta.texto;

            if (botonesOpciones != null && labelsOpciones != null && pregunta.opciones != null)
            {
                for (int i = 0; i < botonesOpciones.Length; i++)
                {
                    bool tieneOpcion = i < pregunta.opciones.Length;
                    botonesOpciones[i].gameObject.SetActive(tieneOpcion);

                    if (tieneOpcion && i < labelsOpciones.Length && labelsOpciones[i] != null)
                    {
                        labelsOpciones[i].text = pregunta.opciones[i];
                    }
                }
            }
        }

        RefrescarTextosFijos();
    }

    public void MostrarResultadoSinRiesgo(int gananciaRonda)
    {
        _gananciaRonda = gananciaRonda;
        _esResultadoSinRiesgo = true;
        _estadoActual = Estado_Visual_Trivia.Resultado;

        panelTrivia.SetActive(true);
        ActivarSubpaneles(decision: false, pregunta: false, resultado: true);
        ConfigurarBotonesAccion(decision: false, continuar: true);
        RefrescarTextos();
    }

    public void Mostrar_Resultado(bool acerto, string respuestaCorrecta, int gananciaAnterior, int gananciaNueva)
    {
        _esResultadoSinRiesgo = false;
        _esResultadoCorrecto = acerto;
        _respuestaCorrecta = respuestaCorrecta ?? string.Empty;
        _gananciaAnterior = gananciaAnterior;
        _gananciaNueva = gananciaNueva;
        _estadoActual = Estado_Visual_Trivia.Resultado;

        panelTrivia.SetActive(true);
        ActivarSubpaneles(decision: false, pregunta: false, resultado: true);
        ConfigurarBotonesAccion(decision: false, continuar: true);
        RefrescarTextos();
    }

    public void Ocultar()
    {
        panelTrivia.SetActive(false);
    }

    private void ActivarSubpaneles(bool decision, bool pregunta, bool resultado)
    {
        if (panelDecision != null) panelDecision.SetActive(decision);
        if (panelPregunta != null) panelPregunta.SetActive(pregunta);
        if (panelResultado != null) panelResultado.SetActive(resultado);
    }

    private void ConfigurarBotonesAccion(bool decision, bool continuar)
    {
        if (botonParticipar != null) botonParticipar.gameObject.SetActive(decision);
        if (botonNoParticipar != null) botonNoParticipar.gameObject.SetActive(decision);
        if (botonContinuar != null) botonContinuar.gameObject.SetActive(continuar);
    }

    private void RefrescarTextosFijos()
    {
        Servicio_Localizacion loc = Servicio_Localizacion.Instancia;
        if (loc == null) return;

        if (labelParticipar != null) labelParticipar.text = loc.Obtener("trivia.participar");
        if (labelNoParticipar != null) labelNoParticipar.text = loc.Obtener("trivia.no_participar");
        if (labelContinuar != null) labelContinuar.text = loc.Obtener("trivia.continuar");
    }

    public void RefrescarTextos()
    {
        Servicio_Localizacion loc = Servicio_Localizacion.Instancia;
        if (loc == null) return;

        RefrescarTextosFijos();

        switch (_estadoActual)
        {
            case Estado_Visual_Trivia.Decision:
                if (labelTitulo != null) labelTitulo.text = loc.Obtener("trivia.titulo");
                if (labelDetalle != null) labelDetalle.text = loc.Obtener("trivia.decision", _gananciaRonda);
                break;

            case Estado_Visual_Trivia.Cargando:
                if (labelTitulo != null) labelTitulo.text = loc.Obtener("trivia.titulo");
                if (labelDetalle != null) labelDetalle.text = loc.Obtener("trivia.cargando");
                break;

            case Estado_Visual_Trivia.Pregunta:
                if (labelTitulo != null) labelTitulo.text = loc.Obtener("trivia.titulo");
                break;

            case Estado_Visual_Trivia.Resultado:
                if (_esResultadoSinRiesgo)
                {
                    if (labelTitulo != null) labelTitulo.text = loc.Obtener("trivia.sin_riesgo");
                    if (labelDetalle != null) labelDetalle.text = loc.Obtener("trivia.sin_riesgo_detalle", _gananciaRonda);
                }
                else if (_esResultadoCorrecto)
                {
                    if (labelTitulo != null) labelTitulo.text = loc.Obtener("trivia.correcto");
                    if (labelDetalle != null) labelDetalle.text = loc.Obtener("trivia.correcto_detalle", _gananciaAnterior, _gananciaNueva);
                }
                else
                {
                    if (labelTitulo != null) labelTitulo.text = loc.Obtener("trivia.incorrecto");
                    if (labelDetalle != null) labelDetalle.text = loc.Obtener("trivia.incorrecto_detalle", _respuestaCorrecta, _gananciaAnterior, _gananciaNueva);
                }
                break;
        }
    }
}