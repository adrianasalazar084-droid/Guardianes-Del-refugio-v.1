using UnityEngine;
using UnityEngine.UI;

public class BarraVidaEnemigo : MonoBehaviour
{
    public Image barraVida;
    [SerializeField] private EnemyHealth enemyHealth;

    void Start()
    {

    }


    void Update()
    {
        barraVida.fillAmount = enemyHealth.VidaActual / (float)enemyHealth.VidaTotal;


    }
}