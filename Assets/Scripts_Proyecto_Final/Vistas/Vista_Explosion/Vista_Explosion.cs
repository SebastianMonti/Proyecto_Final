using UnityEngine;

public class Vista_Explosion : Vista_Entidades
{
    private GameObject referenciaVisual;
    private float duracionVisual = 0.25f;

    public Vista_Explosion()
    {
        GameObject prefab = Resources.Load<GameObject>("Explosion");

        if (prefab != null)
        {
            referenciaVisual = Instantiate(prefab);
        }
        else
        {
            // Respaldo visual por si todavía no creaste "Explosion" en Assets/Resources
            referenciaVisual = GameObject.CreatePrimitive(PrimitiveType.Cube);
            referenciaVisual.transform.localScale = new Vector3(0.9f, 0.35f, 0.9f);
            Destroy(referenciaVisual.GetComponent<Collider>());
            referenciaVisual.GetComponent<Renderer>().material.color = new Color(1f, 0.55f, 0.1f);
        }
    }

    public void Dibujar(int posX, int posZ)
    {
        if (referenciaVisual != null)
        {
            referenciaVisual.transform.position = new Vector3(posX, 0f, posZ);
            Destroy(referenciaVisual, duracionVisual);
        }
    }

    public override void Mover(int posX, int posZ)
    {
        // La explosión no se mueve
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
