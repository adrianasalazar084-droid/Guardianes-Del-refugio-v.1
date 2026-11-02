using UnityEngine;

public class GolpeHitbox : MonoBehaviour
{
    [SerializeField] private int daño = 30;

    [Header("Multiplicador de daño")]
    [Tooltip("Se busca solo en Kobu. Asignalo a mano si este hitbox no es hijo de Kobu.")]
    [SerializeField] private KobuDamage kobuDamage;

    [Header("Partícula de impacto")]
    [SerializeField] private GameObject particulaGolpe;

    // Si está asignada, se usa en lugar de particulaGolpe (para skills especiales).
    private GameObject particulaOverride;


    private void Awake()
    {
        if (kobuDamage == null)
            kobuDamage = GetComponentInParent<KobuDamage>();

        if (kobuDamage == null)
            kobuDamage = FindFirstObjectByType<KobuDamage>();
    }


    private void OnTriggerEnter(Collider other)
    {
        // ¿Golpeamos un enemigo?
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            // El daño base se multiplica por el boost de la skill 3 y las mejoras permanentes
            int dañoFinal = (kobuDamage != null) ? kobuDamage.Calcular(daño) : daño;

            enemyHealth.RecibirDaño(dañoFinal, transform.position);

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