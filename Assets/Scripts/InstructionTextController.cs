using UnityEngine;
using TMPro;

public class InstructionTextController : MonoBehaviour
{
    // Campo visible en el Inspector para arrastrar el objeto TextMeshPro
    public TextMeshProUGUI instructionText;

    private int currentIndex = 0;

    // Lista de instrucciones que se mostrar�n una por una
    private string[] instructions = new string[]
    {
        "Usa WASD para moverte.",
        "Presiona Espacio para saltar.",
        "Pulsa E para interactuar con objetos.",
        "Encuentra la llave escondida en las ruinas.",
        "�Buena suerte, aventurero!"
    };

    void Start()
    {
        // Mostrar la primera instrucci�n al iniciar
        if (instructionText != null && instructions.Length > 0)
        {
            instructionText.text = instructions[currentIndex];
        }
    }

    void Update()
    {
        // Detectar tecla Enter (Return) o Enter del teclado num�rico
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // Avanzar a la siguiente instrucci�n
            currentIndex++;

            if (currentIndex >= instructions.Length)
            {
                // Opcional: reiniciar o mantener el �ltimo mensaje
                // currentIndex = 0; // Si quieres que reinicie
                currentIndex = instructions.Length - 1; // Para mantener el �ltimo
            }

            // Actualizar el texto en pantalla
            instructionText.text = instructions[currentIndex];
        }
    }
}
