using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroduccionController : MonoBehaviour
{
    [Header("Instrucciones")]
    public TextMeshProUGUI instructionText;
    public GameObject instruccionesPanel;

    [Header("Referencia UI Controller")]
    public UIController uiController;

    [Header("Transición de escena")]
    public string siguienteEscena = "CuevaPenumbra";

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

        instruccionesPanel?.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (instruccionesPanel.activeSelf &&
            (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            currentIndex++;
            if (currentIndex >= instructions.Length)
            {
                instruccionesPanel.SetActive(false);
                uiController?.MostrarPanelMisiones();
            }
            else
            {
                instructionText.text = instructions[currentIndex];
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            uiController?.TogglePausa();
        }
    }

    public void CambiarEscena()
    {
        SceneManager.LoadScene(siguienteEscena);
    }
}
