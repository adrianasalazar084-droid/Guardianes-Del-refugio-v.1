using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Vida")]

    // Vida actual del enemigo.
    [SerializeField] private int vidaActual = 0;

    // Vida máxima del enemigo.
    [SerializeField] private int vidaTotal = 100;

    // Permite consultar la vida actual desde otros scripts.
    public int VidaActual
    {
        get
        {
            return vidaActual;
        }
    }

    // Permite consultar la vida máxima desde otros scripts.
    public int VidaTotal
    {
        get
        {
            return vidaTotal;
        }
    }

    [Header("Referencias")]

    // Animator encargado de reproducir las animaciones del enemigo.
    [SerializeField] private Animator anim;

    // Indica si el enemigo ya está muerto.
    private bool estaMuerto = false;

    // Permite consultar desde otros scripts (EnemyMovement, EnemyAttack, EnemyDetection)
    // si el enemigo ya comenzó a morir.
    public bool EstaMuerto
    {
        get
        {
            return estaMuerto;
        }
    }


    void Start()
    {
        // Inicializamos la vida.
        vidaActual = vidaTotal;

        // Si no asignamos el Animator desde el Inspector,
        // lo buscamos automáticamente en este mismo objeto.
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }


    /// <summary>
    /// Recibe daño y comprueba si el enemigo debe morir.
    /// </summary>
    public void RecibirDaño(int daño)
    {
        // Si ya está muerto, ignoramos cualquier daño adicional.
        if (estaMuerto)
            return;

        // Restamos el daño recibido.
        vidaActual = vidaActual - daño;

        // Comprobamos si la vida llegó a 0.
        if (vidaActual <= 0)
        {
            vidaActual = 0;

            Morir();
        }
    }


    /// <summary>
    /// Inicia la muerte del enemigo.
    /// </summary>
    private void Morir()
    {
        // Marcamos al enemigo como muerto.
        estaMuerto = true;

        Debug.Log("Enemigo ha muerto");

        // Soltamos la llave si el enemigo tiene el script.
        SoltarLlave soltarLlave = GetComponent<SoltarLlave>();

        if (soltarLlave != null)
        {
            soltarLlave.Soltar();
        }

        // Reproducimos la animación de muerte.
        anim.Play("Death");
    }


    public void DestruirEnemigo()
    {
        Destroy(gameObject);
    }
}