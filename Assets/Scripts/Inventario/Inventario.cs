using UnityEngine;
using TMPro;

public class Inventario : MonoBehaviour
{
    public int llaves = 0;
    public int monedas = 0;

    public TMP_Text textoLlaves;
    public TMP_Text textoMonedas;

    void Start()
    {
        ActualizarUI();
    }

    public void AgregarLlave()
    {
        llaves++;
        ActualizarUI();
    }

    public void UsarLlave()
    {
        if (llaves > 0)
        {
            llaves--;
            ActualizarUI();
        }
    }

    public void AgregarMoneda()
    {
        monedas++;
        ActualizarUI();
    }

    void ActualizarUI()
    {
        textoLlaves.text = "x " + llaves;

        if (textoMonedas != null)
            textoMonedas.text = "x " + monedas;
    }
}