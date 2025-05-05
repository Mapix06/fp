using UnityEngine;

public class Recolectable : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            ContadorRecolectables contador = FindObjectOfType<ContadorRecolectables>();
            if (contador != null)
            {
                contador.AumentarContador();
            }

            Destroy(gameObject);
        }
    }
}
