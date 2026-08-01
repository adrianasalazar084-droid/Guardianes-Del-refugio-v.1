using UnityEngine;
using UnityEngine.UIElements;

public class BarraVidaUI : MonoBehaviour
{
    public KobuHealth vida;

    private VisualElement fill;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        fill = root.Q<VisualElement>("health-fill");
    }

    void Update()
    {
        float porcentaje = vida.VidaActual / (float)vida.VidaTotal;

        fill.style.width = Length.Percent(porcentaje * 100f);
    }
}