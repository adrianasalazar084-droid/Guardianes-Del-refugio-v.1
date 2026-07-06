using UnityEngine;
using TMPro;

public class Inventario : MonoBehaviour
{
    public int llaves = 0;

    public TMP_Text textoLlaves;

    void Start()
    {
        ActualizarUI();
    }

    public void AgregarLlave()
    {
        llaves++;
        ActualizarUI();
    }

    void ActualizarUI()
    {
        textoLlaves.text = "x " + llaves;
    }
}