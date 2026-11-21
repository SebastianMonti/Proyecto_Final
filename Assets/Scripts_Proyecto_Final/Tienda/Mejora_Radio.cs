using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mejora_Radio : Mejora
{
    public override string Nombre => "Radio";
    protected override int Precio_Base => 50;
    protected override void Aplicar_Efecto(Estadisticas s) { s.radio += 1; }
}
