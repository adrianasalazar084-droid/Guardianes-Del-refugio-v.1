using UnityEngine;

public class DestruirJaula : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventario inventario = other.GetComponent<Inventario>();

            if (inventario != null)
            {
                if (inventario.llaves > 0)
                {
                    inventario.UsarLlave();
                    Destroy(gameObject);
                    Debug.Log("Jaula destruida");
                }
            }
        }
    }
}