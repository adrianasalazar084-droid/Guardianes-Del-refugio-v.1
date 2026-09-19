using UnityEngine;
using System.Collections;
using Benjathemaker;

[RequireComponent(typeof(Rigidbody))]
public class Llave : MonoBehaviour
{
    [Header("Salto inicial")]
    public float fuerzaVertical = 4f;
    public float fuerzaHorizontal = 2f;
    public float fuerzaGiro = 5f;

    [Header("Asentamiento")]
    public float umbralVelocidad = 0.05f;
    public float tiempoQuieta = 0.3f;

    [Header("Referencias")]
    public SimpleGemsAnim gemsAnim;

    [Header("Sonido")]
    public AudioClip sonidoAparecer;
    [Range(0f, 1f)] public float volumenAparecer = 0.7f;

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

        // Sonido al aparecer.
        if (sonidoAparecer != null)
        {
            AudioSource.PlayClipAtPoint(sonidoAparecer, transform.position, volumenAparecer);
        }

        StartCoroutine(EsperarAsentamiento());
    }

    private IEnumerator EsperarAsentamiento()
    {
        float tiempoQuietaAcumulado = 0f;

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