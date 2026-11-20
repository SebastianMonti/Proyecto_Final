using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vista_Entidades : MonoBehaviour 
{
   public abstract void Mover(int posX, int posZ);
   public abstract void Eliminar();
}
