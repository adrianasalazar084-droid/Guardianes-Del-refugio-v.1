using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [Header("Audio")]

    // AudioSource encargado de reproducir los sonidos.
    [SerializeField] private AudioSource audioSource;

    // Lista de sonidos de pasos.
    [SerializeField] private AudioClip[] sonidosPasos;

    // Guarda el último sonido reproducido para no repetirlo inmediatamente.
    private int ultimoIndice = -1;

    private void Awake()
    {
        // Si olvidamos asignar el AudioSource desde el Inspector,
        // lo buscamos automáticamente.
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Reproduce un sonido de paso aleatorio.
    /// Este método será llamado mediante un Animation Event.
    /// </summary>
    public void ReproducirPaso()
    {
        // Si no hay sonidos asignados, no hacemos nada.
        if (sonidosPasos.Length == 0)
            return;

        int indice;

        // Evitamos repetir el mismo sonido dos veces seguidas.
        do
        {
            indice = Random.Range(0, sonidosPasos.Length);
        }
        while (indice == ultimoIndice && sonidosPasos.Length > 1);

        ultimoIndice = indice;

        // Reproduce el sonido seleccionado.
        audioSource.PlayOneShot(sonidosPasos[indice]);
    }
}