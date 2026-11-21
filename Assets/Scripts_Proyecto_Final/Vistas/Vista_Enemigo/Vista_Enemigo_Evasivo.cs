using UnityEngine;

public class Vista_Enemigo_Evasivo : Vista_Enemigo
{
    [SerializeField, Min(0.1f)] private float intervaloMovimiento = 0.45f; // Más rápido que el básico (0.7f)
    private Presentador_Enemigo_Evasivo presentador;
    private float tiempoAcumulado;

    public void Conectar_Presentador(Presentador_Enemigo_Evasivo presentador)
    {
        this.presentador = presentador
            ?? throw new System.ArgumentNullException(nameof(presentador));
    }

    public override void Dibujar(int posX, int posZ)
    {
        transform.position = new Vector3(posX, 0f, posZ);

        if (modeloVisual == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Enemigo_Evasivo");

            if (prefab != null)
            {
                modeloVisual = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogWarning("No se encontró 'Enemigo_Evasivo' en Resources. Usando cápsula azul de respaldo.");
                modeloVisual = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                modeloVisual.transform.SetParent(transform);
                modeloVisual.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                modeloVisual.GetComponent<Renderer>().material.color = Color.cyan;
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