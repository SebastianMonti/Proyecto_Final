using UnityEngine;
using UnityEngine.UIElements;

public class Vista_Personaje : Vista_Entidades
{
    [SerializeField] private GameObject prefabVisual;
    private GameObject referenciaVisual;
    private Presentador_Personaje presentador;

    public Vista_Personaje(Presentador_Personaje p)
    {
        presentador = p;
       
        referenciaVisual = Instantiate(Resources.Load("Jugador")) as GameObject;
        Debug.Log(referenciaVisual.gameObject.name);
        //Debug.Log(referenciaVisual.GetComponent<ControladorTeclas2>());
        //referenciaVisual.GetComponent<Controlador_Teclas>().Inciiar(p);

    }

    public void crear() 
    {
        Debug.Log(referenciaVisual.GetComponent<ControladorTeclas2>());
        referenciaVisual.GetComponent<ControladorTeclas2>();
    }

    public void Dibujar(int posX, int posZ)
    {
       

        Vector3 posicion = new Vector3(posX, 0, posZ);
        referenciaVisual.transform.position = posicion;

    }

    public override void Mover(int posX, int posZ)
    {
        Dibujar(posX, posZ);
    }

    public override void Eliminar()
    {
        if (referenciaVisual != null)
        {
            Destroy(referenciaVisual);
            referenciaVisual = null;
        }
    }
}
/*
using UnityEngine;

public class Vista_Personaje : Vista_Entidades
{
    private GameObject referenciaVisual;
    private Presentador_Entidades presentador;
    public Vista_Personaje(Presentador_Entidades pres)
    {
        presentador = pres;
        referenciaVisual = Instantiate(Resources.Load("Jugador")) as GameObject;
        referenciaVisual.GetComponent<ManejadorTeclas>().SetVistaJugador(this);
    }
    public override void Mover(int posX, int posZ)
    {
        transform.position = new Vector3(posX, 0, posZ);
    }

    public override void Eliminar()
    {
        Destroy(gameObject);
    }
}
*/