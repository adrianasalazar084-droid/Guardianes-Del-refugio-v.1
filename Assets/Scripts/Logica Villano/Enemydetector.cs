using UnityEngine;

// UNICA RESPONSABILIDAD: detectar si el jugador está dentro del radio de alcance.

public class EnemyDetector : MonoBehaviour
{
    [Header("Configuración de detección")]
    public float radioDeteccion = 8f;
    public LayerMask capaJugador;
    public bool jugadorDetectado { get; private set; }
    public Transform jugador { get; private set; }

    void Update()
    {
        Collider[] resultado = Physics.OverlapSphere(transform.position, radioDeteccion, capaJugador);

        if (resultado.Length > 0)
        {
            jugadorDetectado = true;
            jugador = resultado[0].transform;
        }
        else
        {
            jugadorDetectado = false;
            jugador = null;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }
}