using UnityEngine;

public class LogicaKobu : MonoBehaviour
{
    private float velocidad = 1.3f;
    private float velocidadRotacion = 80f;

    private Animator anim;
    public float x, y;


    public Rigidbody rb;
    public float fuerzaDeSalto = 8f;
    public bool puedoSaltar;

    public bool estoyAtacando;

    void Start()
    {
        puedoSaltar = false;
        anim = GetComponent<Animator>();


    }

    void FixedUpdate()
    {
        if (!estoyAtacando)
        {
            transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);
            transform.Translate(0, 0, y * Time.deltaTime * velocidad);
        }
    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Mouse0) && !estoyAtacando)
        {
            anim.SetTrigger("golpe");
            estoyAtacando = true;
        }
    

        if (!estoyAtacando)
        {
            anim.SetFloat("VelX", x);
            anim.SetFloat("VelY", y);
        }
        else
        {
            anim.SetFloat("VelX", 0);
            anim.SetFloat("VelY", 0);
        }

        if (puedoSaltar)

        {
                if (!estoyAtacando)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        anim.SetBool("Salto", true);
                        rb.AddForce(new Vector3(0, fuerzaDeSalto, 0), ForceMode.Impulse);

                    }
                }
         
            anim.SetBool("TocoSuelo", true);

        }
        else
        {
            EstoyCayendo();
        }

    }

    public void EstoyCayendo()
    {
        anim.SetBool("TocoSuelo", false);
        anim.SetBool("Salto", false);
    }

  
    public void DejaDeGolpear()
    {
        estoyAtacando = false;
    }
}
