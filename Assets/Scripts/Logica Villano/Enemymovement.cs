using UnityEngine;

[RequireComponent(typeof(EnemyDetector))]
[RequireComponent(typeof(EnemyPatrol))]
public class EnemyMover : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadCaminar = 3.5f;
    public float velocidadCorrer = 6f;
    public float distanciaMinima = 1.5f;
    public float rangoCorrer = 4f;
    public float velocidadRotacion = 10f;

    [Header("Ataque")]
    public float cooldownAtaque = 2f;
    private float temporizadorAtaque = 0f;
    private bool atacando = false;
    public EnemyHitbox hitboxAtaque;

    [Header("Ground Snapping")]
    public LayerMask capaSuelo;
    public float alturaOrigenRayo = 1f;
    public float distanciaRayo = 3f;
    public float offsetSuelo = 0.05f;
    public float velocidadCaida = 5f;

    [Header("Patrulla")]
    private EnemyDetector detector;
    private EnemyPatrol patrol;
    private Rigidbody rb;
    private Animator animator;

    private const int ESTADO_IDLE = 0;
    private const int ESTADO_CAMINAR = 1;
    private const int ESTADO_CORRER = 2;

    void Start()
    {
        detector = GetComponent<EnemyDetector>();
        patrol = GetComponent<EnemyPatrol>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (temporizadorAtaque > 0f)
        {
            temporizadorAtaque -= Time.fixedDeltaTime;
        }

        int estadoActual;

        if (atacando)
        {
          
            return;
        }

        if (detector.jugadorDetectado && detector.jugador != null)
        {
            estadoActual = Perseguir();
        }
        else
        {
            patrol.MoverPatrulla();
            estadoActual = patrol.EstaEsperando() ? ESTADO_IDLE : ESTADO_CAMINAR;
        }

        AjustarAlturaAlSuelo();

        if (animator != null)
        {
            animator.SetInteger("estadoMovimiento", estadoActual);
        }
    }

    private void AjustarAlturaAlSuelo()
    {
        Vector3 origenRayo = rb.position + Vector3.up * alturaOrigenRayo;

        if (Physics.Raycast(origenRayo, Vector3.down, out RaycastHit hit, distanciaRayo, capaSuelo))
        {
            float alturaObjetivo = hit.point.y + offsetSuelo;
            Vector3 posicionCorregida = rb.position;
            posicionCorregida.y = Mathf.MoveTowards(rb.position.y, alturaObjetivo, velocidadCaida * Time.fixedDeltaTime);
            rb.MovePosition(posicionCorregida);
        }
    }

    private int Perseguir()
    {
        Vector3 direccion = detector.jugador.position - transform.position;
        direccion.y = 0f;
        float distanciaActual = direccion.magnitude;
        direccion.Normalize();

        if (direccion != Vector3.zero)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, rotacionObjetivo, velocidadRotacion * Time.fixedDeltaTime));
        }

        if (distanciaActual <= distanciaMinima)
        {
            if (temporizadorAtaque <= 0f)
            {
                Atacar();
            }
            return ESTADO_IDLE;
        }

        float velocidadActual;
        int estado;

        if (distanciaActual <= rangoCorrer)
        {
            velocidadActual = velocidadCaminar;
            estado = ESTADO_CAMINAR;
        }
        else
        {
            velocidadActual = velocidadCorrer;
            estado = ESTADO_CORRER;
        }

        Vector3 nuevaPosicion = rb.position + direccion * velocidadActual * Time.fixedDeltaTime;
        rb.MovePosition(nuevaPosicion);

        return estado;
    }
    // Llamado por Animation Event — reenvía la llamada al hitbox real
    public void ActivarHitbox()
    {
        if (hitboxAtaque != null) hitboxAtaque.ActivarHitbox();
    }

    public void DesactivarHitbox()
    {
        if (hitboxAtaque != null) hitboxAtaque.DesactivarHitbox();
    }
    private void Atacar()
    {
        atacando = true;
        temporizadorAtaque = cooldownAtaque;
        animator.SetTrigger("atacar");
    }

   
    public void TerminarAtaque()
    {
        atacando = false;
    }
}