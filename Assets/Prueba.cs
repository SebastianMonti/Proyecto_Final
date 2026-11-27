using UnityEngine;
using UnityEngine.SceneManagement;


public class Prueba : MonoBehaviour
{
    [SerializeField] private Vista_Tiempo_Ronda vistaTiempoRonda;

    private Controlador_Explosiones controladorExplosiones;
    private Ronda_Manager manager;
    private Presentador_Trivia presentadorTrivia;
    private Presentador_Menu presentadorMenu;
    private Presentador_Tienda presentadorTienda;
    private Presentador_Impuestos presentadorImpuestos;
    private Presentador_Fin_Juego presentadorFinJuego;
    private Presentador_Jugando presentadorJugando;

    void Start()
    {
        if (Servicio_Localizacion.Instancia == null)
        {
            GameObject locObj = new GameObject("Servicio_Localizacion");
            locObj.AddComponent<Servicio_Localizacion>();
            DontDestroyOnLoad(locObj);
        }

        Tablero tablero = new Tablero();
        Estadisticas estadisticas = new Estadisticas();
        Controlador_Entidades controlador_Entidades = new Controlador_Entidades(estadisticas);
        Generador_Monedas generadorMonedas = new Generador_Monedas(tablero, new System.Random(), estadisticas);

        Celda celda = tablero.Obtener_Celda(4, 4);
        Personaje personaje = new Personaje(celda, tablero, controlador_Entidades);
        celda.Establecer_Entidad(personaje);

        controladorExplosiones = new Controlador_Explosiones(estadisticas, personaje, tablero, controlador_Entidades);
        controladorExplosiones.Pausado = true;

        Fabrica_Enemigos fabrica = new Fabrica_Enemigos(tablero, generadorMonedas, controlador_Entidades, personaje);
        Cargador_Rondas cargador = new Cargador_Rondas(fabrica, tablero);
        Controlador_Trivia trivia = new Controlador_Trivia(new Banco_Preguntas_Local());

        Tienda tienda = new Tienda(estadisticas);                       // UNA sola tienda
        manager = new Ronda_Manager(cargador, estadisticas, trivia, tienda);
        manager.generadorMonedas = generadorMonedas;

        // Menú
        GameObject prefabMenu = Resources.Load<GameObject>("Vista_Esqueleto/Menu_Principal");
        if (prefabMenu != null)
        {
            GameObject menuObj = Instantiate(prefabMenu);
            Canvas canvasMenu = menuObj.GetComponentInChildren<Canvas>(true);
            if (canvasMenu != null) canvasMenu.sortingOrder = 100;
            Vista_Menu vistaMenu = menuObj.GetComponentInChildren<Vista_Menu>(true);
            Vista_Seleccionar_Idioma vistaIdioma = menuObj.GetComponentInChildren<Vista_Seleccionar_Idioma>(true);
            presentadorMenu = new Presentador_Menu(vistaMenu, vistaIdioma, manager);
        }

        // HUD de batalla
        GameObject prefabBatalla = Resources.Load<GameObject>("Vista_Esqueleto/Batalla");
        if (prefabBatalla != null)
        {
            GameObject batallaObj = Instantiate(prefabBatalla);
            Vista_Tiempo_Ronda vistaBatalla = batallaObj.GetComponentInChildren<Vista_Tiempo_Ronda>(true);
            if (vistaBatalla != null)
                presentadorJugando = new Presentador_Jugando(manager, estadisticas, vistaBatalla);
            else
                Debug.LogWarning("El prefab Batalla no tiene Vista_Tiempo_Ronda.");
        }
        else Debug.LogError("No se encontró Resources/Vista_Esqueleto/Batalla");

        // Trivia
        GameObject prefabTrivia = Resources.Load<GameObject>("Vista_Esqueleto/Panel_Trivia");
        if (prefabTrivia != null)
        {
            GameObject triviaObj = Instantiate(prefabTrivia);
            Vista_Trivia vistaTrivia = triviaObj.GetComponentInChildren<Vista_Trivia>(true);
            presentadorTrivia = new Presentador_Trivia(trivia, manager, vistaTrivia);
        }

        // Tienda (misma instancia de arriba)
        GameObject prefabTienda = Resources.Load<GameObject>("Vista_Esqueleto/Panel_Tienda");
        if (prefabTienda != null)
        {
            GameObject tiendaObj = Instantiate(prefabTienda);
            Vista_Tienda vistaTienda = tiendaObj.GetComponentInChildren<Vista_Tienda>(true);
            presentadorTienda = new Presentador_Tienda(tienda, estadisticas, manager, vistaTienda);
        }
        else Debug.LogError("No se encontró Resources/Vista_Esqueleto/Panel_Tienda");

        // Impuestos
        GameObject prefabImpuestos = Resources.Load<GameObject>("Vista_Esqueleto/Panel_Impuestos");
        if (prefabImpuestos != null)
        {
            GameObject impuestosObj = Instantiate(prefabImpuestos);
            Vista_Impuestos vistaImpuestos = impuestosObj.GetComponentInChildren<Vista_Impuestos>(true);
            presentadorImpuestos = new Presentador_Impuestos(manager, vistaImpuestos);
        }
        else Debug.LogError("No se encontró Resources/Vista_Esqueleto/Panel_Impuestos");

        // Fin de juego
        GameObject prefabFinJuego = Resources.Load<GameObject>("Vista_Esqueleto/Panel_Fin_Juego");
        if (prefabFinJuego != null)
        {
            GameObject finObj = Instantiate(prefabFinJuego);
            Vista_Fin_Juego vistaFinJuego = finObj.GetComponentInChildren<Vista_Fin_Juego>(true);
            presentadorFinJuego = new Presentador_Fin_Juego(
                manager, vistaFinJuego,
                () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        }
        else Debug.LogError("No se encontró Resources/Vista_Esqueleto/Panel_Fin_Juego");
    }
    void Update()
    {
        manager.Actualizar(Time.deltaTime);

        bool enBatalla = manager.Fase == Fase.Batalla;
        controladorExplosiones.Pausado = !enBatalla;

        if (enBatalla)
        {
            controladorExplosiones.Actualizar(Time.deltaTime);
        }

        presentadorJugando?.Actualizar();
        presentadorTrivia?.Actualizar();
        presentadorTienda?.Actualizar();
        presentadorImpuestos?.Actualizar();
        presentadorFinJuego?.Actualizar();
    }

    private void OnDestroy()
    {
        presentadorMenu?.Dispose();
        presentadorTienda?.Dispose();
        presentadorImpuestos?.Dispose();
        presentadorFinJuego?.Dispose();
    }
}


/*using UnityEngine;

public class Prueba : MonoBehaviour
{
    private Controlador_Explosiones controladorExplosiones;
    private Ronda_Manager manager;
    private Presentador_Trivia presentadorTrivia;
    private Presentador_Menu presentadorMenu;

    void Start()
    {
        if (Servicio_Localizacion.Instancia == null)
        {
            new GameObject("Servicio_Localizacion").AddComponent<Servicio_Localizacion>();
        }

        Tablero tablero = new Tablero();
        Estadisticas estadisticas = new Estadisticas();
        Controlador_Entidades controlador_Entidades = new Controlador_Entidades(estadisticas);
        Generador_Monedas generadorMonedas = new Generador_Monedas(tablero, new System.Random(), estadisticas);

        Celda celda = tablero.Obtener_Celda(4, 4);
        Personaje personaje = new Personaje(celda, tablero, controlador_Entidades);
        celda.Establecer_Entidad(personaje);

        controladorExplosiones = new Controlador_Explosiones(
            estadisticas,
            personaje,
            tablero,
            controlador_Entidades
        );
        controladorExplosiones.Pausado = true;

        Fabrica_Enemigos fabrica = new Fabrica_Enemigos(tablero, generadorMonedas, controlador_Entidades, personaje);
        Cargador_Rondas cargador = new Cargador_Rondas(fabrica, tablero);

        Controlador_Trivia trivia = new Controlador_Trivia(new Banco_Preguntas_Local());
        manager = new Ronda_Manager(cargador, estadisticas, trivia);

        // 1. Instanciar Menu_Principal desde Resources/Vista_Esqueleto
        GameObject prefabMenu = Resources.Load<GameObject>("Vista_Esqueleto/Menu_Principal");
        if (prefabMenu != null)
        {
            GameObject menuObj = Instantiate(prefabMenu);
            Vista_Menu vistaMenu = menuObj.GetComponentInChildren<Vista_Menu>(true);
            Vista_Seleccionar_Idioma vistaIdioma = menuObj.GetComponentInChildren<Vista_Seleccionar_Idioma>(true);

            presentadorMenu = new Presentador_Menu(vistaMenu, vistaIdioma, manager);
            presentadorMenu.OnPartidaIniciada += () => controladorExplosiones.Pausado = false;
        }
        else
        {
            Debug.LogError("No se encontró Resources/Vista_Esqueleto/Menu_Principal");
        }

        // 2. Instanciar Panel_Trivia desde Resources/Vista_Esqueleto
        GameObject prefabTrivia = Resources.Load<GameObject>("Vista_Esqueleto/Panel_Trivia");
        if (prefabTrivia != null)
        {
            GameObject triviaObj = Instantiate(prefabTrivia);
            Vista_Trivia vistaTrivia = triviaObj.GetComponentInChildren<Vista_Trivia>(true);

            presentadorTrivia = new Presentador_Trivia(trivia, manager, vistaTrivia);
        }
        else
        {
            Debug.LogError("No se encontró Resources/Vista_Esqueleto/Panel_Trivia");
        }
    }

    void Update()
    {
        manager.Actualizar(Time.deltaTime);
        presentadorTrivia?.Actualizar();

        if (manager.Fase != Fase.Trivia)
        {
            controladorExplosiones.Actualizar(Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        presentadorMenu?.Dispose();
    }
}

/* 
 * using UnityEngine;

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
*/
