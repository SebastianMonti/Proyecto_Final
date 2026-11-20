
using UnityEngine;

public class Vista_Enemigo_Basico : Vista_Enemigo
{
    [SerializeField, Min(0.1f)] private float intervaloMovimiento = 0.7f;
    private Presentador_Enemigo_Basico presentador;
    private float tiempoAcumulado;

    public void Conectar_Presentador(Presentador_Enemigo_Basico presentador)
    {
        this.presentador = presentador
            ?? throw new System.ArgumentNullException(nameof(presentador));
    }

    public override void Dibujar(int posX, int posZ)
    {
        transform.position = new Vector3(posX, 0f, posZ);

        //if (modeloVisual == null)
        //{
             GameObject prefab = Resources.Load<GameObject>("Enemigo_Basico");
            // GameObject prefab = Instantiate(Resources.Load("Enemigo_Basico")) as GameObject;

        if (prefab != null)
        {
            modeloVisual = Instantiate(prefab, transform);
        }
        /*else
        {
            // Respaldo por si el prefab en Assets/Resources tiene otro nombre
            Debug.LogWarning("No se encontró 'Enemigo_Basico' en Resources. Usando cubo de respaldo.");
            modeloVisual = GameObject.CreatePrimitive(PrimitiveType.Cube);
            modeloVisual.transform.SetParent(transform);
            modeloVisual.GetComponent<Renderer>().material.color = Color.red;
        }
        */
            modeloVisual.transform.localPosition = Vector3.zero;
        //}
    }

    public override void Mover(int posX, int posZ)
    {
        if (modeloVisual == null)
        {
            Dibujar(posX, posZ);
            return;
        }

        transform.position = new Vector3(posX, 0f, posZ);
    }

    private void Update()
    {
        if (presentador == null) return;

        tiempoAcumulado += Time.deltaTime;
        if (tiempoAcumulado >= intervaloMovimiento)
        {
            tiempoAcumulado = 0f;
            presentador.Mover();
        }
    }

    public override void Eliminar()
    {
        Destroy(gameObject);
    }
}
/*using UnityEngine;

public class Vista_Enemigo_Basico : Vista_Enemigo
{
    [SerializeField] private GameObject prefabVisual;
    [SerializeField, Min(0.1f)] private float intervaloMovimiento = 1f;
    private GameObject referenciaVisual;
    private Presentador_Enemigo_Basico presentador;
    public Vista_Enemigo_Basico(Presentador_Enemigo_Basico presentador)
    {
        this.presentador = presentador;
    }
    public override void Dibujar(int posX, int posZ)
    {
        if (prefabVisual == null)
        {
            Debug.LogError("Asigna el prefab visual del enemigo básico.", this);
            return;
        }

        transform.position = new Vector3(posX, 0f, posZ);

        if (modeloVisual == null)
        {
            referenciaVisual = Instantiate(Resources.Load("Enemigo_Basico")) as GameObject;
            modeloVisual.transform.localPosition = Vector3.zero;
        }
    }
   
    public override void Mover(int posX, int posZ)
    {
        if (modeloVisual == null)
        {
            Dibujar(posX, posZ);
            return;
        }

        transform.position = new Vector3(posX, 0f, posZ);
    }

    public void Conectar_Presentador(Presentador_Enemigo_Basico presentador)
    {
        this.presentador = presentador; 
    }

    private void Start()
    {
        if (intervaloMovimiento <= 0f)
        {
            Debug.LogError("El intervalo de movimiento debe ser mayor que cero.", this);
            enabled = false;
        }
    }

    private  void Update()
    {
        if (presentador == null)
        {
            return;
        }

        tiempoAcumulado += Time.deltaTime;
        if (tiempoAcumulado >= intervaloMovimiento)
        {
            tiempoAcumulado = 0f;
            presentador.Mover();
        }
    }

    public override void Eliminar()
    {
        Destroy(gameObject);
    }

    private float tiempoAcumulado;
}
*/