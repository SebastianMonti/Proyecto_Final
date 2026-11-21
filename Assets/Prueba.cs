using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Prueba : MonoBehaviour
{
    private Controlador_Teclas controlador;
    private Controlador_Explosiones controladorExplosiones;
    private Ronda_Manager manager;

    void Start()
    {   
        Tablero tablero = new Tablero();
        Estadisticas estadisticas = new Estadisticas();
        Controlador_Entidades controlador_Entidades = new Controlador_Entidades(estadisticas);
        Generador_Monedas generadorMonedas = new Generador_Monedas(tablero, new System.Random(), estadisticas);
        Fabrica_Enemigos fabrica = new Fabrica_Enemigos(tablero, generadorMonedas, controlador_Entidades);
        Cargador_Rondas cargador = new Cargador_Rondas(fabrica, tablero);

        // Personaje
        Celda celda = tablero.Obtener_Celda(4, 4);
        Personaje personaje = new Personaje(celda, tablero, controlador_Entidades); // Si tu Personaje recibe controlador_Entidades, pasáselo acá
        celda.Establecer_Entidad(personaje);
        controlador = new Controlador_Teclas(personaje.obtener_presentador());

        // Explosiones automáticas
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
      /*  Celda celda_Enemigo1 = tablero.Obtener_Celda(6, 4); // Cerca del personaje para probar rápido
        Enemigo_Basico enemigo1 = new Enemigo_Basico(
            celda_Enemigo1,
            tablero,
            generador_Monedas,
            controlador_Entidades
        );
      */

        manager = new Ronda_Manager(cargador, estadisticas);
        manager.Iniciar_Partida();
    }

    void Update()
    {
        controlador.Verificar_Tecla_Presionada();
        controladorExplosiones.Actualizar(Time.deltaTime);
        manager.Actualizar(Time.deltaTime);
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Prueba : MonoBehaviour
{
    Controlador_Teclas controlador;
    // Start is called before the first frame update
    void Start()
    {
        Controlador_Entidades controlador_Entidades = new Controlador_Entidades();

        Tablero tablero = new Tablero(); // Crear un tablero de 10x10
        Celda celda = tablero.Obtener_Celda(4, 4);
        Personaje personaje = new Personaje(celda, tablero, controlador_Entidades); 
        celda.Establecer_Entidad(personaje);
        controlador = new Controlador_Teclas(personaje.obtener_presentador());


        //enemigo
        Generador_Monedas generador_Monedas = new Generador_Monedas(tablero,null,null,null);
        
        Celda celda_Enemigo1 = tablero.Obtener_Celda(8, 8);
        Enemigo_Basico enemigo1 = new Enemigo_Basico(celda_Enemigo1, tablero, generador_Monedas, controlador_Entidades);
        celda_Enemigo1.Establecer_Entidad(enemigo1);


    }

    // Update is called once per frame
    void Update()
    {
        controlador.Verificar_Tecla_Presionada();
    }
}
*/
