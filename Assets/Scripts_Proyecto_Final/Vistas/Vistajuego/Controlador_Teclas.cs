using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controlador_Teclas: MonoBehaviour
{
    private  List<Tecla> teclas;
    private  Presentador_Personaje presentador;

   public Controlador_Teclas(Presentador_Personaje p)
    {
        teclas = new List<Tecla>
        {
            new Tecla_Arriba(),
            new Tecla_Abajo(),
            new Tecla_Izquierda(),
            new Tecla_Derecha()
        };
        presentador = p;    
    }

    public void Verificar_Tecla_Presionada()
    {
        foreach (Tecla tecla in teclas) 
        {
            if (tecla.Verificar_Tecla_Ingresada())
            {
                tecla.Ejecutar_Funcionalidad(presentador);
            }
        }
    }

    public void Inciiar(Presentador_Personaje p)
    {
    
        this.presentador = p;
        teclas = new List<Tecla>
        {
            new Tecla_Arriba(),
            new Tecla_Abajo(),
            new Tecla_Izquierda(),
            new Tecla_Derecha()
        };
    }


    /*private void Update()
    {
        Verificar_Tecla_Presionada();
    }*/
}

