using UnityEngine;

// UNICA RESPONSABILIDAD: detectar a Kobu durante el golpe y restarle vida.
public class EnemyHitbox : MonoBehaviour
{
    [SerializeField] private int daño = 15;

    private Collider hitboxCollider;

    void Start()
    {
        hitboxCollider = GetComponent<Collider>();
        hitboxCollider.enabled = false; 
    }

    private void OnTriggerEnter(Collider other)
    {
        KobuHealth kobuHealth = other.GetComponent<KobuHealth>();

        if (kobuHealth != null)
        {
            kobuHealth.RecibirDaño(daño);
        }
    }

    
    public void ActivarHitbox()
    {
        hitboxCollider.enabled = true;
    }

 
    public void DesactivarHitbox()
    {
        hitboxCollider.enabled = false;
    }
}