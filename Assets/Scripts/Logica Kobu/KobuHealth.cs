using UnityEngine;

public class KobuHealth : MonoBehaviour
{
    [SerializeField] private int vidaActual = 0;

    public int VidaActual
    {
        get
        {
            return vidaActual;
        }
    }

    [SerializeField] private int vidaTotal = 100;

    public bool EstaMuerto
    {
        get
        {
            return vidaActual <= 0;
        }
    }

    public int VidaTotal
    {
        get
        {
            return vidaTotal;
        }
    }

    [Header("Referencias")]

    [SerializeField] private Animator anim;
    [SerializeField] private LogicaKobu logicaKobu;
    [SerializeField] private KobuAttack kobuAttack;
    [SerializeField] private PlayerRespawn playerRespawn;


    void Start()
    {
        vidaActual = vidaTotal;

        if (anim == null)
            anim = GetComponent<Animator>();

        if (logicaKobu == null)
            logicaKobu = GetComponent<LogicaKobu>();

        if (kobuAttack == null)
            kobuAttack = GetComponent<KobuAttack>();

        if (playerRespawn == null)
            playerRespawn = GetComponent<PlayerRespawn>();
    }


    public void RecibirDaño(int daño)
    {
        vidaActual -= daño;

        if (vidaActual <= 0)
        {
            vidaActual = 0;

            Morir();
        }
    }


    /// Restaura la vida del jugador al máximo.
    public void RestaurarVida()
    {
        vidaActual = vidaTotal;
    }


    /// Cura una cantidad fija de vida, sin pasarse del máximo.
    public void Curar(int cantidad)
    {
        vidaActual += cantidad;

        if (vidaActual > vidaTotal)
        {
            vidaActual = vidaTotal;
        }
    }


    private void Morir()
    {
        Debug.Log("Kobu ha muerto");

        anim.SetTrigger("Death");

        logicaKobu.enabled = false;
        kobuAttack.enabled = false;
    }
}