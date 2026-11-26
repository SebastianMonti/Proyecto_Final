using System;
using System.Linq;
using UnityEngine;

public class Banco_Preguntas_Local : IProveedor_Preguntas
{
    private Pregunta[] preguntas = Array.Empty<Pregunta>();
    private readonly System.Random rnd = new System.Random();
    private Idioma_Juego? idiomaCargado;

    public Banco_Preguntas_Local()
    {
        Cargar_Segun_Idioma();
    }

    public void Obtener(int dificultad, Action<Pregunta> alTerminar)
    {
        Cargar_Segun_Idioma();

        if (preguntas == null || preguntas.Length == 0)
        {
            Debug.LogError("Banco_Preguntas_Local: no hay preguntas cargadas.");
            alTerminar?.Invoke(null);
            return;
        }

        int mejor = preguntas.Min(p => Math.Abs(p.dificultad - dificultad));
        var candidatas = preguntas
            .Where(p => Math.Abs(p.dificultad - dificultad) <= mejor + 3)
            .ToList();

        alTerminar?.Invoke(candidatas[rnd.Next(candidatas.Count)]);
    }

    private void Cargar_Segun_Idioma()
    {
        Idioma_Juego idiomaActual = Servicio_Localizacion.Instancia != null
            ? Servicio_Localizacion.Instancia.IdiomaActual
            : Idioma_Juego.Espanol;

        if (idiomaCargado == idiomaActual && preguntas != null && preguntas.Length > 0)
            return;

        string nombreArchivo = idiomaActual == Idioma_Juego.Ingles ? "questions" : "preguntas";
        TextAsset asset = Resources.Load<TextAsset>(nombreArchivo);

        if (asset == null)
        {
            Debug.LogError($"No se encontró Resources/{nombreArchivo}.json");
            return;
        }

        Lista_Preguntas datos = JsonUtility.FromJson<Lista_Preguntas>(asset.text);
        preguntas = (datos != null && datos.preguntas != null)
            ? datos.preguntas
            : Array.Empty<Pregunta>();

        idiomaCargado = idiomaActual;
    }
}

/*using System;
    using System.Linq;
    using UnityEngine;

    public class Banco_Preguntas_Local : IProveedor_Preguntas
    {
        readonly Pregunta[] preguntas;
        readonly System.Random rnd = new System.Random();
        private Idioma_Juego? idiomaCargado;
        private System.Collections.Generic.List<Pregunta> preguntas = new System.Collections.Generic.List<Pregunta>();

        public Banco_Preguntas_Local()
        {
            TextAsset json = Resources.Load<TextAsset>("preguntas");
            preguntas = JsonUtility.FromJson<Lista_Preguntas>(json.text).preguntas;
        }
        [System.Serializable]
        public class Lista_Preguntas_Json
        {
            public System.Collections.Generic.List<Pregunta> preguntas;
        }
        public void Obtener(int dificultad, Action<Pregunta> alTerminar)
        {
        
            int mejor = preguntas.Min(p => Math.Abs(p.dificultad - dificultad));
            var candidatas = preguntas
                .Where(p => Math.Abs(p.dificultad - dificultad) <= mejor + 3)
                .ToList();

            alTerminar(candidatas[rnd.Next(candidatas.Count)]);
        }
        private void Cargar_Segun_Idioma()
        {
            Idioma_Juego idiomaActual = Servicio_Localizacion.Instancia != null
                ? Servicio_Localizacion.Instancia.IdiomaActual
                : Idioma_Juego.Espanol;

            if (idiomaCargado == idiomaActual && preguntas.Count > 0)
                return;

            // Lee Resources/preguntas.json o Resources/questions.json
            string nombreArchivo = idiomaActual == Idioma_Juego.Ingles ? "questions" : "preguntas";
            TextAsset asset = Resources.Load<TextAsset>(nombreArchivo);

            if (asset == null)
            {
                Debug.LogError($"No se encontró Resources/{nombreArchivo}.json");
                return;
            }

            Lista_Preguntas_Json datos = JsonUtility.FromJson<Lista_Preguntas_Json>(asset.text);
            preguntas = (datos != null && datos.preguntas != null)
                ? datos.preguntas
                : new System.Collections.Generic.List<Pregunta>();

            idiomaCargado = idiomaActual;
        }
    }*/