using System;
using System.Collections.Generic;

public class Generador_Monedas
{
    private Tablero tablero;
    private Random suerte;
    private Estadisticas estadisticas;
    private Func<Presentador_Moneda_Cobre> fabricaPresentadorCobre;

    private readonly List<Moneda> monedasActivas = new List<Moneda>();//para poder limpiar las q quedan activas

    public Generador_Monedas(
        Tablero tablero,
        Random suerte,
        Estadisticas estadisticas,
        Func<Presentador_Moneda_Cobre> fabricaPresentadorCobre = null)
    {
        this.tablero = tablero;
        this.suerte = suerte;
        this.estadisticas = estadisticas;
        this.fabricaPresentadorCobre = fabricaPresentadorCobre;
    }

    public void Generar_Monedas(Celda Celda_Enemigo_Asesinado, Enemigo Enemigo_Asesinado)
    {
        if (Celda_Enemigo_Asesinado == null || Enemigo_Asesinado == null)
        {
            return;
        }

        // 1. Pide las monedas que el enemigo creó en su constructor[cite: 17, 22]
        List<Moneda> lista_monedas = Enemigo_Asesinado.Obtener_Lista_Monedas();

        // 2. Aplica la estadística Suerte si corresponde
        Aplicar_Suerte(lista_monedas);

        // 3. Ubica cada moneda en una celda libre cercana y la dibuja[cite: 17]
        foreach (Moneda m in lista_monedas)
        {
            Celda celda_libre = tablero.Obtener_Celda_Libre(Celda_Enemigo_Asesinado);

            if (celda_libre != null)
            {
                m.Ubicar_En_Celda(celda_libre);
                m.Dibujar(celda_libre.Obtener_Posicion_X(), celda_libre.Obtener_Posicion_Z());
                monedasActivas.Add(m);
            }
        }
    }

    private void Aplicar_Suerte(List<Moneda> monedas)
    {
        if (estadisticas == null || monedas == null || monedas.Count == 0)
        {
            return;
        }

        if (suerte.NextDouble() < estadisticas.Obtener_Suerte())
        {
            monedas.Add(new Moneda_Cobre());
        }
    }

    public void Limpiar_Monedas_Activas()
    {
        foreach (Moneda m in monedasActivas)
        {
            if (m == null) continue;
            m.Atrapada();
        }
        monedasActivas.Clear();
    }
}