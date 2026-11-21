using System.Collections.Generic;
using UnityEngine;

public class Controlador_Entidades
{
    private readonly Controlador_Colisiones controlador_colisiones;
    private readonly Estadisticas estadisticas;

    public Controlador_Entidades(Estadisticas estadisticas = null)
    {
        this.estadisticas = estadisticas;
        controlador_colisiones = new Controlador_Colisiones();

        controlador_colisiones.Registrar_Colision<Personaje, Moneda_Cobre>(
            Colision_Personaje_Moneda);

        controlador_colisiones.Registrar_Colision<Enemigo_Avaro, Moneda_Cobre>(
            Colision_EnemigoAvaro_Moneda);

        controlador_colisiones.Registrar_Colision<Explosion, Enemigo_Basico>(
            Colision_Explosion_Enemigo);

        controlador_colisiones.Registrar_Colision<Explosion, Enemigo_Evasivo>(
            Colision_Explosion_Enemigo);

        controlador_colisiones.Registrar_Colision<Explosion, Enemigo_Avaro>(
            Colision_Explosion_Enemigo);

        controlador_colisiones.Registrar_Colision<Explosion, Enemigo_Tanque>(
            Colision_Explosion_Enemigo);

        controlador_colisiones.Registrar_Colision<Explosion, Enemigo_Fantasma>(
            Colision_Explosion_Enemigo);
    }

    public void Chequear_Colision(Entidad movil, Celda celda)
    {
        foreach (Entidad entidad in new List<Entidad>(celda.Obtener_Entidades()))
        {
            if (entidad != movil)
            {
                controlador_colisiones.Manejar_Colision(movil, entidad);
            }
        }
    }

    private void Colision_Personaje_Moneda(Personaje personaje, Moneda_Cobre moneda)
    {
        estadisticas?.Sumar_Monedas(moneda.Obtener_Valor());
        Debug.Log($"Moneda atrapada (+{moneda.Obtener_Valor()}). Total: {estadisticas?.Obtener_Monedas()}");
        moneda.Atrapada();
    }

    private void Colision_Explosion_Enemigo(Explosion explosion, Enemigo enemigo)
    {
        enemigo.Decrementar_Vida(explosion.Obtener_Daño());
    }

    private void Colision_EnemigoAvaro_Moneda(Enemigo_Avaro avaro, Moneda_Cobre moneda)
    {
        avaro.Reaccionar_A_Moneda(moneda);
    }
}


/*using System.Collections.Generic;
using UnityEngine;

public class Controlador_Entidades
{
    private readonly Controlador_Colisiones controlador_colisiones;
    private readonly Estadisticas estadisticas;

    public Controlador_Entidades(Estadisticas estadisticas = null)
    {
        this.estadisticas = estadisticas;
        controlador_colisiones = new Controlador_Colisiones();

        controlador_colisiones.Registrar_Colision<Personaje, Moneda_Cobre>(
            Colision_Personaje_Moneda);

        controlador_colisiones.Registrar_Colision<Explosion, Enemigo_Basico>(
            Colision_Explosion_Enemigo);
    }

    public void Chequear_Colision(Entidad movil, Celda celda)
    {
        foreach (Entidad entidad in new List<Entidad>(celda.Obtener_Entidades()))
        {   
            if (entidad != movil)
            {
                controlador_colisiones.Manejar_Colision(movil, entidad);
            }
        }
    }

    private void Colision_Personaje_Moneda(Personaje personaje, Moneda_Cobre moneda)
    {
        estadisticas?.Sumar_Monedas(moneda.Obtener_Valor());
        Debug.Log($"Moneda atrapada (+{moneda.Obtener_Valor()}). Total: {estadisticas?.Obtener_Monedas()}");
        moneda.Atrapada();
    }

    private void Colision_Explosion_Enemigo(Explosion explosion, Enemigo_Basico enemigo)
    {
        enemigo.Decrementar_Vida(explosion.Obtener_Daño());
    }
    private void Colision_EnemigoAvaro_Moneda(Enemigo_Avaro avaro, Moneda_Cobre moneda)
    {
        avaro.Reaccionar_A_Moneda(moneda);
    }
}*/

/*
using System.Collections.Generic;
using UnityEngine;

public class Controlador_Entidades
{
    private readonly Controlador_Colisiones controlador_colisiones;

    public Controlador_Entidades()
    {
        controlador_colisiones = new Controlador_Colisiones();
        controlador_colisiones.Registrar_Colision<Personaje, Moneda_Cobre>(
            Colision_Personaje_Moneda);
    }

    public void Chequear_Colision(Entidad movil, Celda celda)
    {
        foreach (Entidad entidad in new List<Entidad>(celda.Obtener_Entidades()))
        {
            if (entidad != movil)
            {
                controlador_colisiones.Manejar_Colision(movil, entidad);
            }
        }
    }

    private static void Colision_Personaje_Moneda(Personaje personaje, Moneda_Cobre moneda)
    {
        Debug.Log("Colisionó con la moneda.");
        moneda.Atrapada();
    }
}
    */