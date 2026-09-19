using UnityEngine;

public class RecogerMoneda : MonoBehaviour
{
    [Header("Sonido")]
    public AudioClip sonidoRecoger;
    [Range(0f, 1f)] public float volumenRecoger = 0.8f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventario inventario = other.GetComponent<Inventario>();

            if (inventario != null)
            {
                inventario.AgregarMoneda();
            }

            // Sonido al recoger.
            if (sonidoRecoger != null)
            {
                AudioSource.PlayClipAtPoint(sonidoRecoger, transform.position, volumenRecoger);
            }

            Destroy(gameObject);
        }
    }
}