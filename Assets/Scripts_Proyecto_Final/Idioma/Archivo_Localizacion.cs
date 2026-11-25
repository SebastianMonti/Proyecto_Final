using System;
using System.Collections.Generic;

[Serializable]
public class Archivo_Localizacion
{
    public List<Entrada_Localizacion> entradas = new List<Entrada_Localizacion>();
}

public enum Idioma_Juego
{
    Espanol,
    Ingles
}