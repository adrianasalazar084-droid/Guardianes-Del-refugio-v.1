using UnityEngine;

public class GolpeHitbox : MonoBehaviour
{
    [SerializeField] private int daño = 30;

    [Header("Partícula de impacto")]
    [SerializeField] private GameObject particulaGolpe;

    // Si está asignada, se usa en lugar de particulaGolpe (para skills especiales).
    private GameObject particulaOverride;


    private void OnTriggerEnter(Collider other)
    {
        // ¿Golpeamos un enemigo?
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.RecibirDaño(daño, transform.position);

            InstanciarParticula(other);
            return;
        }

        // ¿Golpeamos un objeto destructible?
        Destructible destructible = other.GetComponent<Destructible>();

        if (destructible != null)
        {
            destructible.Romper();

            InstanciarParticula(other);
        }
    }


    private void InstanciarParticula(Collider objetivo)
    {
        GameObject particulaAUsar = (particulaOverride != null) ? particulaOverride : particulaGolpe;

        if (particulaAUsar == null)
            return;

        Vector3 puntoImpacto = objetivo.ClosestPoint(transform.position);

        Instantiate(particulaAUsar, puntoImpacto, Quaternion.identity);
    }


    /// <summary>
    /// Reemplaza temporalmente la partícula de impacto (usado por skills especiales).
    /// Llamar a LimpiarParticulaOverride() cuando termine la ventana de la skill.
    /// </summary>
    public void SetParticulaOverride(GameObject particula)
    {
        particulaOverride = particula;
    }


    /// <summary>
    /// Vuelve a usar la partícula normal del golpe.
    /// </summary>
    public void LimpiarParticulaOverride()
    {
        particulaOverride = null;
    }
}