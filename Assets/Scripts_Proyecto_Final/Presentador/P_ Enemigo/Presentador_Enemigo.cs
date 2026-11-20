
    public abstract class Presentador_Enemigo : Presentador_Entidades
    {
        protected Enemigo enemigo;
        protected Vista_Enemigo vista_enemigo;

        protected Presentador_Enemigo(Enemigo enemigo)
        {
            this.enemigo = enemigo;
           
        }

        public abstract void Mover();

    }