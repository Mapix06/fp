using UnityEngine;
using TMPro;

public class ContadorRecolectables : MonoBehaviour
{
    public TextMeshProUGUI contadorTexto;
    private int contador = 0;

    void Start()
    {
        ActualizarTexto();
    }

    public void AumentarContador()
    {
        contador++;
        ActualizarTexto();
    }

    void ActualizarTexto()
    {
        contadorTexto.text = contador.ToString();
    }
}
