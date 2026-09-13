using UnityEngine;

public class RecogerLlave : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventario inventario = other.GetComponent<Inventario>();

            if (inventario != null)
            {
                inventario.AgregarLlave();
            }

            Destroy(gameObject);
        }
    }
}