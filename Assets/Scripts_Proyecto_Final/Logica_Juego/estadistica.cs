using System;


public class Estadisticas
{
    // Atributos definidos en tu diagrama UML
    public int radio;
    public int daño;
    public float tiempo_extra;
    public float velocidad; // Tiempo de enfriamiento (cooldown) entre explosiones
    public float Suerte;
    public int monedas_actuales;

    // Costos y niveles preliminares para la tienda[cite: 10]
    private int nivel_radio = 1;
    private int costo_radio = 20;

    private int nivel_daño = 1;
    private int costo_daño = 25;

    private int nivel_tiempo = 1;
    private int costo_tiempo = 30;

    private int nivel_velocidad = 1;
    private int costo_velocidad = 35;

    private int nivel_suerte = 1;
    private int costo_suerte = 40;

    public Estadisticas()
    {
        // Valores base iniciales para arrancar una partida nueva (Ronda 1)
        this.radio = 1;
        this.daño = 10;
        this.tiempo_extra = 0f;
        this.velocidad = 2.0f; // 1 explosión cada 2 segundos inicialmente
        this.Suerte = 0.05f;   // 5% de probabilidad base
        this.monedas_actuales = 0;
    }

    // -----------------------------------------------------------------
    // MEJORAS DE TIENDA (Basado en tu UML y el enunciado)[cite: 10, 24]
    // -----------------------------------------------------------------

    public bool obtener_mejora_radio()
    {
        // 1. Verificar si tengo monedas suficientes
        if (monedas_actuales < costo_radio)
        {
            return false;
        }

        // 2. Decrementar la cantidad de monedas[cite: 24]
        monedas_actuales -= costo_radio;

        // 3. Aplicar mejora (aquí podés delegar a mejora_radio.Aplicar(this))[cite: 24]
        nivel_radio++;
        radio += 1; // Aumenta en 1 casilla el brazo de la explosión[cite: 10]

        // 4. Incremento exponencial del precio para la próxima compra[cite: 10]
        costo_radio = (int)Math.Round(costo_radio * 1.6f);

        return true;
    }

    public bool obtener_mejora_daño()
    {
        if (monedas_actuales < costo_daño) return false;

        monedas_actuales -= costo_daño;
        nivel_daño++;
        daño += 5; // Aumenta los puntos de vida que quita la explosión[cite: 10]
        costo_daño = (int)Math.Round(costo_daño * 1.6f);

        return true;
    }

    public bool obtener_mejora_tiempo()
    {
        if (monedas_actuales < costo_tiempo) return false;

        monedas_actuales -= costo_tiempo;
        nivel_tiempo++;
        tiempo_extra += 3.0f; // Agrega segundos al temporizador base de la ronda[cite: 10]
        costo_tiempo = (int)Math.Round(costo_tiempo * 1.6f);

        return true;
    }

    public bool obtener_mejora_velocidad()
    {
        // Límite mínimo para evitar enfriamiento 0 o negativo
        if (monedas_actuales < costo_velocidad || velocidad <= 0.25f) return false;

        monedas_actuales -= costo_velocidad;
        nivel_velocidad++;
        velocidad = Math.Max(0.25f, velocidad - 0.15f); // Reduce enfriamiento entre explosiones[cite: 10]
        costo_velocidad = (int)Math.Round(costo_velocidad * 1.7f);

        return true;
    }

    public bool obtener_mejora_suerte()
    {
        if (monedas_actuales < costo_suerte) return false;

        monedas_actuales -= costo_suerte;
        nivel_suerte++;
        Suerte = Math.Min(0.80f, Suerte + 0.05f); // Aumenta prob. de crítico y mejores monedas[cite: 10]
        costo_suerte = (int)Math.Round(costo_suerte * 1.75f);

        return true;
    }

    // -----------------------------------------------------------------
    // GESTIÓN DE ECONOMÍA E IMPUESTOS[cite: 10]
    // -----------------------------------------------------------------

    public void Sumar_Monedas(int cantidad)
    {
        monedas_actuales += cantidad;
    }

    public bool Pagar_Impuesto(int montoImpuesto)
    {
        if (monedas_actuales < montoImpuesto)
        {
            return false; // Bancarrota -> Game Over[cite: 10]
        }

        monedas_actuales -= montoImpuesto;
        return true;
    }

    // -----------------------------------------------------------------
    // GETTERS (Compatibles con Controlador_Explosiones y Generador_Monedas)
    // -----------------------------------------------------------------

    public int Obtener_Radio() => radio;
    public int Obtener_Daño() => daño;
    public float Obtener_Tiempo() => velocidad; // Cooldown usado por Controlador_Explosiones
    public float Obtener_Tiempo_Extra() => tiempo_extra;
    public float Obtener_Suerte() => Suerte;
    public int Obtener_Monedas() => monedas_actuales;

    public int Obtener_Costo_Radio() => costo_radio;
    public int Obtener_Costo_Daño() => costo_daño;
    public int Obtener_Costo_Tiempo() => costo_tiempo;
    public int Obtener_Costo_Velocidad() => costo_velocidad;
    public int Obtener_Costo_Suerte() => costo_suerte;
}
