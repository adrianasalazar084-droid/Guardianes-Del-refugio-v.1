using UnityEngine;

public class Skill2Action : MonoBehaviour, ISkillAction
{
    [Header("Animación")]
    [SerializeField] private Animator animator;
    [SerializeField] private string animatorTriggerName = "Skill2";

    [Header("Bloqueo de movimiento mientras dura la animación")]
    [SerializeField] private KobuAttack kobuAttack;

    [Header("Curación")]
    [SerializeField] private KobuHealth kobuHealth;
    [SerializeField] private int cantidadCuracion = 30;

    [Header("Partícula propia de la habilidad 2")]
    [SerializeField] private GameObject particulaSkill2;
    [SerializeField] private Transform puntoSkill2;

    // Guarda la referencia a la última partícula instanciada, para poder destruirla después.
    private GameObject particulaInstanciada;

    public void Execute()
    {
        Debug.Log("Habilidad 2 ejecutada");
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        if (animator == null)
        {
            Debug.LogWarning("Skill2Action: falta asignar el Animator en el Inspector.");
            return;
        }

        animator.SetTrigger(animatorTriggerName);

        if (kobuAttack != null)
        {
            kobuAttack.estoyAtacando = true;
        }
        else
        {
            Debug.LogWarning("Skill2Action: falta asignar KobuAttack en el Inspector (no se bloqueará el movimiento).");
        }
    }

    public void AplicarCuracion()
    {
        if (kobuHealth != null)
        {
            kobuHealth.Curar(cantidadCuracion);
        }
        else
        {
            Debug.LogWarning("Skill2Action: falta asignar KobuHealth en el Inspector.");
        }
    }

    // Animation Event: instancia la partícula de la habilidad.
    public void InstanciarParticulaSkill2()
    {
        if (particulaSkill2 != null && puntoSkill2 != null)
        {
            particulaInstanciada = Instantiate(particulaSkill2, puntoSkill2.position, puntoSkill2.rotation);
        }
        else
        {
            Debug.LogWarning("Skill2Action: falta asignar Particula Skill2 o Punto Skill2 en el Inspector.");
        }
    }

    // Animation Event: destruye la partícula instanciada por esta habilidad.
    public void DestruirParticulaSkill2()
    {
        if (particulaInstanciada != null)
        {
            Destroy(particulaInstanciada);
        }
    }
}