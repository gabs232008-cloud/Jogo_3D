using UnityEngine;

public class ControleJogador : MonoBehaviour
{
    public CharacterController controller;
    public Transform checaChao;
    public string tagChao = "Chao";

    public float velocidade = 6f;
    public float alturaPulo = 1.2f;
    public float gravidade = -19.62f;

    Vector3 velocidadeVertical;
    bool estaNoChao;

    void Update()
    {
        // 1. CHECAGEM DO CHÃO
        estaNoChao = false;
        Collider[] colisores = Physics.OverlapSphere(checaChao.position, 0.4f);
        foreach (Collider col in colisores)
        {
            if (col.gameObject != gameObject && col.CompareTag(tagChao))
            {
                estaNoChao = true;
                break;
            }
        }

        if (estaNoChao && velocidadeVertical.y < 0)
        {
            velocidadeVertical.y = -2f;
        }

        // 2. MOVIMENTO HORIZONTAL
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direcao = transform.right * x + transform.forward * z;
        controller.Move(direcao * velocidade * Time.deltaTime);

        // 3. PULO
        if (Input.GetButtonDown("Jump") && estaNoChao)
        {
            velocidadeVertical.y = Mathf.Sqrt(alturaPulo * -2f * gravidade);
        }

        // 4. GRAVIDADE
        velocidadeVertical.y += gravidade * Time.deltaTime;
        controller.Move(velocidadeVertical * Time.deltaTime);
    }
}