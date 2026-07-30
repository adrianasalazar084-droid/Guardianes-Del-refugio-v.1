using JetBrains.Annotations;
using Unity.VisualScripting;
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


    void Start()
    {
        vidaActual = vidaTotal;



        // las buscamos automáticamente.
        if (anim == null)
            anim = GetComponent<Animator>();

        if (logicaKobu == null)
            logicaKobu = GetComponent<LogicaKobu>();

        if (kobuAttack == null)
            kobuAttack = GetComponent<KobuAttack>();

    }
    public void RecibirDaño(int daño)
    {
        vidaActual = vidaActual - daño;
        if (vidaActual <= 0)
        {
            vidaActual = 0;

            Morir();
        }

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

