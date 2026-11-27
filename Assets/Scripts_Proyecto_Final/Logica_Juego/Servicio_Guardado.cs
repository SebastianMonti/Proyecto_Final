using UnityEngine;

public static class Servicio_Guardado
{
    const string CLAVE = "partida_v1";

    public static bool Existe_Partida()
    {
        return PlayerPrefs.HasKey(CLAVE);
    }

    /*
    public static void Guardar(Datos_A_Cargar datos)
    {
        string json = JsonUtility.ToJson(datos);
        Debug.Log("Guardando: " + json);
        PlayerPrefs.SetString(CLAVE, json);
        PlayerPrefs.Save();
    }

    public static Datos_A_Cargar Cargar()
    {
        if (!Existe_Partida()) 
            return null;

        try
        {
            var d = JsonUtility.FromJson<Datos_A_Cargar>(PlayerPrefs.GetString(CLAVE));
            return Validar(d) ? d : null;
        }
        catch (System.Exception)
        {
            return null;   // dato corrupto: se ignora
        }
    }

    public static void Borrar()
    {
        PlayerPrefs.DeleteKey(CLAVE);
        PlayerPrefs.Save();
    }
     */
    static bool Validar(Datos_A_Cargar d)
    {
        if (d == null || d.version != 1 || d.ronda < 1 || d.dinero < 0) 
            return false;
        if (d.nivelesMejoras == null) 
            return false;
        foreach (int n in d.nivelesMejoras) 
            if (n < 0) 
                return false;
        return true;
    }
   

    public static void Guardar(Datos_A_Cargar datos)
    {
        string json = JsonUtility.ToJson(datos);
        Debug.Log("GUARDANDO: " + json);
        PlayerPrefs.SetString(CLAVE, json);
        PlayerPrefs.Save();
    }

    public static Datos_A_Cargar Cargar()
    {
        Debug.Log("CARGAR: existe = " + Existe_Partida());
        if (!Existe_Partida()) return null;

        try
        {
            string json = PlayerPrefs.GetString(CLAVE);
            Debug.Log("CARGAR json: " + json);
            var d = JsonUtility.FromJson<Datos_A_Cargar>(json);
            bool ok = Validar(d);
            Debug.Log("CARGAR valido = " + ok);
            return ok ? d : null;
        }
        catch (System.Exception e)
        {
            Debug.Log("CARGAR excepcion: " + e.Message);
            return null;
        }
    }
    public static void Borrar()
    {
        PlayerPrefs.DeleteKey(CLAVE);
        PlayerPrefs.Save();
    }
}