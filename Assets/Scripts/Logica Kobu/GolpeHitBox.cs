using UnityEngine;

public class GolpeHitbox : MonoBehaviour
{
    [SerializeField] private int daño = 30;

    [Header("Partícula de impacto")]
    [SerializeField] private GameObject particulaGolpe;

    void Start()
    {

    }

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
        if (particulaGolpe == null)
            return;

        // Punto más cercano del collider golpeado a la hitbox, para que la partícula
        // aparezca justo en el punto de contacto en vez del centro del enemigo.
        Vector3 puntoImpacto = objetivo.ClosestPoint(transform.position);

        Instantiate(particulaGolpe, puntoImpacto, Quaternion.identity);
    }
}