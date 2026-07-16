using UnityEngine;

public class LogicaKobu : MonoBehaviour
{
    public float velocidad = 1.3f;
    public float velocidadCorrer = 2.6f; 
    public float velocidadRotacion = 80f;

    private Animator anim;

    public float x, y;

    public Rigidbody rb;
    public float fuerzaDeSalto = 6f;
    public bool puedoSaltar;

    private KobuAttack ataque;

    public bool estoyCorriendo; 
    private bool estabaEnElAire; 

    void Start()
    {
        puedoSaltar = false;
        anim = GetComponent<Animator>();
        ataque = GetComponent<KobuAttack>();
    }

    void FixedUpdate()
    {
        if (!ataque.estoyAtacando)
        {
            transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);

           
            float velocidadActual = estoyCorriendo ? velocidadCorrer : velocidad;
            transform.Translate(0, 0, y * Time.deltaTime * velocidadActual);
        }
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (!ataque.estoyAtacando)
        {
            
            estoyCorriendo = Input.GetKey(KeyCode.LeftShift) && y > 0.1f;

            anim.SetFloat("VelX", x);
            anim.SetFloat("VelY", y);
            anim.SetBool("Correr", estoyCorriendo); 
        }
        else
        {
            anim.SetFloat("VelX", 0);
            anim.SetFloat("VelY", 0);

           
            estoyCorriendo = false;
            anim.SetBool("Correr", false);
        }

        if (puedoSaltar)
        {
            if (!ataque.estoyAtacando)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    anim.SetBool("Salto", true);
                    estabaEnElAire = true; // NUEVO
                    rb.AddForce(Vector3.up * fuerzaDeSalto, ForceMode.Impulse);
                }
            }

            anim.SetBool("TocoSuelo", true);

           
            if (estabaEnElAire)
            {
                anim.SetBool("Salto", false);
                estabaEnElAire = false;
            }
        }
        else
        {
            estabaEnElAire = true;
            EstoyCayendo();
        }
    }

    public void EstoyCayendo()
    {
        anim.SetBool("TocoSuelo", false);
       
    }
}