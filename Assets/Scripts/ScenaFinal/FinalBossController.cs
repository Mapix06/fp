using UnityEngine;

public class FinalBossController : MonoBehaviour
{
    [Header("Estadísticas")]
    public int vidas = 5;

    [Header("Referencias")]
    public ParticleSystem ataqueMagico;
    public Transform objetivoJugador;

    [Header("Efectos")]
    public GameObject efectoDerrota;

    void Start()
    {
        if (objetivoJugador == null)
        {
            GameObject jugador = GameObject.FindGameObjectWithTag("Player");
            if (jugador != null)
            {
                objetivoJugador = jugador.transform;
                Debug.Log("Objetivo del jugador encontrado automáticamente.");
            }
            else
            {
                Debug.LogWarning("No se encontró al jugador.");
            }
        }
    }

    public void AtacarJugador()
    {
        if (ataqueMagico != null && objetivoJugador != null)
        {
            ataqueMagico.transform.LookAt(objetivoJugador);
            ataqueMagico.Play();
            Debug.Log("El jefe ha atacado al jugador.");
        }
    }

    public void PerderVida()
    {
        vidas--;
        Debug.Log("El jefe ha perdido una vida. Vidas restantes: " + vidas);

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

    void Derrotado()
    {
        Debug.Log("¡Final Boss derrotado!");

        if (efectoDerrota != null)
        {
            Instantiate(efectoDerrota, transform.position, Quaternion.identity);
        }

        // Guardar resumen al vencer al jefe
        GameManager.Instance?.GuardarTiemposEnJSON();

        gameObject.SetActive(false);

        // Aquí puedes cargar una escena de victoria si deseas
        // SceneManager.LoadScene("Victoria");
    }
}
