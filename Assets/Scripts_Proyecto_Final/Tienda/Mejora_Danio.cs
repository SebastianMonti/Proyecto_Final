using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mejora_Danio : Mejora
{
    public override string Nombre => "Daño";
    protected override int Precio_Base => 50;
    protected override void Aplicar_Efecto(Estadisticas s) { s.daño += 5; }
}