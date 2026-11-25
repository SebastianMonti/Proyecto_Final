using UnityEngine;

public class Vinculo_Vistas_Presentador : MonoBehaviour
{
    [SerializeField] private Vista_Menu viewMenu;
    [SerializeField] private Vista_Seleccionar_Idioma viewIdioma;

    private Ronda_Manager rondaManager;
    private Presentador_Menu presenter;

    public void Inicializar(Ronda_Manager manager)
    {
        rondaManager = manager;

        if (presenter != null)
        {
            presenter.Dispose();
        }

        presenter = new Presentador_Menu(viewMenu, viewIdioma, rondaManager);
    }

    private void Awake()
    {
        if (rondaManager != null && presenter == null)
        {
            presenter = new Presentador_Menu(viewMenu, viewIdioma, rondaManager);
        }
    }

    private void OnDestroy()
    {
        presenter?.Dispose();
    }
}