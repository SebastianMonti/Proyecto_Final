using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mejora_Velocidad : Mejora
{
    public override string Nombre => "Velocidad";
    protected override int Precio_Base => 60;
    protected override void Aplicar_Efecto(Estadisticas s) { s.velocidad *= 0.9f; }
}