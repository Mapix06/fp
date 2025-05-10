using UnityEngine;

public class PuzzleTimerManager : MonoBehaviour
{
    [Header("Configuración del Tiempo")]
    public float tiempoLimiteDerrota = 40f;
    public float tiempoEsperaVictoria = 30f;

    [Header("Configuración de lógica")]
    public int vidasARestar = 1;

    [Header("Referencias")]
    public FinalBossController bossController;
    public GameObject puzzleActual;
    public GameObject siguientePuzzle; // El siguiente GameObject del puzzle a activar

    private float tiempoInicio;
    private float tiempoRestante;
    private bool cronometroActivo = false;
    private bool puzzleGanado = false;
    private bool primerClickDetectado = false;

    void Start()
    {
        tiempoRestante = tiempoLimiteDerrota;

        if (bossController == null)
            bossController = FindObjectOfType<FinalBossController>();

        if (puzzleActual == null)
            puzzleActual = this.gameObject;
    }

    void Update()
    {
        if (cronometroActivo)
        {
            tiempoRestante -= Time.deltaTime;

            if (tiempoRestante <= 0f && !puzzleGanado)
            {
                DerrotaPorTiempo();
            }
        }
    }

    public void IniciarCronometro()
    {
        if (!primerClickDetectado)
        {
            primerClickDetectado = true;
            tiempoInicio = Time.time;
            tiempoRestante = tiempoLimiteDerrota;
            cronometroActivo = true;
            Debug.Log("? Cronómetro iniciado");
        }
    }

    public void PuzzleCompletado()
    {
        if (puzzleGanado) return;

        puzzleGanado = true;
        cronometroActivo = false;

        float duracion = Time.time - tiempoInicio;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegistrarTiempoPuzzle(puzzleActual.name, duracion);
            Debug.Log($"? {puzzleActual.name} completado en {duracion:F2} segundos");
        }

        if (bossController != null)
        {
            for (int i = 0; i < vidasARestar; i++)
            {
                bossController.PerderVida();
            }
        }

        Invoke(nameof(ActivarSiguientePuzzle), tiempoEsperaVictoria);
    }

    void DerrotaPorTiempo()
    {
        cronometroActivo = false;
        Debug.Log("? Tiempo agotado. El jugador ha perdido.");
        bossController?.AtacarJugador();

        // Reiniciar el puzzle actual
        // Aquí puedes añadir lógica para reiniciarlo visualmente o desactivarlo
    }

    void ActivarSiguientePuzzle()
    {
        if (siguientePuzzle != null)
        {
            puzzleActual.SetActive(false);
            siguientePuzzle.SetActive(true);
            Debug.Log("?? Siguiente puzzle activado: " + siguientePuzzle.name);
        }
        else
        {
            Debug.Log("?? No hay siguiente puzzle definido. Juego completo.");
        }
    }
}
