using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int vidaActual = 0;

    public int VidaActual
    {
        get
        {
            return vidaActual;
        }
    }

    [SerializeField] private int vidaTotal = 100;

    public int VidaTotal
    {
        get
        {
            return vidaTotal;
        }
    }


    void Start()
    {
        vidaActual = vidaTotal;

    }
    public void RecibirDaño(int daño)
    {
        vidaActual = vidaActual - daño;
        if (vidaActual <= 0)
        {
            vidaActual = 0;
             
             SoltarLlave soltarLlave = GetComponent<SoltarLlave>();
        if (soltarLlave != null)
        {
            soltarLlave.Soltar();
        }

        Debug.Log("Enemigo ha muerto");

        Destroy(gameObject);
        }

    }

 
    void Update()
    {

    }
}
