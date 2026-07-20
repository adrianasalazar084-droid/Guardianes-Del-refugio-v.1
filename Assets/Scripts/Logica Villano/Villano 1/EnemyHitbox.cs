using UnityEngine;
using System.Collections.Generic;

public class EnemyHitbox : MonoBehaviour
{
    [Header("Configuración")]

    // Cantidad de daño que hará este ataque.
    [SerializeField] private int daño = 10;


    // Lista de objetos golpeados durante ESTE ataque.
    // Evita hacer daño varias veces al mismo jugador.
    private List<GameObject> objetivosGolpeados = new List<GameObject>();


    private void OnTriggerEnter(Collider other)
    {
        // Verificamos que el objeto tenga el tag "Player".
        if (!other.CompareTag("Player"))
            return;

        // Si ya fue golpeado durante este ataque,
        // no volvemos a hacer daño.
        if (objetivosGolpeados.Contains(other.gameObject))
            return;

        // Lo agregamos a la lista para no repetir el daño.
        objetivosGolpeados.Add(other.gameObject);

        // Obtenemos el componente que administra la vida de Kobu.
        KobuHealth kobuHealth = other.GetComponent<KobuHealth>();

        // Si el jugador tiene el script de vida, aplicamos el daño.
        if (kobuHealth != null)
        {
            kobuHealth.RecibirDaño(daño);

          
        }
    }


    /// <summary>
    /// Limpia la lista de objetivos golpeados.
    /// Debe llamarse al comenzar cada nuevo ataque.
    /// </summary>
    public void ReiniciarGolpe()
    {
        objetivosGolpeados.Clear();
    }
}