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


    private void Awake()
    {
        if (anim == null)
            anim = GetComponent<Animator>();

        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

    
        agent.stoppingDistance = distanciaSeguimiento;
    }

    public void Perseguir(Transform objetivo)
    {
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
        agent.isStopped = true;
        anim.SetFloat("Velocidad", 0f);
    }

 
    public bool HaLlegado()
    {
        return !agent.pathPending &&
               agent.remainingDistance <= agent.stoppingDistance;
    }
}