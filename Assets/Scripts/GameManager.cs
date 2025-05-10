using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Dictionary<string, float> tiemposPorPuzzle = new();
    public int recolectablesTotales = 0;
    public int vidasJugador = 5;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegistrarTiempoPuzzle(string nombrePuzzle, float duracion)
    {
        if (!tiemposPorPuzzle.ContainsKey(nombrePuzzle))
            tiemposPorPuzzle[nombrePuzzle] = duracion;
    }

    public float ObtenerTiempoPuzzle(string nombrePuzzle)
    {
        return tiemposPorPuzzle.ContainsKey(nombrePuzzle) ? tiemposPorPuzzle[nombrePuzzle] : -1f;
    }

    public void GuardarTiemposEnJSON(string nombreArchivo = "resumen_final.json")
    {
        List<PuzzleTiempo> listaTiempos = new();

        foreach (var kvp in tiemposPorPuzzle)
        {
            listaTiempos.Add(new PuzzleTiempo
            {
                nombrePuzzle = kvp.Key,
                duracion = kvp.Value
            });
        }

        DatosJuego resumen = new DatosJuego
        {
            tiempos = listaTiempos,
            recolectablesTotales = this.recolectablesTotales,
            vidasRestantes = this.vidasJugador
        };

        string json = JsonUtility.ToJson(resumen, true);
        string ruta = Path.Combine(Application.persistentDataPath, nombreArchivo);
        File.WriteAllText(ruta, json, Encoding.UTF8);
        Debug.Log("Resumen final guardado en: " + ruta);
    }

    [System.Serializable]
    public class PuzzleTiempo
    {
        public string nombrePuzzle;
        public float duracion;
    }

    [System.Serializable]
    public class DatosJuego
    {
        public List<PuzzleTiempo> tiempos;
        public int recolectablesTotales;
        public int vidasRestantes;
    }
}
