using UnityEngine;
using UnityEngine.UIElements;

public class StaminaHUD : MonoBehaviour
{
    private VisualElement staminaFill;

    void OnEnable() { staminaFill = null; }

    public void SetStamina(float current, float max)
    {
        if (staminaFill == null)
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            staminaFill = root?.Q<VisualElement>("stamina-fill");

            if (staminaFill == null)
            {
                Debug.LogWarning("No encuentro 'stamina-fill' en el UXML de este UIDocument.");
                return;
            }
        }

        staminaFill.style.width = Length.Percent(current / max * 100f);
    }
}