using UnityEngine;

public class Destructible : MonoBehaviour
{

    /// Destruye el objeto.
    /// Más adelante aquí agregaremos:
    /// - Sonido de destrucción.
    /// - Partículas.
    /// - Animación.

    public void Romper()
    {
        Debug.Log(gameObject.name + " ha sido destruido.");

        // Soltamos monedas si el objeto tiene el script.
        SoltarMonedas soltarMonedas = GetComponent<SoltarMonedas>();

        if (soltarMonedas != null)
        {
            soltarMonedas.Soltar();
        }

        Destroy(gameObject);
    }
}