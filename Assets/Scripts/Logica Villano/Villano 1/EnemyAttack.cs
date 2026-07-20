using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Referencias")]

    // Referencia al Animator para reproducir la animación.
    [SerializeField] private Animator anim;


    [Header("Ataque")]

    // Tiempo que debe esperar entre un ataque y otro.
    [SerializeField] private float tiempoEntreAtaques = 1.5f;
    [SerializeField] private EnemyHitbox hitbox;


    // Guarda el instante en que podrá volver a atacar.
    private float siguienteAtaque;


    private void Awake()
    {
        // Si olvidamos asignar el Animator desde el Inspector,
        // lo buscamos automáticamente.
        if (anim == null)
            anim = GetComponent<Animator>();

        if (hitbox == null)
            hitbox = GetComponentInChildren<EnemyHitbox>();
    }


    /// <summary>
    /// Intenta realizar un ataque.
    /// Si aún está en enfriamiento (cooldown), no hace nada.
    /// </summary>
    public void Atacar()
    {
        
        // ¿Todavía no puede atacar?
        if (Time.time < siguienteAtaque)
            return;

        // Guardamos el momento del próximo ataque permitido.
        siguienteAtaque = Time.time + tiempoEntreAtaques;

        hitbox.ReiniciarGolpe();

        // Activamos la animación.
        anim.SetTrigger("Attack");
    }
  
}