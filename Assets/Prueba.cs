using UnityEngine;

public class Prueba : MonoBehaviour
{
    private Controlador_Explosiones controladorExplosiones;
    private Ronda_Manager manager;

    Controlador_Trivia trivia;
    Estado_Trivia ultimoEstado = Estado_Trivia.Inactiva;

    [SerializeField] private Vinculo_Vistas_Presentador vinculoVistas;//para q unity lo asigne desde el insp
    void Start()
    {
        Tablero tablero = new Tablero();
        Estadisticas estadisticas = new Estadisticas();
        Controlador_Entidades controlador_Entidades = new Controlador_Entidades(estadisticas);
        Generador_Monedas generadorMonedas = new Generador_Monedas(tablero, new System.Random(), estadisticas);

        // Personaje (su Vista_Personaje ya escucha el teclado)
        Celda celda = tablero.Obtener_Celda(4, 4);
        Personaje personaje = new Personaje(celda, tablero, controlador_Entidades);
        celda.Establecer_Entidad(personaje);

        Fabrica_Enemigos fabrica = new Fabrica_Enemigos(tablero, generadorMonedas, controlador_Entidades, personaje);
        Cargador_Rondas cargador = new Cargador_Rondas(fabrica, tablero);

        controladorExplosiones = new Controlador_Explosiones(
            estadisticas,
            personaje,
            tablero,
            controlador_Entidades
        );

        // Enemigo
        Generador_Monedas generador_Monedas = new Generador_Monedas(
            tablero,
            new System.Random(),
            estadisticas
        );
        /*  Celda celda_Enemigo1 = tablero.Obtener_Celda(6, 4); // Cerca del personaje para probar r�pido
          Enemigo_Basico enemigo1 = new Enemigo_Basico(
              celda_Enemigo1,
              tablero,
              generador_Monedas,
              controlador_Entidades
          );
        */

        trivia = new Controlador_Trivia(new Banco_Preguntas_Local());
        manager = new Ronda_Manager(cargador, estadisticas, trivia);

        if (vinculoVistas != null)
        {
            vinculoVistas.Inicializar(manager);
        }
        
       // manager.Iniciar_Partida();

        
    }

    void Update()
    {
        //controlador.Verificar_Tecla_Presionada();
        controladorExplosiones.Actualizar(Time.deltaTime);

        //para probar!
        if (manager != null)
        {
            manager.Actualizar(Time.deltaTime);
            if (manager.Fase == Fase.Trivia) Probar_Trivia();
        }
    }

    void Probar_Trivia()
    {
        if (trivia.Estado != ultimoEstado)
        {
            ultimoEstado = trivia.Estado;
            Avisar_Estado();
        }

        if (trivia.Estado == Estado_Trivia.Decidiendo)
        {
            if (Input.GetKeyDown(KeyCode.P)) trivia.Participar();
            if (Input.GetKeyDown(KeyCode.O)) trivia.No_Participar();
        }
        else if (trivia.Estado == Estado_Trivia.Respondiendo)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) trivia.Responder(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) trivia.Responder(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) trivia.Responder(2);
            if (Input.GetKeyDown(KeyCode.Alpha4)) trivia.Responder(3);
        }
        else if (trivia.Estado == Estado_Trivia.Resultado && Input.GetKeyDown(KeyCode.Return))
        {
            manager.Terminar_Trivia();
        }
    }

    void Avisar_Estado()
    {
        switch (trivia.Estado)
        {
            case Estado_Trivia.Decidiendo:
                Debug.Log($"[Trivia] Ganaste {trivia.Ganancias} esta ronda. P = participar, O = no participar");
                break;
            case Estado_Trivia.Respondiendo:
                MostrarPregunta();
                break;
            case Estado_Trivia.Resultado:
                Debug.Log($"[Trivia] Ganancias {trivia.Ganancias} -> {trivia.GananciasFinales} (ajuste {trivia.Ajuste()}). Enter para continuar");
                break;
        }
    }

    void MostrarPregunta()
    {
        var p = trivia.Pregunta_Actual;
        if (p != null) Debug.Log($"{p.texto}  1){p.opciones[0]} 2){p.opciones[1]} 3){p.opciones[2]} 4){p.opciones[3]}");
    }
}

