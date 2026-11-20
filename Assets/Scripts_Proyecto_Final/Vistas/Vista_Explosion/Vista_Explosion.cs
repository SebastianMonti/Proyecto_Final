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
/*using System.Collections;
using UnityEngine;


    public class Vista_Explosion : Vista_Entidades
    {
        [Header("Configuración Visual")]
        [SerializeField] private GameObject prefabExplosion;
        [SerializeField] private Color colorFuego = new Color(1f, 0.55f, 0.1f);
        [SerializeField] private float duracionVisual = 0.25f;

        private GameObject modeloVisual;

        public void Dibujar(int posX, int posZ)
        {
            Vector3 posicion = new Vector3(posX, 0f, posZ);
            transform.position = posicion;

            if (modeloVisual != null)
            {
                Destroy(modeloVisual);
            }

            if (prefabExplosion != null)
            {
                modeloVisual = Instantiate(prefabExplosion, posicion, Quaternion.identity, transform);
            }
            else
            {
                // Primitiva de respaldo para visualizar los brazos de la explosión en la grilla
                modeloVisual = GameObject.CreatePrimitive(PrimitiveType.Cube);
                modeloVisual.transform.SetParent(transform);
                modeloVisual.transform.position = posicion;
                modeloVisual.transform.localScale = new Vector3(0.9f, 0.35f, 0.9f);

                // Desactivamos el Collider para no interferir con la física de Unity
                Collider col = modeloVisual.GetComponent<Collider>();
                if (col != null)
                {
                    Destroy(col);
                }

                Renderer renderer = modeloVisual.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = colorFuego;
                }
            }

            modeloVisual.name = $"Explosion_Visual_{posX}_{posZ}";
            StartCoroutine(AutoDestruir());
        }

        private IEnumerator AutoDestruir()
        {
            yield return new WaitForSeconds(duracionVisual);
            Eliminar();
        }

      
        public override void Mover(int posX, int posZ)
        {
        // La explosión no se mueve, pero implementamos el método para cumplir con la interfaz
        }

        public override void Eliminar()
        {
            if (modeloVisual != null)
            {
                Destroy(modeloVisual);
            }
            Destroy(gameObject);
        }
}*/