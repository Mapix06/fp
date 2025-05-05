using UnityEngine;
using TMPro;

public class PuzzleController : MonoBehaviour
{
    public FinalBossController boss;
    public GameObject[] puzzles;
    public float tiempoMaximo = 30f;
    public TextMeshProUGUI textoTemporizador;

    private int puzzleActual = -1;
    private float tiempoRestante;
    private bool enPuzzle = false;

    void Start()
    {
        ActivarSiguientePuzzle();
    }

    void Update()
    {
        if (!enPuzzle) return;

        tiempoRestante -= Time.deltaTime;
        if (textoTemporizador != null)
            textoTemporizador.text = Mathf.Ceil(tiempoRestante).ToString(); // <--- solo el número

        if (tiempoRestante <= 0)
        {
            FallarPuzzle();
        }
    }

    public void ResolverPuzzle()
    {
        if (puzzles[puzzleActual] != null)
            puzzles[puzzleActual].SetActive(false);

        boss.PerderVida();
        ActivarSiguientePuzzle();
    }

    void FallarPuzzle()
    {
        if (puzzles[puzzleActual] != null)
            puzzles[puzzleActual].SetActive(false);

        boss.AtacarJugador();
        ActivarSiguientePuzzle();
    }

    void ActivarSiguientePuzzle()
    {
        puzzleActual++;

        if (puzzleActual >= puzzles.Length)
        {
            Debug.Log("Todos los puzzles han sido completados o fallados.");
            enPuzzle = false;
            textoTemporizador.text = "";
            return;
        }

        puzzles[puzzleActual].SetActive(true);
        tiempoRestante = tiempoMaximo;
        enPuzzle = true;
    }
}
