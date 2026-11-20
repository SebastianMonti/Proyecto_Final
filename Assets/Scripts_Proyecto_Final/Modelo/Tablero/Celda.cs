using System.Collections.Generic;

    public class Celda
    {
        private List<Entidad> Entidades;
        private int x;
        private int z;

        // Constructor para inicializar la posici�n y la lista
        public Celda(int x, int z)
        {
            this.x = x;
            this.z = z;
            this.Entidades = new List<Entidad>();
        }

        public bool Esta_Libre()
        {
            return Entidades.Count == 0;
        }

        public bool TieneExplosion()
        {
            return Entidades.Exists(entidad => entidad is Explosion);
        }

        public bool Hay_Enemigo()
        {
            return Entidades.Exists(entidad => entidad is Enemigo);
        }

        public void Establecer_Entidad(Entidad entidad)
        {
            if (entidad != null && !Entidades.Contains(entidad))
            {
                Entidades.Add(entidad);
            }
        }
        public List<Entidad> Obtener_Entidades()
        {
            return new List<Entidad>(Entidades);
        }                  
        public void Remover_Entidad()
        {
            if (Entidades.Count > 0)
            {
                Entidades.RemoveAt(Entidades.Count - 1);
            }
        }

        public bool Quitar_Entidad(Entidad entidad)
        {
            return Entidades.Remove(entidad);
        }

        public int Obtener_Posicion_X()
        {
            return x;
        }

        public int Obtener_Posicion_Z()
        {
            return z;
        }
    }
