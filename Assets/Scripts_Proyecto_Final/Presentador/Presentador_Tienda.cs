using System;

public class Presentador_Tienda
{
    private static readonly string[] IdsMejoras = { "radio", "tiempo", "velocidad", "danio", "suerte" };
    private static readonly string[] ClavesNombre = { "tienda.radio", "tienda.tiempo", "tienda.velocidad", "tienda.danio", "tienda.suerte" };

    private readonly Tienda tienda;
    private readonly Estadisticas stats;
    private readonly Ronda_Manager manager;
    private readonly Vista_Tienda vista;

    private bool estabaEnTienda;

    public Presentador_Tienda(Tienda tienda, Estadisticas stats, Ronda_Manager manager, Vista_Tienda vista)
    {
        this.tienda = tienda ?? throw new ArgumentNullException(nameof(tienda));
        this.stats = stats ?? throw new ArgumentNullException(nameof(stats));
        this.manager = manager ?? throw new ArgumentNullException(nameof(manager));
        this.vista = vista ?? throw new ArgumentNullException(nameof(vista));

        this.vista.OnComprarMejoraSolicitada += ManejarCompra;
        this.vista.OnContinuarSolicitado += ManejarContinuar;
        this.vista.Ocultar();
    }

    public void Actualizar()
    {
        bool enTienda = manager.Fase == Fase.Tienda;

        if (enTienda && !estabaEnTienda)
        {
            estabaEnTienda = true;
            vista.Mostrar(
                ConstruirDatosMejoras(),
                Tienda.Monto_Proximo_Impuesto(manager.Numero),
                Tienda.Rondas_Restantes(manager.Numero)
            );
        }
        else if (!enTienda && estabaEnTienda)
        {
            estabaEnTienda = false;
            vista.Ocultar();
        }
    }

    private void ManejarCompra(string idMejora)
    {
        if (manager.Fase != Fase.Tienda) return;

        int indice = Array.IndexOf(IdsMejoras, idMejora);
        if (indice < 0) return;

        int saldoActual = stats.Obtener_Monedas();
        int costo = tienda.Comprar(indice, saldoActual);

        if (costo > 0)
        {
            stats.Sumar_Monedas(-costo);
            vista.ActualizarDatos(
                ConstruirDatosMejoras(),
                Tienda.Monto_Proximo_Impuesto(manager.Numero),
                Tienda.Rondas_Restantes(manager.Numero)
            );
        }
    }

    private void ManejarContinuar()
    {
        if (manager.Fase != Fase.Tienda) return;

        estabaEnTienda = false;
        vista.Ocultar();
        manager.Terminar_Tienda();
    }

    private Datos_Mejora_Tienda[] ConstruirDatosMejoras()
    {
        int saldo = stats.Obtener_Monedas();
        var lista = tienda.Mejoras;
        var datos = new Datos_Mejora_Tienda[lista.Count];

        for (int i = 0; i < lista.Count; i++)
        {
            Mejora m = lista[i];
            datos[i] = new Datos_Mejora_Tienda
            {
                idMejora = i < IdsMejoras.Length ? IdsMejoras[i] : m.Nombre.ToLower(),
                claveNombre = i < ClavesNombre.Length ? ClavesNombre[i] : string.Empty,
                nivel = m.Nivel,
                precio = m.Precio,
                puedeComprar = tienda.Puede_Comprar(i, saldo)
            };
        }

        return datos;
    }

    public void Dispose()
    {
        if (vista != null)
        {
            vista.OnComprarMejoraSolicitada -= ManejarCompra;
            vista.OnContinuarSolicitado -= ManejarContinuar;
        }
    }
}