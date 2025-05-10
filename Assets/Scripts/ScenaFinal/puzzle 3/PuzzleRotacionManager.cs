using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleRotacionManager : MonoBehaviour
{
    [System.Serializable]
    public class PuzzleSolution
    {
        public int statueID;
        public float requiredAngle;
    }

    [Header("Configuración del Puzzle")]
    public List<PuzzleSolution> solution = new List<PuzzleSolution>
    {
        new PuzzleSolution { statueID = 0, requiredAngle = 0f },
        new PuzzleSolution { statueID = 1, requiredAngle = 90f },
        new PuzzleSolution { statueID = 2, requiredAngle = 180f },
        new PuzzleSolution { statueID = 3, requiredAngle = 270f }
    };

    [Header("Eventos al Resolver")]
    public UnityEvent onPuzzleSolved;
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDeactivate;
    public AudioClip puzzleSolvedSound;
    public float delayBeforeAction = 1f;

    private List<EstatuaRotable> estatuas = new List<EstatuaRotable>();
    private bool puzzleResuelto = false;
    private AudioSource audioSource;
    private PuzzleTimerManager timerManager;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        timerManager = FindObjectOfType<PuzzleTimerManager>();
    }

    public void RegistrarEstatua(EstatuaRotable estatua)
    {
        if (!estatuas.Contains(estatua))
        {
            estatuas.Add(estatua);
            Debug.Log($"Estatua {estatua.GetID()} registrada", estatua.gameObject);
        }
    }

    public void ValidarPuzzle()
    {
        if (puzzleResuelto || estatuas.Count < solution.Count) return;

        Debug.Log("Validando puzzle...");
        bool todasCorrectas = true;

        foreach (var item in solution)
        {
            EstatuaRotable estatua = estatuas.Find(e => e.GetID() == item.statueID);
            if (estatua == null)
            {
                Debug.LogError($"Estatua con ID {item.statueID} no encontrada");
                todasCorrectas = false;
                break;
            }

            float anguloActual = estatua.GetAnguloActual();
            if (!Mathf.Approximately(anguloActual, item.requiredAngle))
            {
                Debug.Log($"Estatua {item.statueID} en ángulo incorrecto: {anguloActual}° (debería ser {item.requiredAngle}°)", estatua.gameObject);
                todasCorrectas = false;
                break;
            }
        }

        if (todasCorrectas)
        {
            timerManager?.PuzzleCompletado(); // Marca victoria y quita vidas
            PuzzleResuelto();
        }
    }

    void PuzzleResuelto()
    {
        puzzleResuelto = true;
        Debug.Log("¡Puzzle resuelto correctamente!");

        if (puzzleSolvedSound != null) audioSource.PlayOneShot(puzzleSolvedSound);
        onPuzzleSolved.Invoke();
        Invoke("ExecutePostSolveActions", delayBeforeAction);
    }

    void ExecutePostSolveActions()
    {
        foreach (GameObject obj in objectsToActivate) obj.SetActive(true);
        foreach (GameObject obj in objectsToDeactivate) obj.SetActive(false);
        FindObjectOfType<FinalBossController>()?.PerderVida();
        FindObjectOfType<FinalBossController>()?.PerderVida(); // Se quitan 2 vidas
    }

    [ContextMenu("Reiniciar Puzzle")]
    public void ReiniciarPuzzle()
    {
        puzzleResuelto = false;
        foreach (EstatuaRotable estatua in estatuas)
        {
            estatua.transform.rotation = Quaternion.identity;
        }
    }

    void OnGUI()
    {
        if (estatuas.Count == solution.Count)
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            GUILayout.Label("<size=16><b>Estado del Puzzle</b></size>");

            foreach (var item in solution)
            {
                EstatuaRotable estatua = estatuas.Find(e => e.GetID() == item.statueID);
                if (estatua != null)
                {
                    float anguloActual = estatua.GetAnguloActual();
                    bool correcto = Mathf.Approximately(anguloActual, item.requiredAngle);

                    GUILayout.Label($"Estatua {item.statueID}: {anguloActual}° / {item.requiredAngle}° " +
                                  (correcto ? "<color=green>?</color>" : "<color=red>?</color>"));
                }
            }

            GUILayout.EndArea();
        }
    }
}
