using UnityEngine;
using UnityEngine.AI;

public class PlayerStamina : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private StaminaHUD hud;
    [SerializeField] private NavMeshAgent agent;

    [Header("Stamina")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float drainPerSecond = 20f;
    [SerializeField] private float regenPerSecond = 15f;
    [SerializeField] private float regenDelay = 1f;
    [SerializeField] private float minToRunAgain = 20f;

    [Header("Velocidades")]
    [SerializeField] private float walkSpeed = 3.5f;
    [SerializeField] private float runSpeed = 6f;

    private float current;
    private float lastUseTime;
    private bool exhausted;

    void Awake()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        current = maxStamina;
        hud.SetStamina(current, maxStamina);
    }

    void Update()
    {
        bool wantsToRun = Input.GetKey(KeyCode.LeftShift);
        bool running = wantsToRun && current > 0f && !exhausted;

        if (running)
        {
            current -= drainPerSecond * Time.deltaTime;
            lastUseTime = Time.time;
        }
        else if (Time.time - lastUseTime > regenDelay)
        {
            current += regenPerSecond * Time.deltaTime;
        }

        current = Mathf.Clamp(current, 0f, maxStamina);

        if (current <= 0f) exhausted = true;
        else if (current >= minToRunAgain) exhausted = false;

        if (agent != null)
            agent.speed = running ? runSpeed : walkSpeed;

        hud.SetStamina(current, maxStamina);
    }

    public bool CanSprint() => !exhausted && current > 0f;
}