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

    // Referencia al Animator para reproducir la animación de muerte.
    [SerializeField] private Animator anim;

    // Script encargado del movimiento.
    [SerializeField] private LogicaKobu logicaKobu;

    // Script encargado del ataque.
    [SerializeField] private KobuAttack kobuAttack;

    // Script encargado de colocar nuevamente al jugador en el Spawn.
    [SerializeField] private PlayerRespawn playerRespawn;


    void Start()
    {
        vidaActual = vidaTotal;

        // Buscamos automáticamente los componentes si no fueron asignados
        // desde el Inspector.
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


    private void Morir()
    {
        Debug.Log("Kobu ha muerto");

        // Reproducimos la animación de muerte.
        anim.SetTrigger("Death");

        // Desactivamos el movimiento.
        logicaKobu.enabled = false;

        // Desactivamos el ataque.
        kobuAttack.enabled = false;
    }
}