/*
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class Creador_Vista_Menu_Editor
{
    [MenuItem("Tools/UI/Crear Vista Menu")]
    public static void CrearVistaMenu()
    {
        // 1. Buscar o crear Canvas
        Canvas canvas = Object.FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasGo = new GameObject("Canvas");
            Undo.RegisterCreatedObjectUndo(canvasGo, "Crear Canvas");

            canvas = canvasGo.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            CanvasScaler scaler = canvasGo.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);

            canvasGo.AddComponent<GraphicRaycaster>();
        }

        // 2. Asegurar EventSystem para que funcionen los clicks
        if (Object.FindFirstObjectByType<EventSystem>() == null)
        {
            GameObject eventSystemGo = new GameObject("EventSystem");
            Undo.RegisterCreatedObjectUndo(eventSystemGo, "Crear EventSystem");
            eventSystemGo.AddComponent<EventSystem>();
            eventSystemGo.AddComponent<StandaloneInputModule>();
        }

        // 3. Crear o reutilizar Vista_Menu
        GameObject vistaMenuGo = GameObject.Find("Vista_Menu");
        if (vistaMenuGo == null)
        {
            vistaMenuGo = new GameObject("Vista_Menu");
            Undo.RegisterCreatedObjectUndo(vistaMenuGo, "Crear Vista_Menu");
            vistaMenuGo.transform.SetParent(canvas.transform, false);
        }

        RectTransform vistaRect = vistaMenuGo.GetComponent<RectTransform>();
        if (vistaRect == null) vistaRect = vistaMenuGo.AddComponent<RectTransform>();
        EstirarCompleto(vistaRect);

        Vista_Menu vistaMenu = vistaMenuGo.GetComponent<Vista_Menu>();
        if (vistaMenu == null) vistaMenu = vistaMenuGo.AddComponent<Vista_Menu>();

        // 4. Crear Panel_Menu
        GameObject panelMenuGo = new GameObject("Panel_Menu");
        Undo.RegisterCreatedObjectUndo(panelMenuGo, "Crear Panel_Menu");
        panelMenuGo.transform.SetParent(vistaMenuGo.transform, false);

        RectTransform panelRect = panelMenuGo.AddComponent<RectTransform>();
        EstirarCompleto(panelRect);

        Image panelImagen = panelMenuGo.AddComponent<Image>();
        panelImagen.color = new Color(0.10f, 0.10f, 0.14f, 0.85f);

        VerticalLayoutGroup layout = panelMenuGo.AddComponent<VerticalLayoutGroup>();
        layout.childAlignment = TextAnchor.MiddleCenter;
        layout.spacing = 24f;
        layout.padding = new RectOffset(40, 40, 40, 40);
        layout.childControlWidth = false;
        layout.childControlHeight = false;
        layout.childForceExpandWidth = false;
        layout.childForceExpandHeight = false;

        // 5. Crear botones Jugar y Seleccionar Idioma
        (Button botonJugar, TMP_Text labelJugar) = CrearBotonTMP(
            "Boton_Jugar",
            "Label_Jugar",
            "Jugar",
            panelMenuGo.transform
        );

        (Button botonIdioma, TMP_Text labelIdioma) = CrearBotonTMP(
            "Boton_Seleccionar_Idioma",
            "Label_Seleccionar_Idioma",
            "Seleccionar Idioma",
            panelMenuGo.transform
        );

        // 6. Asignar referencias en Vista_Menu via SerializedObject
        SerializedObject vistaSO = new SerializedObject(vistaMenu);
        vistaSO.FindProperty("panelMenu").objectReferenceValue = panelMenuGo;
        vistaSO.FindProperty("botonJugar").objectReferenceValue = botonJugar;
        vistaSO.FindProperty("botonSeleccionarIdioma").objectReferenceValue = botonIdioma;
        vistaSO.FindProperty("labelJugar").objectReferenceValue = labelJugar;
        vistaSO.FindProperty("labelSeleccionarIdioma").objectReferenceValue = labelIdioma;
        vistaSO.ApplyModifiedProperties();

        // 7. Crear o reutilizar Presentador_Menu y asignar Vista_Menu
        GameObject presentadorGo = GameObject.Find("Presentador_Menu");
        if (presentadorGo == null)
        {
            presentadorGo = new GameObject("Presentador_Menu");
            Undo.RegisterCreatedObjectUndo(presentadorGo, "Crear Presentador_Menu");
        }

        Presentador_Menu presentador = presentadorGo.GetComponent<Presentador_Menu>();
        if (presentador == null) presentador = presentadorGo.AddComponent<Presentador_Menu>();

        SerializedObject presentadorSO = new SerializedObject(presentador);
        presentadorSO.FindProperty("vistaMenu").objectReferenceValue = vistaMenu;

        
        Ronda_Manager rondaManager = Object.FindFirstObjectByType<Ronda_Manager>();
        if (rondaManager != null)
        {
            presentadorSO.FindProperty("rondaManager").objectReferenceValue = rondaManager;
        }
        

        Vista_Seleccionar_Idioma vistaIdioma = Object.FindFirstObjectByType<Vista_Seleccionar_Idioma>();
        if (vistaIdioma != null)
        {
            presentadorSO.FindProperty("vistaSeleccionarIdioma").objectReferenceValue = vistaIdioma;
        }

        presentadorSO.ApplyModifiedProperties();

        Selection.activeGameObject = vistaMenuGo;
        Debug.Log("Vista_Menu y Presentador_Menu creados y conectados correctamente.");
    }

    private static (Button, TMP_Text) CrearBotonTMP(
        string nombreBoton,
        string nombreLabel,
        string textoInicial,
        Transform padre)
    {
        GameObject botonGo = new GameObject(nombreBoton);
        Undo.RegisterCreatedObjectUndo(botonGo, $"Crear {nombreBoton}");
        botonGo.transform.SetParent(padre, false);

        RectTransform botonRect = botonGo.AddComponent<RectTransform>();
        botonRect.sizeDelta = new Vector2(340f, 70f);

        Image imagen = botonGo.AddComponent<Image>();
        imagen.color = new Color(0.22f, 0.25f, 0.33f, 1f);

        Button boton = botonGo.AddComponent<Button>();
        boton.targetGraphic = imagen;

        GameObject labelGo = new GameObject(nombreLabel);
        Undo.RegisterCreatedObjectUndo(labelGo, $"Crear {nombreLabel}");
        labelGo.transform.SetParent(botonGo.transform, false);

        RectTransform labelRect = labelGo.AddComponent<RectTransform>();
        EstirarCompleto(labelRect);

        TextMeshProUGUI tmp = labelGo.AddComponent<TextMeshProUGUI>();
        tmp.text = textoInicial;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.fontSize = 28f;
        tmp.color = Color.white;

        return (boton, tmp);
    }

    private static void EstirarCompleto(RectTransform rect)
    {
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }
}
*/