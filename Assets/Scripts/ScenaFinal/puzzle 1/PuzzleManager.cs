using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("Puzzle Configuration")]
    public int[] correctSequence = { 1, 3, 2, 4 };

    private List<int> playerSequence = new List<int>();
    private List<ButtonInteractable> botonesRegistrados = new List<ButtonInteractable>();
    private bool puzzleSolved = false;

    private PuzzleTimerManager timerManager;

    void Start()
    {
        timerManager = FindObjectOfType<PuzzleTimerManager>();
    }

    public void RegistrarBoton(ButtonInteractable boton)
    {
        if (!botonesRegistrados.Contains(boton))
        {
            botonesRegistrados.Add(boton);
        }
    }

    public void ButtonPressed(int buttonID)
    {
        if (puzzleSolved) return;

        // Iniciar cronómetro al primer clic
        timerManager?.IniciarCronometro();

        playerSequence.Add(buttonID);

        if (CheckSequenceSoFar())
        {
            if (playerSequence.Count == correctSequence.Length)
            {
                puzzleSolved = true;
                Debug.Log("¡Felicidades! Pasa al siguiente puzzle.");
                timerManager?.PuzzleCompletado();  // Marca como resuelto, quita 1 vida y guarda tiempo
            }
        }
        else
        {
            Debug.Log("Secuencia incorrecta. Inténtalo de nuevo.");
            ResetAllButtons();
            playerSequence.Clear();
        }
    }

    private bool CheckSequenceSoFar()
    {
        for (int i = 0; i < playerSequence.Count; i++)
        {
            if (playerSequence[i] != correctSequence[i])
            {
                return false;
            }
        }
        return true;
    }

    private void ResetAllButtons()
    {
        foreach (var btn in botonesRegistrados)
        {
            btn.ResetToDefaultMaterial();
        }
    }
}
