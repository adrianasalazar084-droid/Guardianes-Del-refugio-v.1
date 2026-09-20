using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

[RequireComponent(typeof(UIDocument))]
public class DamageVignette : MonoBehaviour
{
    public static DamageVignette Instancia { get; private set; }

    [Header("Configuración")]
    [SerializeField] private Color colorVinieta = new Color(1f, 0f, 0f, 0.35f);
    [SerializeField] private float duracionFadeIn = 0.05f;
    [SerializeField] private float duracionFadeOut = 0.4f;

    private VisualElement overlay;
    private Coroutine vinietaCoroutine;


    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        Instancia = this;

        var document = GetComponent<UIDocument>();
        var root = document.rootVisualElement;

        // Creamos el overlay por código, para no depender de un archivo UXML externo.
        overlay = new VisualElement();
        overlay.style.position = Position.Absolute;
        overlay.style.left = 0;
        overlay.style.right = 0;
        overlay.style.top = 0;
        overlay.style.bottom = 0;
        overlay.style.backgroundColor = new StyleColor(new Color(colorVinieta.r, colorVinieta.g, colorVinieta.b, 0f));
        overlay.pickingMode = PickingMode.Ignore;

        root.Add(overlay);
    }


    public void Mostrar()
    {
        if (vinietaCoroutine != null)
            StopCoroutine(vinietaCoroutine);

        vinietaCoroutine = StartCoroutine(VinietaCoroutine());
    }


    private IEnumerator VinietaCoroutine()
    {
        yield return Fade(0f, colorVinieta.a, duracionFadeIn);
        yield return Fade(colorVinieta.a, 0f, duracionFadeOut);

        vinietaCoroutine = null;
    }


    private IEnumerator Fade(float desde, float hasta, float duracion)
    {
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracion)
        {
            float t = tiempoTranscurrido / duracion;
            float alphaActual = Mathf.Lerp(desde, hasta, t);

            overlay.style.backgroundColor = new StyleColor(new Color(colorVinieta.r, colorVinieta.g, colorVinieta.b, alphaActual));

            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }

        overlay.style.backgroundColor = new StyleColor(new Color(colorVinieta.r, colorVinieta.g, colorVinieta.b, hasta));
    }
}