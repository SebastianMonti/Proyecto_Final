public enum Estado_Trivia { Inactiva, Decidiendo, Cargando, Respondiendo, Resultado }

public class Controlador_Trivia
{
    readonly IProveedor_Preguntas proveedor;
    int dificultad;

    public Estado_Trivia Estado = Estado_Trivia.Inactiva;
    public int Ganancias;         // G: lo ganado en la ronda
    public int GananciasFinales;   // 2G, G/2 o G
    public bool Participo;
    public bool Acerto;
    public Pregunta Pregunta_Actual;

    // Cuánto hay que sumar (o restar) al saldo
    public int Ajuste (){
        return GananciasFinales - Ganancias;
    }
    public Controlador_Trivia(IProveedor_Preguntas proveedor)
    {
        this.proveedor = proveedor;
    }

    public void Iniciar(int ganancias, int ronda)
    {
        Ganancias = ganancias;
        dificultad = ronda;                 // ronda 1..50 = dificultad 1..50
        Participo = false;
        Acerto = false;
        Pregunta_Actual = null;
        Estado = Estado_Trivia.Decidiendo;  // se decide ANTES de ver la pregunta
    }

    public void No_Participar()
    {
        Participo = false;
        GananciasFinales = Ganancias;
        Estado = Estado_Trivia.Resultado;
    }

    public void Participar()
    {
        if (Estado != Estado_Trivia.Decidiendo) return;
        Participo = true;
        Estado = Estado_Trivia.Cargando;
        proveedor.Obtener(dificultad, pregunta =>
        {
            Pregunta_Actual = pregunta;
            Estado = Estado_Trivia.Respondiendo;
        });
    }

    public void Responder(int indice)
    {
        if (Estado != Estado_Trivia.Respondiendo) 
            return;
        Acerto = indice == Pregunta_Actual.correcta;
        GananciasFinales = Acerto ? Ganancias * 2 : Ganancias / 2;
        Estado = Estado_Trivia.Resultado;
    }

    public void Cerrar()
    {
        Estado = Estado_Trivia.Inactiva;
    }
}