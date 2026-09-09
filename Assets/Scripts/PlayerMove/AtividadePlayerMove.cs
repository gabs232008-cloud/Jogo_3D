using Unity.VisualScripting;
using UnityEngine;

public class Atividade : MonoBehaviour
{
    [Header("Movimeto")]

    public float velocidade = 5f;
    public float forcaPulo = 6f;

    private Rigidbody rb;
    private bool noChao;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")&& noChao)
        {
            rb.AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
        }
    }


    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direcao = new Vector3(h, 0f, v) * velocidade;
        direcao.y = rb.linearVelocity.y;
        rb.linearVelocity = direcao;
    }

    private void OnCollisionStay(Collision colisao)
    {
        if(colisao.gameObject.CompareTag("Chao"))
        {
            noChao = true;
        }
    }

    private void OnCollisionExit(Collision colisao)
    {
      
            noChao = false;
        
    }
}
