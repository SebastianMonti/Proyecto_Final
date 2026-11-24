using UnityEngine;

public class Vista_Personaje : Vista_Entidades
{
    private GameObject modeloVisual;
    private Presentador_Personaje presentador;
    private Controlador_Teclas controladorTeclas;

    private void Awake()
    {
        GameObject prefab = Resources.Load<GameObject>("Jugador");

        if (prefab != null)
        {
            modeloVisual = Instantiate(prefab, transform);
            modeloVisual.transform.localPosition = Vector3.zero;
        }
        else
        {
            Debug.LogError("No se encontró el prefab 'Jugador' en Assets/Resources.", this);
        }
    }

    public void Conectar_Presentador(Presentador_Personaje presentador)
    {
        this.presentador = presentador
            ?? throw new System.ArgumentNullException(nameof(presentador));

        controladorTeclas = new Controlador_Teclas(this.presentador);
    }

    private void Update()
    {
        controladorTeclas?.Verificar_Tecla_Presionada();
    }

    public void Dibujar(int posX, int posZ)
    {
        transform.position = new Vector3(posX, 0f, posZ);
    }

    public override void Mover(int posX, int posZ)
    {
        Dibujar(posX, posZ);
    }

    public override void Eliminar()
    {
        Destroy(gameObject);
    }
}

