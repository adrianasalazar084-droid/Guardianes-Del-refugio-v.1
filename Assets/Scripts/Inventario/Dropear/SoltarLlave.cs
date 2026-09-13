using UnityEngine;

public class SoltarLlave : MonoBehaviour
{
    public GameObject llavePrefab;

    // Punto desde donde cae la llave. Si no se asigna, usa la posición de este objeto.
    public Transform puntoDeSpawn;

    public void Soltar()
    {
        Vector3 posicion = (puntoDeSpawn != null) ? puntoDeSpawn.position : transform.position;

        Instantiate(llavePrefab, posicion, Quaternion.identity);
    }
}