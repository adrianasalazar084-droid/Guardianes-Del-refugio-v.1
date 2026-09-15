using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Velocidades")]

    [SerializeField] private float velocidadCaminar = 1f;
    [SerializeField] private float velocidadCorrer = 2f;


    [Header("Distancias")]

    [SerializeField] private float distanciaSeguimiento = 1f;
    [SerializeField] private float distanciaCorrer = 3f;


    [Header("Referencias")]

    [SerializeField] private Animator anim;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private EnemyAttack enemyAttack;
    [SerializeField] private EnemyKnockback enemyKnockback;


    private void Awake()
    {
        if (anim == null)
            anim = GetComponent<Animator>();

        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (enemyHealth == null)
            enemyHealth = GetComponent<EnemyHealth>();

        if (enemyAttack == null)
            enemyAttack = GetComponent<EnemyAttack>();

        if (enemyKnockback == null)
            enemyKnockback = GetComponent<EnemyKnockback>();

        agent.stoppingDistance = distanciaSeguimiento;
    }

    public void Perseguir(Transform objetivo)
    {
        if (enemyHealth != null && enemyHealth.EstaMuerto)
        {
            Detener();
            return;
        }

        if (enemyAttack != null && enemyAttack.EstaAtacando)
        {
            Detener();
            return;
        }

        // Mientras dura el knockback, el propio EnemyKnockback controla el movimiento.
        if (enemyKnockback != null && enemyKnockback.EnKnockback)
        {
            return;
        }

        if (objetivo == null)
            return;

        float distancia = Vector3.Distance(transform.position, objetivo.position);

        agent.isStopped = false;
        agent.SetDestination(objetivo.position);

        if (distancia > distanciaCorrer)
        {
            agent.speed = velocidadCorrer;
            anim.SetFloat("Velocidad", 1f);
        }
        else
        {
            agent.speed = velocidadCaminar;
            anim.SetFloat("Velocidad", 0.5f);
        }
    }

    public void Detener()
    {
        // No pisamos el knockback si está en curso.
        if (enemyKnockback != null && enemyKnockback.EnKnockback)
            return;

        agent.isStopped = true;
        anim.SetFloat("Velocidad", 0f);
    }

    public bool HaLlegado()
    {
        return !agent.pathPending &&
               agent.remainingDistance <= agent.stoppingDistance;
    }
}