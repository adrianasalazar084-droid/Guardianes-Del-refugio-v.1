using System.Collections;
using UnityEngine;

/// <summary>
/// Habilidad 3: boost de daño temporal.
/// El cooldown NO empieza al activarla: empieza cuando el boost termina (lo maneja SkillsHUDController).
/// </summary>
public class Skill3Action : MonoBehaviour, ISkillAction
{
    [Header("Boost de daño")]
    [Tooltip("1.5 = 50% más de daño en todos los golpes.")]
    [SerializeField] private float multiplicadorDano = 1.5f;

    [Tooltip("Segundos que dura el boost. El cooldown empieza cuando termina.")]
    [SerializeField] private float duracionBoost = 8f;

    [Header("Referencias (se buscan solas si están vacías)")]
    [SerializeField] private KobuDamage kobuDamage;
    [SerializeField] private KobuHealth kobuHealth;
    [SerializeField] private KobuAttack kobuAttack;

    [Header("Animación (opcional)")]
    [SerializeField] private Animator animator;
    [Tooltip("Dejalo vacío si no tenés animación: el boost se activa igual.")]
    [SerializeField] private string animatorTriggerName = "Skill3";
    [Tooltip("Segundos que Kobu no puede moverse ni atacar mientras hace la animación.")]
    [SerializeField] private float tiempoBloqueoMovimiento = 1f;

    [Header("Efecto mientras dura el boost (opcional)")]
    [Tooltip("Partícula en loop que acompaña a Kobu mientras dura el boost.")]
    [SerializeField] private GameObject particulaBoost;
    [SerializeField] private Transform puntoParticula;

    /// <summary>True mientras el boost está activo.</summary>
    public bool EstaActivo { get; private set; }

    /// <summary>1 al activarse y baja hasta 0 cuando termina. Lo usa el HUD.</summary>
    public float TiempoRestanteNormalizado =>
        duracionBoost > 0f ? Mathf.Clamp01(tiempoRestante / duracionBoost) : 0f;

    private float tiempoRestante;
    private GameObject particulaInstanciada;

    // ---------- Inicio ----------

    void Awake()
    {
        if (kobuDamage == null) kobuDamage = Buscar<KobuDamage>();
        if (kobuHealth == null) kobuHealth = Buscar<KobuHealth>();
        if (kobuAttack == null) kobuAttack = Buscar<KobuAttack>();

        if (animator == null && kobuAttack != null)
            animator = kobuAttack.GetComponent<Animator>();
    }

    void Start()
    {
        if (kobuDamage == null)
            Debug.LogWarning("Skill3Action: no encuentro KobuDamage. Agregalo al GameObject de Kobu.");

        if (!string.IsNullOrEmpty(animatorTriggerName) && !TieneTrigger(animatorTriggerName))
            Debug.LogWarning($"Skill3Action: el Animator no tiene un trigger '{animatorTriggerName}'. El boost se activará sin animación.");
    }

    // Busca el componente en este objeto, en sus padres y, por último, en toda la escena
    T Buscar<T>() where T : UnityEngine.Object
    {
        T encontrado = GetComponent<T>();

        if (encontrado == null) encontrado = GetComponentInParent<T>();
        if (encontrado == null) encontrado = FindFirstObjectByType<T>();

        return encontrado;
    }

    // ---------- Activar ----------

    public void Execute()
    {
        if (EstaActivo) return;

        if (kobuDamage == null)
        {
            Debug.LogWarning("Skill3Action: falta KobuDamage, no se puede activar el boost.");
            return;
        }

        kobuDamage.ActivarBoost(multiplicadorDano);
        tiempoRestante = duracionBoost;
        EstaActivo = true;

        CrearParticula();
        ReproducirAnimacion();
    }

    void ReproducirAnimacion()
    {
        if (!TieneTrigger(animatorTriggerName)) return;

        animator.SetTrigger(animatorTriggerName);

        if (kobuAttack != null && tiempoBloqueoMovimiento > 0f)
            StartCoroutine(BloquearMovimiento());
    }

    IEnumerator BloquearMovimiento()
    {
        kobuAttack.estoyAtacando = true;
        yield return new WaitForSeconds(tiempoBloqueoMovimiento);
        kobuAttack.estoyAtacando = false;
    }

    bool TieneTrigger(string nombre)
    {
        if (animator == null || string.IsNullOrEmpty(nombre)) return false;

        foreach (AnimatorControllerParameter parametro in animator.parameters)
        {
            if (parametro.name == nombre && parametro.type == AnimatorControllerParameterType.Trigger)
                return true;
        }

        return false;
    }

    // ---------- Duración ----------

    void Update()
    {
        if (!EstaActivo) return;

        // Si Kobu muere, el boost se cancela
        if (kobuHealth != null && kobuHealth.EstaMuerto)
        {
            Terminar();
            return;
        }

        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante <= 0f)
            Terminar();
    }

    void Terminar()
    {
        EstaActivo = false;
        tiempoRestante = 0f;

        if (kobuDamage != null)
            kobuDamage.QuitarBoost();

        if (particulaInstanciada != null)
        {
            Destroy(particulaInstanciada);
            particulaInstanciada = null;
        }
    }

    void OnDisable()
    {
        // Seguridad: el multiplicador no debe quedar pegado si este script se desactiva
        if (EstaActivo) Terminar();
    }

    // ---------- Efecto visual ----------

    void CrearParticula()
    {
        if (particulaBoost == null) return;

        Transform padre = puntoParticula != null ? puntoParticula : kobuDamage.transform;
        particulaInstanciada = Instantiate(particulaBoost, padre.position, padre.rotation, padre);
    }
}