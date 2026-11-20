using UnityEngine;

public class Vista_Moneda_Cobre : Vista_Entidades
{
    private GameObject referenciaVisual;

    private void Awake()
    {
        GameObject prefab = Resources.Load<GameObject>("Moneda_Cobre");

        if (prefab != null)
        {
            referenciaVisual = Instantiate(prefab, transform);
        }
        else
        {
            // Respaldo visual si todavía no creaste "Moneda_Cobre" en Assets/Resources
            referenciaVisual = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            referenciaVisual.transform.SetParent(transform);
            referenciaVisual.transform.localScale = new Vector3(0.5f, 0.1f, 0.5f);
            Destroy(referenciaVisual.GetComponent<Collider>());
            referenciaVisual.GetComponent<Renderer>().material.color = new Color(0.85f, 0.5f, 0.2f);
        }

        referenciaVisual.transform.localPosition = Vector3.zero;
        gameObject.SetActive(false); // Queda oculta hasta que muera el enemigo
    }

    public void Dibujar(int posX, int posZ)
    {
        transform.position = new Vector3(posX, 0.25f, posZ);
        gameObject.SetActive(true);
    }

    public override void Mover(int posX, int posZ)
    {
        // La moneda no se mueve
    }

    public override void Eliminar()
    {
        Destroy(gameObject); // Destruye el contenedor y el modelo visual hijo al agarrarla
    }
}


/*using UnityEngine;

public class Vista_Moneda_Cobre : Vista_Entidades
{
    private GameObject referenciaVisual;

    public Vista_Moneda_Cobre()
    {
        GameObject prefab = Resources.Load<GameObject>("Moneda_Cobre");

        if (prefab != null)
        {
            referenciaVisual = Instantiate(prefab);
        }
        else
        {
            // Respaldo visual por si todavía no creaste "Moneda_Cobre" en Assets/Resources
            referenciaVisual = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            referenciaVisual.transform.localScale = new Vector3(0.5f, 0.1f, 0.5f);
            Destroy(referenciaVisual.GetComponent<Collider>());
            referenciaVisual.GetComponent<Renderer>().material.color = new Color(0.85f, 0.5f, 0.2f);
        }

        referenciaVisual.SetActive(false);
    }

    public void Dibujar(int posX, int posZ)
    {
        if (referenciaVisual != null)
        {
            referenciaVisual.transform.position = new Vector3(posX, 0.25f, posZ);
            referenciaVisual.SetActive(true);
        }
    }

    public override void Mover(int posX, int posZ)
    {
        // La moneda no se mueve
    }

    public override void Eliminar()
    {
        if (referenciaVisual != null)
        {
            Destroy(referenciaVisual);
            referenciaVisual = null;
        }
    }
}*/