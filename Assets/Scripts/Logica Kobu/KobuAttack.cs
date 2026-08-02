using UnityEngine;

public class KobuAttack : MonoBehaviour
{
    private Animator anim;

    public bool estoyAtacando;

    // Alterna entre golpe izquierdo y derecho
    private bool siguienteGolpeDerecho = false;

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
            if (siguienteGolpeDerecho)
            {

                anim.SetTrigger("Rightpunch");
            }
            else
            {

                anim.SetTrigger("golpe");
            }

            siguienteGolpeDerecho = !siguienteGolpeDerecho;
            estoyAtacando = true;
        }
    }

    // Animation Event: Instancia la partícula del golpe
    public void InstanciarParticulaGolpe()
    {
        if (particulaGolpe != null && puntoGolpe != null)
        {
            Instantiate(particulaGolpe, puntoGolpe.position, puntoGolpe.rotation);
        }
    }

    // Animation Event: Se llama al final de la animación
    public void DejaDeGolpear()
    {

        estoyAtacando = false;
    }
}