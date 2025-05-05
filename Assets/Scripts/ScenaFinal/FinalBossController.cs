using UnityEngine;

public class FinalBossController : MonoBehaviour
{
    [Header("Estad�sticas")]
    [Tooltip("N�mero de vidas del jefe final")]
    public int vidas = 5;

    [Header("Referencias")]
    [Tooltip("Sistema de part�culas para el ataque m�gico")]
    public ParticleSystem ataqueMagico;

    [Tooltip("Objetivo al que atacar� el jefe")]
    public Transform objetivoJugador;

    [Header("Efectos")]
    [Tooltip("Efecto de explosi�n al ser derrotado")]
    public GameObject efectoDerrota;

    void Start()
    {
        // Verificar referencias importantes al inicio
        if (objetivoJugador == null)
        {
            // Intenta encontrar al jugador autom�ticamente
            GameObject jugador = GameObject.FindGameObjectWithTag("Player");
            if (jugador != null)
            {
                objetivoJugador = jugador.transform;
                Debug.Log("Objetivo del jugador encontrado autom�ticamente.");
            }
            else
            {
                Debug.LogWarning("No se encontr� al jugador. Arrastra un GameObject con el tag 'Player' al campo objetivoJugador.");
            }
        }
    }

    /// <summary>
    /// Hace que el jefe ataque al jugador activando el sistema de part�culas
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
            Debug.LogError("No se puede atacar: falta el sistema de part�culas o el objetivo del jugador.");
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
        Debug.Log("�Final Boss derrotado!");

        // Activar efecto de derrota si existe
        if (efectoDerrota != null)
        {
            Instantiate(efectoDerrota, transform.position, Quaternion.identity);
        }

        // Desactivar el GameObject del jefe
        gameObject.SetActive(false);

        // Aqu� puedes cargar la escena final o mostrar victoria
        // GameManager.Instance.MostrarVictoria();
    }
}