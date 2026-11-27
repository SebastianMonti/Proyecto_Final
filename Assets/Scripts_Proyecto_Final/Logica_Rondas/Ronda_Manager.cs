using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Fase { Batalla, Trivia, Tienda, Impuestos, FinJuego }

public class Ronda_Manager
{
    const float DURACION_BASE = 20f;
    public bool EsVictoria { get; private set; }
    readonly Cargador_Rondas cargador;
    readonly Estadisticas stats;
    readonly Controlador_Trivia trivia;
    public Tienda tienda;
    int ultimoSegundoMostrado = -1;
    public int Numero => numero;
    public Ronda Ronda_Actual => ronda;
    public Generador_Monedas generadorMonedas;

    Ronda ronda;
    int numero = 1;
    int saldoInicialRonda;
    public Fase Fase { get; private set; }


    public Ronda_Manager(Cargador_Rondas cargador, Estadisticas stats, Controlador_Trivia trivia, Tienda tienda)
    {
        this.cargador = cargador;
        this.stats = stats;
        this.trivia = trivia;
        this.tienda = tienda;
    }

    public void Iniciar_Partida()
    {
        numero = 1;
        EsVictoria = false;
        Iniciar_Ronda();
    }

    public void Iniciar_Ronda()
    {
        DatosRonda datos = cargador.ObtenerDatosRonda(numero);
        saldoInicialRonda = stats.Obtener_Monedas();
        ronda = new Ronda(datos, DURACION_BASE + stats.tiempo_extra);

        Fase = Fase.Batalla;
        Debug.Log($"Ronda {numero}: horda de {datos.CantidadHorda}, impuesto {datos.Impuesto}");
    
        Guardar_Progreso();
    }

    public void Actualizar(float dt)
    {
        if (Fase != Fase.Batalla || ronda == null) 
            return;

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
                generadorMonedas.Limpiar_Monedas_Activas();
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
        if (cargador.EsUltimaRonda(numero))
        {
            EsVictoria = true;
            Fase = Fase.FinJuego;
            return;
        }

        numero++;
        Iniciar_Ronda();
    }

    public void Terminar_Trivia()
    {
        stats.Sumar_Monedas(trivia.Ajuste());
        trivia.Cerrar();
        Fase = Fase.Tienda;
    }

    public void Terminar_Tienda()
    {
        if (numero % 5 == 0)
        {
            Fase = Fase.Impuestos;
            return;
        }

        Avanzar_Ronda();
    }

    public int Obtener_Impuesto_Actual()
    {
        return cargador.ObtenerDatosRonda(numero).Impuesto;
    }

    public bool Intentar_Pagar_Impuesto()
    {
        int impuesto = Obtener_Impuesto_Actual();

        if (stats.Obtener_Monedas() >= impuesto)
        {
            stats.Sumar_Monedas(-impuesto);
            Avanzar_Ronda();
            return true;
        }

        EsVictoria = false;
        Fase = Fase.FinJuego;
        Debug.Log("Bancarrota: no alcanzaron las monedas para pagar el impuesto.");
        return false;
    }
    void Guardar_Progreso()
    {
        Servicio_Guardado.Guardar(new Datos_A_Cargar()
        {
            ronda = numero,
            dinero = stats.Obtener_Monedas(),
            proximoImpuesto = Tienda.Monto_Proximo_Impuesto(numero),
            nivelesMejoras = tienda.Obtener_Niveles()
        });
    }

    public void Continuar_Partida(Datos_A_Cargar d)
    {
        numero = d.ronda;
        tienda.Restaurar_Niveles(d.nivelesMejoras);   
        stats.monedas_actuales = d.dinero;           
        Iniciar_Ronda();                              
    }

}