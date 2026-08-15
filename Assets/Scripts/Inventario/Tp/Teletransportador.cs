using UnityEngine;

public class Teletransportador : MonoBehaviour
{
    [Header("Configuración")]

    // Cantidad de llaves necesarias para utilizar el teletransportador.
    [SerializeField] private int llavesNecesarias = 1;

    // Punto exacto donde aparecerá Kobu.
    [SerializeField] private Transform puntoDestino;


    private void OnTriggerEnter(Collider other)
    {
        // Buscamos el Inventario en el objeto que entró al Trigger.
        Inventario inventario = other.GetComponent<Inventario>();

        // Si el objeto no tiene Inventario, no hacemos nada.
        if (inventario == null)
            return;

        // Comprobamos si Kobu tiene suficientes llaves.
        if (inventario.llaves >= llavesNecesarias)
        {
            Debug.Log("Kobu tiene la llave. Teletransportando...");

            // Movemos a Kobu al punto de destino.
            other.transform.position = puntoDestino.position;

            // Hacemos que Kobu mire en la misma dirección que el destino.
            other.transform.rotation = puntoDestino.rotation;
        }
        else
        {
            Debug.Log("Kobu no tiene la llave necesaria.");
        }
    }
}