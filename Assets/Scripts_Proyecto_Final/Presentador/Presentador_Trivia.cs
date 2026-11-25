using UnityEngine;

public class Presentador_Trivia
{
    private readonly Controlador_Trivia trivia;
    private readonly Ronda_Manager manager;
    private readonly Vista_Trivia vista;
    private Estado_Trivia ultimoEstado = Estado_Trivia.Inactiva;

    public Presentador_Trivia(Controlador_Trivia trivia, Ronda_Manager manager, Vista_Trivia vista)
    {
        this.trivia = trivia;
        this.manager = manager;
        this.vista = vista;
        vista.Establecer_Presentador(this);
        vista.Ocultar();
    }

    // Se llama en cada frame desde Prueba.Update
    public void Actualizar()
    {
        if (trivia.Estado == ultimoEstado)
            return;

        ultimoEstado = trivia.Estado;

        switch (trivia.Estado)
        {
            case Estado_Trivia.Inactiva:
                vista.Ocultar();
                break;

            case Estado_Trivia.Decidiendo:
                vista.MostrarDecision(trivia.Ganancias);
                break;

            case Estado_Trivia.Cargando:
                vista.MostrarCargando();
                break;

            case Estado_Trivia.Respondiendo:
                vista.MostrarPregunta(trivia.Pregunta_Actual);
                break;

            case Estado_Trivia.Resultado:
                Armar_Resultado();
                break;
        }
    }

    private void Armar_Resultado()
    {
        int g = trivia.Ganancias;
        int f = trivia.GananciasFinales;

        if (!trivia.Participo)
        {
            vista.MostrarResultadoSinRiesgo(g);
        }
        else if (trivia.Acerto)
        {
            vista.Mostrar_Resultado(
                acerto: true,
                respuestaCorrecta: string.Empty,
                gananciaAnterior: g,
                gananciaNueva: f
            );
        }
        else
        {
            Pregunta p = trivia.Pregunta_Actual;
            string respuestaCorrecta = (p != null && p.opciones != null && p.correcta >= 0 && p.correcta < p.opciones.Length)
                ? p.opciones[p.correcta]
                : string.Empty;

            vista.Mostrar_Resultado(
                acerto: false,
                respuestaCorrecta: respuestaCorrecta,
                gananciaAnterior: g,
                gananciaNueva: f
            );
        }
    }

    // Llamados por los botones de la vista
    public void Al_Participar()
    {
        trivia.Participar();
    }

    public void Al_No_Participar()
    {
        trivia.No_Participar();
    }

    public void Al_Responder(int indice)
    {
        trivia.Responder(indice);
    }

    public void Al_Continuar()
    {
        if (trivia.Estado == Estado_Trivia.Resultado)
        {
            manager.Terminar_Trivia();
        }
    }
}