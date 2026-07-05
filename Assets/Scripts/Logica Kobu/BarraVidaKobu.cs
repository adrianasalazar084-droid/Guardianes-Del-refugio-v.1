using UnityEngine;
using UnityEngine.UI;

public class BarraVidaKobu : MonoBehaviour
{
    public Image barraVida;
    [SerializeField] private KobuHealth kobuHealth;

    void Start()
    {

    }


    void Update()
    {
        barraVida.fillAmount = kobuHealth.VidaActual / (float)kobuHealth.VidaTotal;
    }
}
