using UnityEngine;

// UNICA RESPONSABILIDAD: elegir puntos aleatorios dentro de un área y moverse hacia ellos.
public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrulla")]
    public float radioPatrulla = 6f;
    public float velocidadPatrulla = 2f;
    public float tiempoEspera = 2f; 
    public float distanciaLlegada = 0.3f; 

    private Vector3 centroPatrulla;
    private Vector3 puntoDestino;
    private float temporizadorEspera;
    private bool esperando = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        centroPatrulla = transform.position; 
        ElegirNuevoPunto();
    }

    void ElegirNuevoPunto()
    {
        Vector2 puntoAleatorio2D = Random.insideUnitCircle * radioPatrulla;
        puntoDestino = centroPatrulla + new Vector3(puntoAleatorio2D.x, 0f, puntoAleatorio2D.y);
    }

    public void MoverPatrulla()
    {
        if (esperando)
        {
            temporizadorEspera -= Time.fixedDeltaTime;
            if (temporizadorEspera <= 0f)
            {
                esperando = false;
                ElegirNuevoPunto();
            }
            return;
        }

        Vector3 direccion = puntoDestino - transform.position;
        direccion.y = 0f;
        float distancia = direccion.magnitude;

        if (distancia <= distanciaLlegada)
        {
            esperando = true;
            temporizadorEspera = tiempoEspera;
            return;
        }

        direccion.Normalize();
        Vector3 nuevaPosicion = rb.position + direccion * velocidadPatrulla * Time.fixedDeltaTime;
        rb.MovePosition(nuevaPosicion);

        if (direccion != Vector3.zero)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, rotacionObjetivo, 10f * Time.fixedDeltaTime));
        }
    }

    public bool EstaEsperando()
    {
        return esperando;
    }
}