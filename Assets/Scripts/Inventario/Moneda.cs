using UnityEngine;
using System.Collections;
using Benjathemaker;

[RequireComponent(typeof(Rigidbody))]
public class Moneda : MonoBehaviour
{
    [Header("Salto inicial")]
    public float fuerzaVertical = 4f;
    public float fuerzaHorizontal = 2f;
    public float fuerzaGiro = 5f;

    [Header("Asentamiento")]
    // Velocidad por debajo de la cual consideramos que ya está quieta.
    public float umbralVelocidad = 0.05f;
    // Tiempo que debe mantenerse quieta antes de congelarla (evita falsos positivos en el aire).
    public float tiempoQuieta = 0.3f;

    [Header("Referencias")]
    public SimpleGemsAnim gemsAnim;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (gemsAnim == null)
            gemsAnim = GetComponent<SimpleGemsAnim>();

        if (gemsAnim != null)
            gemsAnim.enabled = false;
    }

    private void Start()
    {
        Vector3 direccionAleatoria = new Vector3(
            Random.Range(-1f, 1f),
            0f,
            Random.Range(-1f, 1f)
        ).normalized;

        Vector3 impulso = Vector3.up * fuerzaVertical + direccionAleatoria * fuerzaHorizontal;

        rb.AddForce(impulso, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * fuerzaGiro, ForceMode.Impulse);

        StartCoroutine(EsperarAsentamiento());
    }

    private IEnumerator EsperarAsentamiento()
    {
        float tiempoQuietaAcumulado = 0f;

        // Esperamos un instante antes de empezar a chequear,
        // para que el impulso inicial no se detecte como "ya quieta".
        yield return new WaitForSeconds(0.2f);

        while (tiempoQuietaAcumulado < tiempoQuieta)
        {
            if (rb.linearVelocity.magnitude < umbralVelocidad)
            {
                tiempoQuietaAcumulado += Time.deltaTime;
            }
            else
            {
                tiempoQuietaAcumulado = 0f;
            }

            yield return null;
        }

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        if (gemsAnim != null)
            gemsAnim.enabled = true;
    }
}