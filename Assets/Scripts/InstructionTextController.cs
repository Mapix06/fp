using UnityEngine;
using TMPro;

public class InstructionTextController : MonoBehaviour
{
    public TextMeshProUGUI instructionText;
    public GameObject instruccionesPanel;
    public GameObject misionesPanel;

    private int currentIndex = 0;

    private string[] instructions = new string[]
    {
        "Usa W para moverte.",
        "Usa W + Shift para correr",
        "Usa A y D para mover la cámara",
        "Presiona Espacio para saltar.",
        "Pulsa E para interactuar con objetos.",
        "Recolecta los mapas en el camino",
        "Encuentra la llave escondida en las ruinas.",
        "¡Buena suerte, aventurero!"
    };

    void Start()
    {
        if (instructionText != null && instructions.Length > 0)
        {
            instructionText.text = instructions[currentIndex];
        }

        if (misionesPanel != null)
        {
            misionesPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            currentIndex++;

            if (currentIndex >= instructions.Length)
            {
                if (instruccionesPanel != null)
                {
                    instruccionesPanel.SetActive(false);
                }

                if (misionesPanel != null)
                {
                    misionesPanel.SetActive(true);
                }

                return;
            }

            instructionText.text = instructions[currentIndex];
        }
    }
}
