using UnityEngine;


    public abstract class Vista_Enemigo : Vista_Entidades
    {
        protected GameObject modeloVisual;

        public abstract void Dibujar(int posX, int posZ); 
        public virtual void Destruir()
        {
            if (modeloVisual != null)
            {
                Destroy(modeloVisual);
            }
            Destroy(gameObject);
        }
    }
