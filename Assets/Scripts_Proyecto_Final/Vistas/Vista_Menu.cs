using UnityEngine;
using UnityEngine.UI;

namespace Refactoring.Vistas
{
    public interface IPresentadorMenu
    {
        void Click_Boton_Seleccionar();
        void Cargar_Partida();
    }

    public class Vista_Menu : MonoBehaviour
    {
        public GameObject Panel_Menu;
        public Button Boton_Seleccionar_Nivel;
        public Button Boton_Cargar_Partida;
        public Image Imagen_Snake;

        private IPresentadorMenu presentador;

        public void Instanciar_Presentador(IPresentadorMenu p)
        {
            this.presentador = p;
        }

        public void Ocultar_Panel_Menu()
        {
            if (Panel_Menu != null) Panel_Menu.SetActive(false);
        }

        public void Mostrar_Panel_Menu()
        {
            if (Panel_Menu != null) Panel_Menu.SetActive(true);
        }

        // Estos m�todos se enlazan en el evento OnClick() de los botones en el inspector de Unity
        public void Click_en_Boton_Seleccionar_Nivel()
        {
            presentador?.Click_Boton_Seleccionar();
        }

        public void Click_en_Boton_Cargar_Partida()
        {
            presentador?.Cargar_Partida();
        }
    }
}
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vista_Menu : MonoBehaviour
{
    public GameObject Panel_Menu;
    public Button Boton_Seleccionar_Nivel;
    public Button Boton_Cargar_Partida;
    public Image Imagen_Snake;
    public Presentador presenter;

    public void Instanciar_presentador(Presentador p)
    {
        presenter = p;
    }
    public void Ocultar_Panel_Menu()
    {
        Panel_Menu.SetActive(false);
    }
    public void Mostrar_Panel_Menu()
    {
        Panel_Menu.SetActive(true);
    }
    public void Clic_En_Boton_Seleccionar_Nivel()
    {
        presenter.Clic_Boton_Seleccionar();
    }
    public void Clic_En_Boton_Cargar_Partida()
    {
        presenter.Cargar_Partida();
    }
}
*/