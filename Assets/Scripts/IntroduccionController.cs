using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroduccionController : MonoBehaviour
{
    [Header("Instrucciones")]
    public TextMeshProUGUI instructionText;
    public GameObject instruccionesPanel;

    [Header("Pausa y UI")]
    public GameObject panelPausa;
    public GameObject panelControles;

    [Header("Referencia UI Controller")]
    public UIController uiController;  // ? referencia al script UIController

    [Header("Transición de escena")]
    public string siguienteEscena = "CuevaPenumbra";

    private bool juegoPausado = false;
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
        panelPausa?.SetActive(false);
        panelControles?.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;
    }

    void Update()
    {
        // Instrucciones
        if (instruccionesPanel.activeSelf &&
            (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            currentIndex++;
            if (currentIndex >= instructions.Length)
            {
                instruccionesPanel.SetActive(false);
                uiController?.MostrarPanelMisiones(); // <- se muestra el panel de misiones
            }
            else
            {
                instructionText.text = instructions[currentIndex];
            }
        }

        // Pausa
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePausa();
        }
    }

    public void TogglePausa()
    {
        juegoPausado = !juegoPausado;

        panelPausa?.SetActive(juegoPausado);
        panelControles?.SetActive(false);

        Cursor.lockState = juegoPausado ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = juegoPausado;
        Time.timeScale = juegoPausado ? 0f : 1f;
    }

    public void ContinuarJuego()
    {
        juegoPausado = false;
        panelPausa?.SetActive(false);
        panelControles?.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void MostrarControles()
    {
        panelControles?.SetActive(true);
        panelPausa?.SetActive(false);
    }

    public void CerrarControles()
    {
        panelControles?.SetActive(false);
        panelPausa?.SetActive(true);
    }

    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Menu");
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }

    public void CambiarEscena()
    {
        SceneManager.LoadScene(siguienteEscena);
    }
}
