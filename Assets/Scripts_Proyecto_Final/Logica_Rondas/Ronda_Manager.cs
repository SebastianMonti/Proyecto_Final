using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Fase { Batalla, Trivia, Tienda, Impuestos, FinJuego }

public class Ronda_Manager
{
    const float DURACION_BASE = 20f;

    readonly Cargador_Rondas cargador;
    readonly Estadisticas stats;
    readonly Controlador_Trivia trivia;
    int ultimoSegundoMostrado = -1;
    public int Numero => numero;
    public Ronda Ronda_Actual => ronda;

    Ronda ronda;
    int numero = 1;
    int saldoInicialRonda;
    public Fase Fase { get; private set; }

    public Ronda_Manager(Cargador_Rondas cargador, Estadisticas stats, Controlador_Trivia trivia)
    {
        this.cargador = cargador;
        this.stats = stats;
        this.trivia = trivia;
    }

    public void Iniciar_Partida()
    {
        numero = 1;
        Iniciar_Ronda();
    }

    public void Iniciar_Ronda()
    {
        DatosRonda datos = cargador.ObtenerDatosRonda(numero);
        saldoInicialRonda = stats.Obtener_Monedas();
        ronda = new Ronda(datos, DURACION_BASE + stats.tiempo_extra);

        Fase = Fase.Batalla;
        Debug.Log($"Ronda {numero}: horda de {datos.CantidadHorda}, impuesto {datos.Impuesto}");
    }

    public void Actualizar(float dt)
    {
        if (Fase != Fase.Batalla) return;

        ronda.Actualizar_Tiempo(dt);
        int segundos = Mathf.CeilToInt(ronda.TiempoRestante);
        if (segundos != ultimoSegundoMostrado)
        {
            ultimoSegundoMostrado = segundos;
            Debug.Log($"[Ronda {numero}] Tiempo: {segundos}s | Vivos: {ronda.EnemigosVivos}");
        }

        
        
            if (ronda.Terminada)
            {
                ronda.Limpiar_Enemigos();
                Fase = Fase.Trivia;
                int ganancias = stats.Obtener_Monedas() -saldoInicialRonda;   // lo ganado en la ronda
                trivia.Iniciar(ganancias, numero);
                return;
            }
        

        if (ronda.SinEnemigos)
        {
            ronda.Agregar_Horda(cargador.ObtenerDatosRonda(numero).horda);
            Debug.Log("Horda nueva");
        }
    }

    // El enemigo avisa cuando muere
    public void Enemigo_Murio(Enemigo e)
    {
        ronda.Quitar(e);
        if (ronda.SinEnemigos && !ronda.Terminada)
        {
            var nueva = cargador.ObtenerDatosRonda(numero).horda;

            ronda.Agregar_Horda(nueva);
        }
    }

    public void Avanzar_Ronda()
    {
        if (cargador.EsUltimaRonda(numero)) { Fase = Fase.FinJuego; return; }
        numero++;
        Iniciar_Ronda();
    }

    public void Terminar_Trivia()
    {
        stats.Sumar_Monedas(trivia.Ajuste());
        trivia.Cerrar();
        Avanzar_Ronda();     // más adelante acá va la tienda
    }

}