using UnityEngine;
using UnityEngine.UIElements;

public class MonedasUI : MonoBehaviour
{
    public Inventario inventario;

    private Label label;
    private int ultimoValorMostrado = -1;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        label = root.Q<Label>("coins-label");
    }

    void Update()
    {
        if (inventario.monedas != ultimoValorMostrado)
        {
            ultimoValorMostrado = inventario.monedas;
            label.text = "x " + ultimoValorMostrado;
        }
    }
}