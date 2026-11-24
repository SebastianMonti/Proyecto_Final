    public class Personaje : Entidad
    {
        public Celda Celda_Actual { get; private set; }
        private Controlador_Movimiento controladorMovimiento;
        private  Tablero Tablero;
        private Presentador_Personaje presentador;
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

    public override void Dibujar(int Pos_X, int Pos_Z)
    {
    //se usa el mover y el Actualizar_posicion para que el personaje se dibuje en la nueva posición
    }

    public void Borrar(Presentador_Entidades p_personaje, int Pos_X, int Pos_Z)
    {
        // Lógica para decirle al presentador que elimine el gráfico anterior[cite: 27]
    }
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
