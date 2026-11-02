using UnityEngine;

/// <summary>
/// Guarda los multiplicadores de daño de Kobu. GolpeHitbox lo consulta en cada golpe.
/// - Multiplicador permanente: para armas y mejoras (más adelante).
/// - Boost temporal: lo activa la habilidad 3.
/// </summary>
public class KobuDamage : MonoBehaviour
{
    [Header("Multiplicadores")]
    [Tooltip("Mejoras permanentes (armas, tienda...). 1 = sin cambios.")]
    [SerializeField] private float multiplicadorPermanente = 1f;

    private float multiplicadorBoost = 1f;

    public float MultiplicadorTotal => multiplicadorPermanente * multiplicadorBoost;
    public bool BoostActivo => multiplicadorBoost != 1f;

    /// <summary>Devuelve el daño final de un golpe a partir de su daño base.</summary>
    public int Calcular(int danoBase)
    {
        if (danoBase <= 0) return danoBase;
        return Mathf.Max(1, Mathf.RoundToInt(danoBase * MultiplicadorTotal));
    }

    public void ActivarBoost(float multiplicador)
    {
        multiplicadorBoost = Mathf.Max(0f, multiplicador);
    }

    public void QuitarBoost()
    {
        multiplicadorBoost = 1f;
    }

    public void SetMultiplicadorPermanente(float multiplicador)
    {
        multiplicadorPermanente = Mathf.Max(0f, multiplicador);
    }
}