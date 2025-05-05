using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
    [Header("Panel Misiones")]
    public GameObject panelMisiones;
    public TextMeshProUGUI misionesText;

    private bool panelVisible = false;

    void Start()
    {
        if (panelMisiones != null)
            panelMisiones.SetActive(false); // Empieza oculto

        string escenaActual = SceneManager.GetActiveScene().name;
        List<string> misiones = ObtenerMisionesPorEscena(escenaActual);

        if (misionesText != null)
            misionesText.text = FormatearMisiones(misiones);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && panelMisiones != null)
        {
            panelVisible = !panelVisible;
            panelMisiones.SetActive(panelVisible);
        }
    }

    List<string> ObtenerMisionesPorEscena(string escena)
    {
        List<string> misiones = new();

        switch (escena)
        {
            case "Introduccion":
                misiones.Add("Busca la antorcha");
                misiones.Add("Recolecta todos los mapas");
                break;
            case "CuevaPenumbra":
                misiones.Add("Resuelve los puzzles antes de que se acabe el tiempo");
                misiones.Add("Recolecta los mapas");
                misiones.Add("Obtén la pieza del corazón del tiempo");
                break;
            case "CuevaEco":
                misiones.Add("Resuelve los puzzles");
                misiones.Add("Recolecta la pieza del corazón del tiempo");
                misiones.Add("Recolecta todos los mapas");
                misiones.Add("Usa la antorcha");
                break;
            case "Final":
            case "EscenaFinal":
                misiones.Add("Vence al jefe");
                misiones.Add("Consigue la llave");
                break;
            default:
                misiones.Add("No hay misiones definidas para esta escena.");
                break;
        }

        return misiones;
    }

    string FormatearMisiones(List<string> misiones)
    {
        return string.Join("\n• ", misiones).Insert(0, "• ");
    }

    public void MostrarPanelMisiones()
    {
        if (panelMisiones != null)
        {
            panelMisiones.SetActive(true);
            panelVisible = true;
        }
    }

}
