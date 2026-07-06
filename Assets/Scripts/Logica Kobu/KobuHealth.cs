using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class KobuHealth : MonoBehaviour
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

            Debug.Log("Kobu ha muerto");
        }

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            RecibirDaño(10);
        }
    }
}
