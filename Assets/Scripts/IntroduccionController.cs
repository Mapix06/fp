using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroduccionController : MonoBehaviour
{
    public GameObject panelPausa;
    public GameObject panelControles;
    public GameObject misionesPanel; 

    private bool juegoPausado = false;

    void Start()
    {
        if (panelPausa != null)
            panelPausa.SetActive(false);

        if (panelControles != null)
            panelControles.SetActive(false);

        if (misionesPanel != null)
            misionesPanel.SetActive(true); // Mostrar al inicio

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

    public void TogglePausa()
    {
        juegoPausado = !juegoPausado;

        if (juegoPausado)
        {
            if (panelPausa != null)
                panelPausa.SetActive(true);
            if (misionesPanel != null)
                misionesPanel.SetActive(false); // Ocultar misiones

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else
        {
            if (panelPausa != null)
                panelPausa.SetActive(false);
            if (panelControles != null)
                panelControles.SetActive(false);
            if (misionesPanel != null)
                misionesPanel.SetActive(true); // Volver a mostrar misiones

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }

    public void ContinuarJuego()
    {
        juegoPausado = false;

        if (panelPausa != null)
            panelPausa.SetActive(false);
        if (panelControles != null)
            panelControles.SetActive(false);
        if (misionesPanel != null)
            misionesPanel.SetActive(true); // Asegurar que se muestra

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void MostrarControles()
    {
        if (panelControles != null)
            panelControles.SetActive(true);
        if (panelPausa != null)
            panelPausa.SetActive(false);
    }

    public void CerrarControles()
    {
        if (panelControles != null)
            panelControles.SetActive(false);
        if (panelPausa != null)
            panelPausa.SetActive(true);
    }

    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Menu");
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }
}
