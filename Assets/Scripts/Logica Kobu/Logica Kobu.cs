using UnityEngine;

public class LogicaKobu : MonoBehaviour
{
    private float velocidad = 1.3f;
    private float velocidadRotacion = 80f;

    private Animator anim;
    public float x, y;



    void Start()
    {
        anim = GetComponent<Animator>();


    }

  
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");


        transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);
        transform.Translate(0, 0, y * Time.deltaTime * velocidad);

        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);

        




    }
}
