using System;

public interface IProveedor_Preguntas
{
    // Callback porque la API va a ser asíncrona; el banco local responde al instante
    void Obtener(int dificultad, Action<Pregunta> alTerminar);
}