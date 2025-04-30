using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class MisionesController : MonoBehaviour
{
    public TextMeshProUGUI misionesText; 

    void Start()
    {
        string escenaActual = SceneManager.GetActiveScene().name;
        List<string> misiones = ObtenerMisionesPorEscena(escenaActual);

        if (misionesText != null)
        {
            misionesText.text = FormatearMisiones(misiones);
        }
    }

    List<string> ObtenerMisionesPorEscena(string escena)
    {
        List<string> misiones = new List<string>();

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
}
