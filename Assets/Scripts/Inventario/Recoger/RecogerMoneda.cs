using UnityEngine;

public class RecogerMoneda : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventario inventario = other.GetComponent<Inventario>();

            if (inventario != null)
            {
                inventario.AgregarMoneda();
            }

            Destroy(gameObject);
        }
    }
}