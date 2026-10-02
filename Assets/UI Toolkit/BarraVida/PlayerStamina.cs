using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private StaminaHUD hud;
    [SerializeField] private Animator animator;

    [Header("Stamina")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float drainPerSecond = 20f;
    [SerializeField] private float regenPerSecond = 15f;
    [SerializeField] private float regenDelay = 1f;           // pausa normal tras gastar
    [SerializeField] private float exhaustedRegenDelay = 2f;  // pausa si se quedó en 0
    [SerializeField] private float minToActAgain = 20f;       // mínimo para volver a correr/dash

    [Header("Dash")]
    [SerializeField] private KeyCode dashKey = KeyCode.Alpha3;
    [SerializeField] private float dashCost = 25f;

    private LogicaKobu movimiento;
    private float current;
    private float lastUseTime;
    private bool exhausted;

    void Awake()
    {
        movimiento = GetComponent<LogicaKobu>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        current = maxStamina;
        hud.SetStamina(current, maxStamina);
    }

    void Update()
    {
        // Gasta stamina solo si Kobu está corriendo de verdad
        bool running = movimiento != null && movimiento.estoyCorriendo && !exhausted && current > 0f;

        if (running)
        {
            current -= drainPerSecond * Time.deltaTime;
            lastUseTime = Time.time;
        }

        // Dash
        if (Input.GetKeyDown(dashKey) && !exhausted && current >= dashCost)
        {
            current -= dashCost;
            lastUseTime = Time.time;
            if (animator != null) animator.SetTrigger("Dash");
        }

        current = Mathf.Clamp(current, 0f, maxStamina);

        // Al llegar a 0 queda agotado
        if (current <= 0f && !exhausted)
        {
            exhausted = true;
            lastUseTime = Time.time;
        }

        // Recarga tras la pausa
        float delay = exhausted ? exhaustedRegenDelay : regenDelay;
        if (!running && Time.time - lastUseTime > delay)
        {
            current += regenPerSecond * Time.deltaTime;
            current = Mathf.Clamp(current, 0f, maxStamina);
        }

        // Sale del agotamiento al llegar al mínimo
        if (exhausted && current >= minToActAgain)
            exhausted = false;

        hud.SetStamina(current, maxStamina);
    }

    public bool CanSprint() => !exhausted && current > 0f;
}