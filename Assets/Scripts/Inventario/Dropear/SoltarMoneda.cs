using UnityEngine;

public class SoltarMonedas : MonoBehaviour
{
    public GameObject monedaPrefab;
    public int cantidadMinima = 2;
    public int cantidadMaxima = 5;

    // Punto desde donde caen las monedas. Si no se asigna, usa la posición de este objeto.
    public Transform puntoDeSpawn;

    public void Soltar()
    {
        Vector3 posicion = (puntoDeSpawn != null) ? puntoDeSpawn.position : transform.position;

        int cantidad = Random.Range(cantidadMinima, cantidadMaxima + 1);

        for (int i = 0; i < cantidad; i++)
        {
            Instantiate(monedaPrefab, posicion, Quaternion.identity);
        }
    }
}