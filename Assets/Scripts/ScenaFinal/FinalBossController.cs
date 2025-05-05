using UnityEngine;

public class FinalBossController : MonoBehaviour
{
    [Header("Estadísticas")]
    [Tooltip("Número de vidas del jefe final")]
    public int vidas = 5;

    [Header("Referencias")]
    [Tooltip("Sistema de partículas para el ataque mágico")]
    public ParticleSystem ataqueMagico;

    [Tooltip("Objetivo al que atacará el jefe")]
    public Transform objetivoJugador;

    [Header("Efectos")]
    [Tooltip("Efecto de explosión al ser derrotado")]
    public GameObject efectoDerrota;

    void Start()
    {
        // Verificar referencias importantes al inicio
        if (objetivoJugador == null)
        {
            // Intenta encontrar al jugador automáticamente
            GameObject jugador = GameObject.FindGameObjectWithTag("Player");
            if (jugador != null)
            {
                objetivoJugador = jugador.transform;
                Debug.Log("Objetivo del jugador encontrado automáticamente.");
            }
            else
            {
                Debug.LogWarning("No se encontró al jugador. Arrastra un GameObject con el tag 'Player' al campo objetivoJugador.");
            }
        }
    }

    /// <summary>
    /// Hace que el jefe ataque al jugador activando el sistema de partículas
    /// </summary>
    public void AtacarJugador()
    {
        if (ataqueMagico != null && objetivoJugador != null)
        {
            ataqueMagico.transform.LookAt(objetivoJugador);
            ataqueMagico.Play();
            Debug.Log("El jefe ha atacado al jugador.");
        }
        else
        {
            Debug.LogError("No se puede atacar: falta el sistema de partículas o el objetivo del jugador.");
        }
    }

    /// <summary>
    /// Reduce la vida del jefe en 1 y verifica si ha sido derrotado
    /// </summary>
    public void PerderVida()
    {
        vidas--;
        Debug.Log("El jefe ha perdido una vida. Vidas restantes: " + vidas);

        // Efecto visual opcional al perder vida
        if (ataqueMagico != null)
        {
            ataqueMagico.Stop();
            ataqueMagico.Play();
        }

        if (vidas <= 0)
        {
            Derrotado();
        }
    }

    /// <summary>
    /// Gestiona la derrota del jefe final
    /// </summary>
    void Derrotado()
    {
        Debug.Log("¡Final Boss derrotado!");

        // Activar efecto de derrota si existe
        if (efectoDerrota != null)
        {
            Instantiate(efectoDerrota, transform.position, Quaternion.identity);
        }

        // Desactivar el GameObject del jefe
        gameObject.SetActive(false);

        // Aquí puedes cargar la escena final o mostrar victoria
        // GameManager.Instance.MostrarVictoria();
    }
}