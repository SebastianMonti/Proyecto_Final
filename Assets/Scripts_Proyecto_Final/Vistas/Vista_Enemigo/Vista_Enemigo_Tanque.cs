using UnityEngine;

public class Vista_Enemigo_Tanque : Vista_Enemigo
{
    [SerializeField, Min(0.1f)] private float intervaloMovimiento = 1.4f; // El doble de lento que el básico (0.7f)
    private Presentador_Enemigo_Tanque presentador;
    private float tiempoAcumulado;

    public void Conectar_Presentador(Presentador_Enemigo_Tanque presentador)
    {
        this.presentador = presentador
            ?? throw new System.ArgumentNullException(nameof(presentador));
    }

    public override void Dibujar(int posX, int posZ)
    {
        transform.position = new Vector3(posX, 0f, posZ);

        if (modeloVisual == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Enemigo_Tanque");

            if (prefab != null)
            {
                modeloVisual = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogWarning("No se encontró 'Enemigo_Tanque' en Resources. Usando cubo pesado de respaldo.");
                modeloVisual = GameObject.CreatePrimitive(PrimitiveType.Cube);
                modeloVisual.transform.SetParent(transform);
                modeloVisual.transform.localScale = new Vector3(0.95f, 1.1f, 0.95f);
                modeloVisual.GetComponent<Renderer>().material.color = new Color(0.25f, 0.25f, 0.35f);
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