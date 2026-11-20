using System.Collections.Generic;


    public class Tablero
    {
        private Celda[,] tablero;
        private int ancho_x;
        private int ancho_z;

        //public Tablero(int ancho_x, int ancho_z)
        public Tablero()
        {
            this.ancho_x = 20;
            this.ancho_z = 20;

            // Inicialización de la matriz
            tablero = new Celda[ancho_x, ancho_z];

            for (int i = 0; i < ancho_x; i++)
            {
                for (int j = 0; j < ancho_z; j++)
                {
                    // Le pasamos i, j para que la celda sepa su posición
                    tablero[i, j] = new Celda(i, j);
                }
            }
        }

        public Celda Obtener_Celda(int x, int z)
        {
            // Verificamos que las coordenadas estén dentro de los límites de la matriz
            if (x >= 0 && x < ancho_x && z >= 0 && z < ancho_z)
            {
                return tablero[x, z];
            }
            return null; // Si está fuera de los límites
        }

        public List<Celda> Obtener_Celdas_Libres()
        {
            List<Celda> celdas_libres = new List<Celda>();

            for (int i = 0; i < ancho_x; i++)
            {
                for (int j = 0; j < ancho_z; j++)
                {
                    if (tablero[i, j].Esta_Libre() == true)
                    {
                        celdas_libres.Add(tablero[i, j]);
                    }
                }
            }

            return celdas_libres;
        }
        public Celda Obtener_Celda_Libre(Celda origen)
        {
            if (origen == null) return null;

            // 1. Primero revisamos la misma celda donde murió el enemigo
            if (origen.Esta_Libre())
            {
                return origen;
            }

            int ox = origen.Obtener_Posicion_X();
            int oz = origen.Obtener_Posicion_Z();
            int maxRadio = System.Math.Max(ancho_x, ancho_z);

            // 2. Buscamos en anillos alrededor del origen (distancia 1, 2, 3...)
            for (int radio = 1; radio < maxRadio; radio++)
            {
                for (int dx = -radio; dx <= radio; dx++)
                {
                    for (int dz = -radio; dz <= radio; dz++)
                    {
                        // Evaluamos solo el perímetro del anillo actual
                        if (System.Math.Abs(dx) != radio && System.Math.Abs(dz) != radio)
                        {
                            continue;
                        }

                        int nx = ox + dx;
                        int nz = oz + dz;

                        if (Esta_Dentro_Del_Tablero(nx, nz))
                        {
                            Celda candidata = Obtener_Celda(nx, nz);
                            if (candidata != null && candidata.Esta_Libre())
                            {
                                return candidata;
                            }
                        }
                    }
                }
            }

            return null; // El tablero está completamente lleno
        }
    public List<Celda> Obtener_Celdas_Vecinas(Celda celda)
        {
            List<Celda> vecinas = new List<Celda>();
            if (celda == null)
            {
                return vecinas;
            }

            int x = celda.Obtener_Posicion_X();
            int z = celda.Obtener_Posicion_Z();
            Celda[] candidatas =
            {
                Obtener_Celda(x + 1, z),
                Obtener_Celda(x - 1, z),
                Obtener_Celda(x, z + 1),
                Obtener_Celda(x, z - 1)
            };

            foreach (Celda candidata in candidatas)
            {
                if (candidata != null)
                {
                    vecinas.Add(candidata);
                }
            }

            return vecinas;
        }
        public List<Celda> Obtener_Celdas_En_Cruz(Celda centro, int radio)
        {
            List<Celda> celdasAfectadas = new List<Celda>();
            if (centro == null) return celdasAfectadas;

            int cx = centro.Obtener_Posicion_X();
            int cz = centro.Obtener_Posicion_Z();

            // 1. Agregamos la celda central (donde está parado el personaje)
            celdasAfectadas.Add(centro);

            // 2. Extendemos los 4 brazos según el valor de 'radio'
            // Direcciones: Derecha (1,0), Izquierda (-1,0), Arriba (0,1), Abajo (0,-1)
            int[] dirX = { 1, -1, 0, 0 };
            int[] dirZ = { 0, 0, 1, -1 };

            for (int d = 0; d < 4; d++)
            {
                for (int paso = 1; paso <= radio; paso++)
                {
                    int nx = cx + dirX[d] * paso;
                    int nz = cz + dirZ[d] * paso;

                    // Verificamos que no se salga de los límites de la grilla
                    if (!Esta_Dentro_Del_Tablero(nx, nz))
                    {
                        break; // Cortamos este brazo si llegamos al borde
                    }

                    Celda celdaVecina = Obtener_Celda(nx, nz);

                    celdasAfectadas.Add(celdaVecina);
                }
            }

            return celdasAfectadas;
        }
        public bool Esta_Dentro_Del_Tablero(int x, int z)
        {
            return x >= 0 && x < ancho_x && z >= 0 && z < ancho_z;
        }

        public int Obtener_Ancho_X()
        {
                return ancho_x;
        }
        public int Obtener_Ancho_Z()
        {
                return ancho_z;
        }
    }
