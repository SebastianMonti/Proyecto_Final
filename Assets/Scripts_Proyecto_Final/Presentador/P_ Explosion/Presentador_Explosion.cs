public class Presentador_Explosion : Presentador_Entidades
{
    private readonly Vista_Explosion vista1;

    public Presentador_Explosion()
    {
        this.vista1 = new Vista_Explosion();
    }

    public override void dibujar(int posX, int posZ)
    {
        vista1.Dibujar(posX, posZ);
    }

    public override void EliminarEntidad()
    {
        vista1.Eliminar();
    }
}

/*using UnityEngine;
    public class Presentador_Explosion : Presentador_Entidades
    {
        private Vista_Explosion vista_explosion;

        // Recibe la instancia de Vista_Explosion (por ejemplo, instanciada desde un prefab)
        public Presentador_Explosion(Vista_Explosion vista)
        {
            this.vista_explosion = vista;
        }

        // Constructor vacío: crea el GameObject con Vista_Explosion al momento de dibujar
        public Presentador_Explosion()
        {
        }

        public override void dibujar(int posX, int posZ)
        {
            if (vista_explosion == null)
            {
                GameObject go = new GameObject($"Explosion_{posX}_{posZ}");
                vista_explosion = go.AddComponent<Vista_Explosion>();
            }

            vista_explosion.Dibujar(posX, posZ);
        }

        public override void EliminarEntidad()
        {
            if (vista_explosion != null)
            {
                vista_explosion.Eliminar();
                vista_explosion = null;
            }
        }
    }
*/