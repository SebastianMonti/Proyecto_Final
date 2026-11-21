using UnityEngine;

public class Vista_Enemigo_Fantasma : Vista_Enemigo
{
    [SerializeField, Min(0.1f)] private float intervaloMovimiento = 0.65f;
    [SerializeField, Min(0.5f)] private float tiempoVisible = 3f;
    [SerializeField, Min(0.5f)] private float tiempoInvisible = 2f;

    private Presentador_Enemigo_Fantasma presentador;
    private float tiempoAcumuladoMovimiento;
    private float tiempoAcumuladoVisibilidad;
    private bool estaInvisible;

    public void Conectar_Presentador(Presentador_Enemigo_Fantasma presentador)
    {
        this.presentador = presentador
            ?? throw new System.ArgumentNullException(nameof(presentador));
    }

    public override void Dibujar(int posX, int posZ)
    {
        transform.position = new Vector3(posX, 0f, posZ);

        if (modeloVisual == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Enemigo_Fantasma");

            if (prefab != null)
            {
                modeloVisual = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogWarning("No se encontró 'Enemigo_Fantasma' en Resources. Usando esfera blanca de respaldo.");
                modeloVisual = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                modeloVisual.transform.SetParent(transform);
                modeloVisual.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                modeloVisual.GetComponent<Renderer>().material.color = new Color(0.85f, 0.9f, 1f);
            }

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

        // Actualiza la posición aunque modeloVisual esté desactivado (invisible)
        transform.position = new Vector3(posX, 0f, posZ);
    }

    private void Update()
    {
        if (presentador == null) return;

        // 1. Ciclo de movimiento en la grilla
        tiempoAcumuladoMovimiento += Time.deltaTime;
        if (tiempoAcumuladoMovimiento >= intervaloMovimiento)
        {
            tiempoAcumuladoMovimiento = 0f;
            presentador.Mover();
        }

        // 2. Ciclo periódico de invisibilidad
        Actualizar_Visibilidad();
    }

    private void Actualizar_Visibilidad()
    {
        if (modeloVisual == null) return;

        tiempoAcumuladoVisibilidad += Time.deltaTime;
        float limiteActual = estaInvisible ? tiempoInvisible : tiempoVisible;

        if (tiempoAcumuladoVisibilidad >= limiteActual)
        {
            tiempoAcumuladoVisibilidad = 0f;
            estaInvisible = !estaInvisible;
            modeloVisual.SetActive(!estaInvisible);
        }
    }

    public override void Eliminar()
    {
        Destroy(gameObject);
    }
}