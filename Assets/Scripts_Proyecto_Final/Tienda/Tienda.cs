using System.Collections.Generic;

public class Tienda
{
    readonly List<Mejora> mejoras;
    readonly Estadisticas stats;
    public IReadOnlyList<Mejora> Mejoras => mejoras;
    public Tienda(Estadisticas stats)
    {
        this.stats = stats;
        mejoras = new List<Mejora>
        {
            new Mejora_Radio(),
            new Mejora_Tiempo(),
            new Mejora_Velocidad(),
            new Mejora_Danio(),
            new Mejora_Suerte()
        };
    }

    public bool Puede_Comprar(int indice, int saldo)
    {
        return saldo >= mejoras[indice].Precio;
    }

    // Devuelve lo que costó (0 si no alcanzó el saldo). Quien la llama descuenta el saldo.
    public int Comprar(int indice, int saldo)
    {
        if (!Puede_Comprar(indice, saldo)) 
            return 0;

        Mejora m = mejoras[indice];
        int costo = m.Precio;      // el precio ANTES de subir de nivel
        m.Subir_Nivel(stats);
        return costo;
    }

    // Información de impuestos para la pantalla. 'ronda' = la que se acaba de jugar.
    public static int Ronda_Del_Proximo_Impuesto(int ronda)
    {
        return ((ronda + 4) / 5) * 5;
    }
    public static int Rondas_Restantes(int ronda)
    {
        return Ronda_Del_Proximo_Impuesto(ronda) - ronda;
    }
    public static int Monto_Proximo_Impuesto(int ronda)
    {
        return Cargador_Rondas.CalcularImpuesto(Ronda_Del_Proximo_Impuesto(ronda) / 5);
    }
    // Persistencia: solo hay que guardar los niveles
    public int[] Obtener_Niveles()
    {
        var niveles = new int[mejoras.Count];
        for (int i = 0; i < mejoras.Count; i++) niveles[i] = mejoras[i].Nivel;
        return niveles;
    }

    public void Restaurar_Niveles(int[] niveles)
    {
        for (int i = 0; i < mejoras.Count && i < niveles.Length; i++)
            mejoras[i].Restaurar(niveles[i], stats);
    }
}