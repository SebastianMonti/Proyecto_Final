using UnityEngine;
//llamar vista controles

/*public class Vista_Juego : MonoBehaviour
{
    [SerializeField] private GameObject prefabPersonaje;
    [SerializeField] private GameObject prefabMoneda;
    [SerializeField] private Vector2Int posicionInicial = new Vector2Int(10, 10);
    [SerializeField] private Vector2Int posicionMoneda = new Vector2Int(15, 10);

    private Controlador_Teclas controlador_teclas;

    private void Start()
    {
        if (prefabPersonaje == null)
        {
            Debug.LogError("Asigna el prefab del personaje en Vista_Juego.", this);
            enabled = false;
            return;
        }

        if (prefabMoneda == null)
        {
            Debug.LogError("Asigna el prefab de la moneda en Vista_Juego.", this);
            enabled = false;
            return;
        }

        Tablero tablero = new Tablero();
        Celda celdaInicial = tablero.Obtener_Celda(posicionInicial.x, posicionInicial.y);
        if (celdaInicial == null)
        {
            Debug.LogError("La posición inicial del personaje está fuera del tablero.", this);
            enabled = false;
            return;
        }

        Celda celdaMoneda = tablero.Obtener_Celda(posicionMoneda.x, posicionMoneda.y);
        if (celdaMoneda == null || celdaMoneda == celdaInicial)
        {
            Debug.LogError("La moneda debe estar en una celda válida y distinta de la posición inicial.", this);
            enabled = false;
            return;
        }

        Personaje personaje = new Personaje(celdaInicial);
        personaje.Actualizar_Posicion(celdaInicial);

        GameObject instanciaPersonaje = Instantiate(
            prefabPersonaje,
            new Vector3(posicionInicial.x, 0, posicionInicial.y),
            Quaternion.identity);
        Vista_Personaje vistaPersonaje = instanciaPersonaje.GetComponent<Vista_Personaje>();
        if (vistaPersonaje == null)
        {
            vistaPersonaje = instanciaPersonaje.AddComponent<Vista_Personaje>();
        }

        GameObject instanciaMoneda = Instantiate(
            prefabMoneda,
            new Vector3(posicionMoneda.x, 0, posicionMoneda.y),
            Quaternion.identity);
        Vista_Moneda_Cobre vistaMoneda = instanciaMoneda.GetComponent<Vista_Moneda_Cobre>();
        if (vistaMoneda == null)
        {
            vistaMoneda = instanciaMoneda.AddComponent<Vista_Moneda_Cobre>();
        }

        Presentador_Moneda_Cobre presentadorMoneda =
            new Presentador_Moneda_Cobre();
        Moneda_Cobre moneda = new Moneda_Cobre(celdaMoneda);
        presentadorMoneda.dibujar(posicionMoneda.x, posicionMoneda.y);

        Controlador_Entidades controladorEntidades = new Controlador_Entidades();
        Controlador_Movimiento controladorMovimiento =
            new Controlador_Movimiento(tablero, personaje, controladorEntidades);
        Ronda ronda = new Ronda(personaje, controladorMovimiento);
        Presentador_Personaje presentador =
            new Presentador_Personaje(controladorMovimiento);
        controlador_teclas = new Controlador_Teclas(presentador);
    }

    private void Update()
    {
        if (controlador_teclas != null)
        {
            controlador_teclas.Verificar_Tecla_Presionada();
        }
    }
}*/
