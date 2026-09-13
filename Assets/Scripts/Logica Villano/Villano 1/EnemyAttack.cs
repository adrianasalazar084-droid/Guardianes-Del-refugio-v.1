using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Referencias")]

    [SerializeField] private Animator anim;
    [SerializeField] private EnemyHealth enemyHealth;


    [Header("Ataque")]

    [SerializeField] private float tiempoEntreAtaques = 1.5f;
    [SerializeField] private EnemyHitbox hitbox;


    private float siguienteAtaque;

    // Indica si el enemigo está en medio de la animación de ataque.
    private bool estaAtacando = false;

    // Permite consultar desde EnemyMovement / EnemyDetection si el enemigo está atacando.
    public bool EstaAtacando
    {
        get
        {
            return estaAtacando;
        }
    }


    private void Awake()
    {
        if (anim == null)
            anim = GetComponent<Animator>();

        if (hitbox == null)
            hitbox = GetComponentInChildren<EnemyHitbox>();

        if (enemyHealth == null)
            enemyHealth = GetComponent<EnemyHealth>();
    }


    public void Atacar()
    {
        if (enemyHealth != null && enemyHealth.EstaMuerto)
            return;

        // Si ya está atacando, no reiniciamos el ataque.
        if (estaAtacando)
            return;

        if (Time.time < siguienteAtaque)
            return;

        siguienteAtaque = Time.time + tiempoEntreAtaques;

        // Marcamos que empezó a atacar (esto bloqueará el movimiento).
        estaAtacando = true;

        hitbox.ReiniciarGolpe();

        anim.SetTrigger("Attack");
    }


    /// <summary>
    /// Debe llamarse desde un Animation Event al final del clip "Attack".
    /// </summary>
    public void FinAtaque()
    {
        estaAtacando = false;
        anim.ResetTrigger("Attack");
    }

}