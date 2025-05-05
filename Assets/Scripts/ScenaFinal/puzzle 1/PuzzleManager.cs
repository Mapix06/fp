using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("Puzzle Configuration")]
    [Tooltip("El orden correcto en que se deben presionar los botones (IDs)")]
    public int[] correctSequence = { 1, 3, 2, 4 };

    private List<int> playerSequence = new List<int>();
    private List<ButtonInteractable> botonesRegistrados = new List<ButtonInteractable>();
    private bool puzzleSolved = false;

    // Llamado por cada botón al Start
    public void RegistrarBoton(ButtonInteractable boton)
    {
        if (!botonesRegistrados.Contains(boton))
        {
            botonesRegistrados.Add(boton);
        }
    }

    // Llamado por cada botón cuando se presiona
    public void ButtonPressed(int buttonID)
    {
        if (puzzleSolved) return;

        playerSequence.Add(buttonID);

        if (CheckSequenceSoFar())
        {
            if (playerSequence.Count == correctSequence.Length)
            {
                puzzleSolved = true;
                Debug.Log("¡Felicidades! Pasa al siguiente puzzle.");
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
