using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [Header("Referencias")]

    [SerializeField] private Transform jugador;

  
    [SerializeField] private EnemyMovement enemyMovement;

    [SerializeField] private KobuHealth kobuHealth;

    [Header("Detección")]

    
    [SerializeField] private float radioDeteccion = 5f;
    [SerializeField] private float distanciaAtaque = 2f;
    [SerializeField] private EnemyAttack enemyAttack;


    private void Awake()
    {
        if (enemyMovement == null)
            enemyMovement = GetComponent<EnemyMovement>();

        if (enemyAttack == null)
            enemyAttack = GetComponent<EnemyAttack>();

        if (kobuHealth == null && jugador != null)
        {
            kobuHealth = jugador.GetComponent<KobuHealth>();
        }
    }
   

    private void Update()
    {
        // Si Kobu murió, dejamos de perseguir y atacar.
        if (kobuHealth != null && kobuHealth.EstaMuerto)
        {
            enemyMovement.Detener();
            return;
        }

        if (jugador == null)
            return;

        
        float distancia = Vector3.Distance(transform.position, jugador.position);

        // Si el jugador está fuera del radio de detección,
        // el enemigo permanece quieto.
        if (distancia > radioDeteccion)
        {
            enemyMovement.Detener();
            return; 
        }

        // Si el jugador está a distancia de ataque,
        // dejamos de movernos y atacamos.
        if (distancia <= distanciaAtaque)
        {
            enemyMovement.Detener();
            enemyAttack.Atacar();
        }
        else
        {
            // Si todavía no está a distancia de ataque,
            // seguimos persiguiéndolo.
            enemyMovement.Perseguir(jugador);
        }
    }

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }
}