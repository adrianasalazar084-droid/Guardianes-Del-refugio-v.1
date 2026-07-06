using UnityEngine;

public class KobuAttack : MonoBehaviour
{
    private Animator anim;

    public bool estoyAtacando;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !estoyAtacando)
        {
            anim.SetTrigger("golpe");
            estoyAtacando = true;
        }
    }

    public void DejaDeGolpear()
    {
        estoyAtacando = false;
    }
}