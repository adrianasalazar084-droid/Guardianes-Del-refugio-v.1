using UnityEngine;

public class Skill1Action : MonoBehaviour, ISkillAction
{
    [Header("Animación")]
    [SerializeField] private Animator animator;
    [SerializeField] private string animatorTriggerName = "Skill1";

    [Header("Bloqueo de movimiento mientras dura la animación")]
    [SerializeField] private KobuAttack kobuAttack;

    [Header("Partícula propia de la habilidad 1")]
    [SerializeField] private GameObject particulaSkill1;

    [Header("Hitbox compartida con el golpe normal")]
    [SerializeField] private GolpeHitbox golpeHitbox;

    public void Execute()
    {
        Debug.Log("Habilidad 1 ejecutada");
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        if (animator == null)
        {
            Debug.LogWarning("Skill1Action: falta asignar el Animator en el Inspector.");
            return;
        }

        animator.SetTrigger(animatorTriggerName);

        if (kobuAttack != null)
        {
            kobuAttack.estoyAtacando = true;
        }
        else
        {
            Debug.LogWarning("Skill1Action: falta asignar KobuAttack en el Inspector (no se bloqueará el movimiento).");
        }
    }

    // Animation Event: llamar justo ANTES/al inicio del frame donde se activa el hitbox de la skill.
    public void ActivarParticulaEspecialSkill1()
    {
        if (golpeHitbox != null && particulaSkill1 != null)
        {
            golpeHitbox.SetParticulaOverride(particulaSkill1);
        }
        else
        {
            Debug.LogWarning("Skill1Action: falta asignar GolpeHitbox o Particula Skill1 en el Inspector.");
        }
    }

    // Animation Event: llamar justo DESPUÉS del frame donde se desactiva el hitbox de la skill.
    public void DesactivarParticulaEspecialSkill1()
    {
        if (golpeHitbox != null)
        {
            golpeHitbox.LimpiarParticulaOverride();
        }
    }
}