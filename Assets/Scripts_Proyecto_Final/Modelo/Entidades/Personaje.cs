    //metodo actualizar_posicion
    public class Personaje : Entidad
    {
        public Celda Celda_Actual { get; private set; }
        private Controlador_Movimiento controladorMovimiento;
        private  Tablero Tablero;
        private Presentador_Personaje presentador;

    /*public Personaje(Celda celda, Tablero tablero)
            {
                this.Celda_Actual = celda;
                this.Tablero = tablero;
                this.controladorMovimiento = new Controlador_Movimiento(tablero, this, new Controlador_Entidades());
                presentador = new Presentador_Personaje(this);
            }*/
    public Personaje(Celda celda, Tablero tablero, Controlador_Entidades controladorEntidades)
    {
        this.Celda_Actual = celda;
        this.Tablero = tablero;
        this.controladorMovimiento = new Controlador_Movimiento(tablero, this, controladorEntidades);
        presentador = new Presentador_Personaje(this);
    }
    public Presentador_Personaje obtener_presentador()
        {
            return presentador;
        }

        //public bool Verificar_Tipo_Entidad(Celda c)
        //{
        // Asume que tu clase Celda tiene este método auxiliar
        //  return c.TienePersonaje();
        //}

        public override void Dibujar(int Pos_X, int Pos_Z)
        {
            // Adaptado para usar el Presenter base (Llamará a dibujar personaje/serpiente)[cite: 27]
            // p_personaje.dibujar_personaje(Pos_X, Pos_Z); 
        }

        public void Borrar(Presentador_Entidades p_personaje, int Pos_X, int Pos_Z)
        {
            // Lógica para decirle al presentador que elimine el gráfico anterior[cite: 27]
        }

        /*public void Establecer_Celda(Celda nuevaCelda)
        {
            if (nuevaCelda == null)
            {
                throw new System.ArgumentNullException(nameof(nuevaCelda));
            }

            if (celda == nuevaCelda)
            {
                return;
            }

            if (celda != null)
            {
                celda.Remover_Entidad(this);
            }

            celda = nuevaCelda;
            celda.Establecer_Entidad(this);
        }*/

        public void Actualizar_Posicion(Celda Celda_A_La_Que_Se_Mueve)
        {
            // 1. Le avisa al tablero que la celda en la que estaba ya no va a estar más[cite: 27]
            Celda_Actual.Quitar_Entidad(this);

            // 2. El personaje actualiza la celda en la que está[cite: 27]
            Celda_Actual = Celda_A_La_Que_Se_Mueve;

            // 3. Establece su propia referencia en la nueva celda[cite: 27]
            Celda_Actual.Establecer_Entidad(this);
        }

        public Celda Obtener_Celda_Actual()
        {
            return Celda_Actual;
        }

        public Controlador_Movimiento Obtener_Controlador_Movimiento()
        {
            return controladorMovimiento;
        }
    }
