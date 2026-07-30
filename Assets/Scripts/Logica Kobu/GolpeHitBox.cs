using UnityEngine;

public class GolpeHitbox : MonoBehaviour
{
    [SerializeField] private int daño = 30;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // ¿Golpeamos un enemigo?
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.RecibirDaño(daño);
            return;
        }

        // ¿Golpeamos un objeto destructible?
        Destructible destructible = other.GetComponent<Destructible>();

        if (destructible != null)
        {
            destructible.Romper();
        }
    }
}