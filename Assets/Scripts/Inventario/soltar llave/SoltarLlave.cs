using UnityEngine;

public class SoltarLlave : MonoBehaviour
{
    public GameObject llavePrefab;

    public void Soltar()
    {
        Instantiate(llavePrefab, transform.position, Quaternion.identity);
    }
}