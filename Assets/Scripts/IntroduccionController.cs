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

    // Bot�n Pausar
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

    // Bot�n CONTINUE
    public void ContinuarJuego()
    {
        juegoPausado = false;
        panelPausa.SetActive(false);
        panelControles.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    // Bot�n CONTROLS dentro del panel de pausa
    public void MostrarControles()
    {
        if (panelControles != null)
            panelControles.SetActive(true);
        if (panelPausa != null)
            panelPausa.SetActive(false);
    }

    // Bot�n de regresar desde CONTROLS
    public void CerrarControles()
    {
        if (panelControles != null)
            panelControles.SetActive(false);
        if (panelPausa != null)
            panelPausa.SetActive(true);
    }

    // Bot�n MENU (volver al men� principal)
    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1f; // Por si estaba pausado
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Menu"); // Aseg�rate de que la escena se llame exactamente "Menu"
    }

    // Bot�n EXIT (salir del juego)
    public void SalirDelJuego()
    {
        Application.Quit();
    }
}
