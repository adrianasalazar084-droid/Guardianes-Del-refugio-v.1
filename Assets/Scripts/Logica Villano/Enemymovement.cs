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

    [Header("Ground Snapping")]
    public LayerMask capaSuelo;
    public float alturaOrigenRayo = 1f; 
    public float distanciaRayo = 3f;    
    public float offsetSuelo = 0.05f;   

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
        int estadoActual;

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
            Vector3 posicionCorregida = rb.position;
            posicionCorregida.y = hit.point.y + offsetSuelo;
            rb.MovePosition(posicionCorregida);

           
        }
        Debug.DrawRay(origenRayo, Vector3.down * distanciaRayo, Color.green);
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
}