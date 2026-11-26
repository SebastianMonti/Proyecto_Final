using System;

public class Presentador_Impuestos
{
    private readonly Ronda_Manager manager;
    private readonly Vista_Impuestos vista;

    private bool estabaEnImpuestos;

    public Presentador_Impuestos(Ronda_Manager manager, Vista_Impuestos vista)
    {
        this.manager = manager ?? throw new ArgumentNullException(nameof(manager));
        this.vista = vista ?? throw new ArgumentNullException(nameof(vista));

        this.vista.OnPagarSolicitado += ManejarPago;
        this.vista.Ocultar();
    }

    public void Actualizar()
    {
        bool enImpuestos = manager.Fase == Fase.Impuestos;

        if (enImpuestos && !estabaEnImpuestos)
        {
            estabaEnImpuestos = true;
            vista.Mostrar(manager.Obtener_Impuesto_Actual());
        }
        else if (!enImpuestos && estabaEnImpuestos)
        {
            estabaEnImpuestos = false;
            vista.Ocultar();
        }
    }

    private void ManejarPago()
    {
        if (manager.Fase != Fase.Impuestos) return;

        estabaEnImpuestos = false;
        vista.Ocultar();
        manager.Intentar_Pagar_Impuesto();
    }

    public void Dispose()
    {
        if (vista != null)
        {
            vista.OnPagarSolicitado -= ManejarPago;
        }
    }
}