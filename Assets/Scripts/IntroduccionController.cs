using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroduccionController : MonoBehaviour
{
    public GameObject panelPausa;
    public GameObject panelControles;

    private bool juegoPausado = false;

    void Start()
    {
        if (panelPausa != null)
            panelPausa.SetActive(false);

        if (panelControles != null)
            panelControles.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePausa();
        }
    }

    // Botón Pausar
    public void TogglePausa()
    {
        juegoPausado = !juegoPausado;

        if (juegoPausado)
        {
            panelPausa.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else
        {
            panelPausa.SetActive(false);
            panelControles.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }

    // Botón CONTINUE
    public void ContinuarJuego()
    {
        juegoPausado = false;
        panelPausa.SetActive(false);
        panelControles.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    // Botón CONTROLS dentro del panel de pausa
    public void MostrarControles()
    {
        if (panelControles != null)
            panelControles.SetActive(true);
        if (panelPausa != null)
            panelPausa.SetActive(false);
    }

    // Botón de regresar desde CONTROLS
    public void CerrarControles()
    {
        if (panelControles != null)
            panelControles.SetActive(false);
        if (panelPausa != null)
            panelPausa.SetActive(true);
    }

    // Botón MENU (volver al menú principal)
    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1f; // Por si estaba pausado
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Menu"); // Asegúrate de que la escena se llame exactamente "Menu"
    }

    // Botón EXIT (salir del juego)
    public void SalirDelJuego()
    {
        Application.Quit();
    }
}
