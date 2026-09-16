using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class SkillsHUDController : MonoBehaviour
{
    [Header("Cooldown de cada habilidad (segundos)")]
    [SerializeField] private float skill1CooldownDuration = 5f;
    [SerializeField] private float skill2CooldownDuration = 8f;
    [SerializeField] private float skill3CooldownDuration = 12f;

    [Header("Script de la habilidad 1")]
    [SerializeField] private Skill1Action skill1Action;

    [Header("Script de la habilidad 2")]
    [SerializeField] private Skill2Action skill2Action;

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
    }

    private void Update()
    {
        HandleInput();
        TickCooldowns();
    }

    private void HandleInput()
    {
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
                if (skill3TimeRemaining <= 0f)
                {
                    UseSkill(3);
                    skill3TimeRemaining = skill3CooldownDuration;
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
            SetOverlayHeight(skill1Overlay, normalized * 100f);
        }

        if (skill2TimeRemaining > 0f)
        {
            skill2TimeRemaining -= Time.deltaTime;
            float normalized = Mathf.Clamp01(skill2TimeRemaining / skill2CooldownDuration);
            SetOverlayHeight(skill2Overlay, normalized * 100f);
        }

        if (skill3TimeRemaining > 0f)
        {
            skill3TimeRemaining -= Time.deltaTime;
            float normalized = Mathf.Clamp01(skill3TimeRemaining / skill3CooldownDuration);
            SetOverlayHeight(skill3Overlay, normalized * 100f);
        }
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

        // Skill 3 todavía sin script propio - se agrega más adelante de la misma forma.
    }
}