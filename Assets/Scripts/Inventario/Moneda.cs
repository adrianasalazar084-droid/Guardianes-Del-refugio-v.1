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
    public float tiempoParaAsentarse = 1.2f;

    [Header("Referencias")]
    public SimpleGemsAnim gemsAnim;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (gemsAnim == null)
            gemsAnim = GetComponent<SimpleGemsAnim>();

        // La rotaci�n/flotaci�n decorativa arranca reci�n cuando la moneda se asienta.
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

        StartCoroutine(Asentarse());
    }

    private IEnumerator Asentarse()
    {
        yield return new WaitForSeconds(tiempoParaAsentarse);

        // Frenamos y congelamos la f�sica para que quede fija en el piso.
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        // Activamos la animaci�n decorativa, igual que en las gemas/llave.
        if (gemsAnim != null)
            gemsAnim.enabled = true;
    }
}