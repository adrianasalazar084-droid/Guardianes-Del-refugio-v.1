using UnityEngine;
using System.Collections;

public class EnemyFlash : MonoBehaviour
{
    [Header("Configuración del destello")]
    public Color colorDestello = Color.white;
    public float duracionDestello = 0.1f;

    public Renderer[] renderers;

    private MaterialPropertyBlock propBlock;
    private Coroutine flashCoroutine;

    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");


    private void Awake()
    {
        propBlock = new MaterialPropertyBlock();

        if (renderers == null || renderers.Length == 0)
            renderers = GetComponentsInChildren<Renderer>();
    }


    public void Flash()
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(FlashCoroutine());
    }


    private IEnumerator FlashCoroutine()
    {
        AplicarColor(colorDestello);

        yield return new WaitForSeconds(duracionDestello);

        QuitarColor();

        flashCoroutine = null;
    }


    private void AplicarColor(Color color)
    {
        foreach (Renderer r in renderers)
        {
            if (r == null)
                continue;

            r.GetPropertyBlock(propBlock);
            propBlock.SetColor(BaseColor, color);
            r.SetPropertyBlock(propBlock);
        }
    }


    private void QuitarColor()
    {
        foreach (Renderer r in renderers)
        {
            if (r == null)
                continue;

            r.SetPropertyBlock(null);
        }
    }
}