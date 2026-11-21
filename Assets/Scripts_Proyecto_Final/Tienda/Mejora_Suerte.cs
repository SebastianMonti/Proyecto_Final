using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mejora_Suerte : Mejora
{
    public override string Nombre => "Suerte";
    protected override int Precio_Base => 70;
    protected override void Aplicar_Efecto(Estadisticas s) { s.Suerte += 0.05f; }
}