using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mejora_Tiempo : Mejora
{
    public override string Nombre => "Tiempo";
    protected override int Precio_Base => 40;
    protected override void Aplicar_Efecto(Estadisticas s) { s.tiempo_extra += 3; }
}
