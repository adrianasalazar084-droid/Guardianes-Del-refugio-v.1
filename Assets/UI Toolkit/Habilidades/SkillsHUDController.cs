using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class SkillsHUDController : MonoBehaviour
{
    [Header("Cooldown de cada habilidad (segundos)")]
    [SerializeField] private float skill1CooldownDuration = 5f;
    [SerializeField] private float skill2CooldownDuration = 8f;
    [Tooltip("La habilidad 3 empieza a contar su cooldown cuando termina el boost, no al activarla.")]
    [SerializeField] private float skill3CooldownDuration = 25f;

    [Header("Script de la habilidad 1")]
    [SerializeField] private Skill1Action skill1Action;

    [Header("Script de la habilidad 2")]
    [SerializeField] private Skill2Action skill2Action;

    [Header("Script de la habilidad 3")]
    [SerializeField] private Skill3Action skill3Action;

    [Header("Colores de las barras")]
    [Tooltip("Si está desmarcado, las barras de cooldown usan el color definido en el USS.")]
    [SerializeField] private bool usarColoresDelInspector = true;
    [SerializeField] private Color colorCooldownSkill1 = new Color(0.37f, 0.2f, 0.52f, 0.57f);
    [SerializeField] private Color colorCooldownSkill2 = new Color(0.37f, 0.2f, 0.52f, 0.57f);
    [SerializeField] private Color colorCooldownSkill3 = new Color(0.37f, 0.2f, 0.52f, 0.57f);
    [Tooltip("Color de la barra de la habilidad 3 mientras dura el boost.")]
    [SerializeField] private Color colorBoost = new Color(1f, 0.75f, 0.15f, 0.6f);

    private UIDocument document;

    private Button skill1;
    private Button skill2;
    private Button skill3;

    private VisualElement skill1Overlay;
    private VisualElement skill2Overlay;
    private VisualElement skill3Overlay;

    private float skill1TimeRemaining;
    private float skill2TimeRemaining;
    private float skill3TimeRemaining;

    private bool skill3EstabaActiva;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        var root = document.rootVisualElement;

        skill1 = root.Q<Button>("skill-1");
        skill2 = root.Q<Button>("skill-2");
        skill3 = root.Q<Button>("skill-3");

        skill1Overlay = root.Q<VisualElement>("skill-1-cooldown");
        skill2Overlay = root.Q<VisualElement>("skill-2-cooldown");
        skill3Overlay = root.Q<VisualElement>("skill-3-cooldown");

        SetOverlayHeight(skill1Overlay, 0f);
        SetOverlayHeight(skill2Overlay, 0f);
        SetOverlayHeight(skill3Overlay, 0f);

        if (skill1Action == null)
            Debug.LogWarning("SkillsHUDController: falta asignar Skill1Action en el Inspector.");

        if (skill2Action == null)
            Debug.LogWarning("SkillsHUDController: falta asignar Skill2Action en el Inspector.");

        if (skill3Action == null)
            Debug.LogWarning("SkillsHUDController: falta asignar Skill3Action en el Inspector.");
    }

    private void Update()
    {
        HandleInput();
        TickCooldowns();
        UpdateSkill3Boost();
    }

    private void HandleInput()
    {
        // Sin pausa ni Game Over no se pueden usar habilidades
        if (PauseMenuController.EstaPausado) return;
        if (GameOverController.JuegoTerminado) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TryUseSkill(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TryUseSkill(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TryUseSkill(3);
        }
    }

    private void TryUseSkill(int index)
    {
        switch (index)
        {
            case 1:
                if (skill1TimeRemaining <= 0f)
                {
                    UseSkill(1);
                    skill1TimeRemaining = skill1CooldownDuration;
                }
                break;

            case 2:
                if (skill2TimeRemaining <= 0f)
                {
                    UseSkill(2);
                    skill2TimeRemaining = skill2CooldownDuration;
                }
                break;

            case 3:
                // El cooldown de la skill 3 NO se asigna acá: empieza cuando termina el boost (ver UpdateSkill3Boost).
                if (skill3TimeRemaining <= 0f && skill3Action != null && !skill3Action.EstaActivo)
                {
                    UseSkill(3);
                }
                break;
        }
    }

    private void TickCooldowns()
    {
        if (skill1TimeRemaining > 0f)
        {
            skill1TimeRemaining -= Time.deltaTime;
            float normalized = Mathf.Clamp01(skill1TimeRemaining / skill1CooldownDuration);
            ApplyCooldownColor(skill1Overlay, colorCooldownSkill1);
            SetOverlayHeight(skill1Overlay, normalized * 100f);
        }

        if (skill2TimeRemaining > 0f)
        {
            skill2TimeRemaining -= Time.deltaTime;
            float normalized = Mathf.Clamp01(skill2TimeRemaining / skill2CooldownDuration);
            ApplyCooldownColor(skill2Overlay, colorCooldownSkill2);
            SetOverlayHeight(skill2Overlay, normalized * 100f);
        }

        if (skill3TimeRemaining > 0f)
        {
            skill3TimeRemaining -= Time.deltaTime;
            float normalized = Mathf.Clamp01(skill3TimeRemaining / skill3CooldownDuration);
            ApplyCooldownColor(skill3Overlay, colorCooldownSkill3);
            SetOverlayHeight(skill3Overlay, normalized * 100f);
        }
    }

    // Mientras dura el boost, la barra de la skill 3 se vacía en color dorado.
    // Cuando termina, empieza el cooldown con la barra normal.
    private void UpdateSkill3Boost()
    {
        bool activa = skill3Action != null && skill3Action.EstaActivo;

        if (activa)
        {
            skill3EstabaActiva = true;
            skill3Overlay.style.backgroundColor = colorBoost;
            SetOverlayHeight(skill3Overlay, skill3Action.TiempoRestanteNormalizado * 100f);
        }
        else if (skill3EstabaActiva)
        {
            skill3EstabaActiva = false;

            // Vuelve al color de cooldown y arranca el cooldown
            ApplyCooldownColor(skill3Overlay, colorCooldownSkill3);
            skill3TimeRemaining = skill3CooldownDuration;
            SetOverlayHeight(skill3Overlay, 100f);
        }
    }

    // Pinta la barra con el color del Inspector, o vuelve al color del USS si no se usan colores propios.
    private void ApplyCooldownColor(VisualElement overlay, Color color)
    {
        if (usarColoresDelInspector)
            overlay.style.backgroundColor = color;
        else
            overlay.style.backgroundColor = StyleKeyword.Null;
    }

    private void SetOverlayHeight(VisualElement overlay, float heightPercent)
    {
        overlay.style.height = Length.Percent(heightPercent);
    }

    private void UseSkill(int index)
    {
        Debug.Log($"Habilidad {index} usada");

        if (index == 1)
        {
            skill1Action?.Execute();
        }
        else if (index == 2)
        {
            skill2Action?.Execute();
        }
        else if (index == 3)
        {
            skill3Action.Execute();
        }
    }
}