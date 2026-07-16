using UnityEngine;

public class KobuAttack : MonoBehaviour
{
    private Animator anim;

    public bool estoyAtacando;

    public GameObject particulaGolpe; 
    public Transform puntoGolpe; 

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

   
    public void InstanciarParticulaGolpe()
    {
        if (particulaGolpe != null && puntoGolpe != null)
        {
            Instantiate(particulaGolpe, puntoGolpe.position, puntoGolpe.rotation);
        }
    }

    public void DejaDeGolpear()
    {
        estoyAtacando = false;
    }
}