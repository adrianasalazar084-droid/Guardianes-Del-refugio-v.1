using UnityEngine;

public class Destructible : MonoBehaviour
{
    
    /// Destruye el objeto.
    /// Más adelante aquí agregaremos:
    /// - Sonido de destrucción.
    /// - Partículas.
    /// - Animación.
    /// - Objetos que suelte (loot).
  
    public void Romper()
    {
        Debug.Log(gameObject.name + " ha sido destruido.");

        Destroy(gameObject);
    }
}