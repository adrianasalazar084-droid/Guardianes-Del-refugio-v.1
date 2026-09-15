using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Vida")]

    [SerializeField] private int vidaActual = 0;
    [SerializeField] private int vidaTotal = 100;

    public int VidaActual { get { return vidaActual; } }
    public int VidaTotal { get { return vidaTotal; } }

    [Header("Referencias")]

    [SerializeField] private Animator anim;
    [SerializeField] private EnemyFlash enemyFlash;
    [SerializeField] private EnemyKnockback enemyKnockback;

    private bool estaMuerto = false;

    public bool EstaMuerto { get { return estaMuerto; } }


    void Start()
    {
        vidaActual = vidaTotal;

        if (anim == null)
            anim = GetComponent<Animator>();

        if (enemyFlash == null)
            enemyFlash = GetComponent<EnemyFlash>();

        if (enemyKnockback == null)
            enemyKnockback = GetComponent<EnemyKnockback>();
    }


    /// <summary>
    /// Recibe daño. origenGolpe es opcional: si se pasa, se usa para calcular
    /// la dirección del knockback (alejándolo del punto de impacto).
    /// </summary>
    public void RecibirDaño(int daño, Vector3? origenGolpe = null)
    {
        if (estaMuerto)
            return;

        vidaActual = vidaActual - daño;

        if (enemyFlash != null)
        {
            enemyFlash.Flash();
        }

        if (enemyKnockback != null && origenGolpe.HasValue)
        {
            enemyKnockback.AplicarKnockback(origenGolpe.Value);
        }

        if (vidaActual <= 0)
        {
            vidaActual = 0;

            Morir();
        }
    }


    private void Morir()
    {
        estaMuerto = true;

        Debug.Log("Enemigo ha muerto");

        SoltarLlave soltarLlave = GetComponent<SoltarLlave>();
        if (soltarLlave != null)
        {
            soltarLlave.Soltar();
        }

        SoltarMonedas soltarMonedas = GetComponent<SoltarMonedas>();
        if (soltarMonedas != null)
        {
            soltarMonedas.Soltar();
        }

        anim.Play("Death");
    }


    public void DestruirEnemigo()
    {
        Destroy(gameObject);
    }
}