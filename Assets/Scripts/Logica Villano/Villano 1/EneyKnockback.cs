using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyKnockback : MonoBehaviour
{
    [Header("Knockback")]
    public float fuerza = 6f;
    public float duracion = 0.30f; // Duración del empujón en segundos. sirve para que el enemigo no se mueva mientras dura el empujón.

    private NavMeshAgent agent;
    private Coroutine knockbackCoroutine;

    // Permite a EnemyMovement saber que debe quedarse quieto mientras dura el empujón.
    public bool EnKnockback { get; private set; }


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    /// <summary>
    /// Empuja al enemigo alejándolo del punto de origen del golpe.
    /// </summary>
    public void AplicarKnockback(Vector3 origenGolpe)
    {
        Vector3 direccion = transform.position - origenGolpe;
        direccion.y = 0f;

        // Si el golpe vino exactamente desde la misma posición, no hay dirección válida.
        if (direccion.sqrMagnitude < 0.001f)
            return;

        direccion.Normalize();

        if (knockbackCoroutine != null)
            StopCoroutine(knockbackCoroutine);

        knockbackCoroutine = StartCoroutine(KnockbackCoroutine(direccion));
    }


    private IEnumerator KnockbackCoroutine(Vector3 direccion)
    {
        EnKnockback = true;
        agent.isStopped = true;

        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracion)
        {
            // agent.Move respeta el NavMesh, evitando que el enemigo atraviese paredes.
            agent.Move(direccion * fuerza * Time.deltaTime);

            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }

        agent.isStopped = false;
        EnKnockback = false;
        knockbackCoroutine = null;
    }
}