using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using UnityEngine;


public class GolpeHitbox : MonoBehaviour
{
    [SerializeField] private int daño = 30;


    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.RecibirDaño(daño);
        }
    }
}

