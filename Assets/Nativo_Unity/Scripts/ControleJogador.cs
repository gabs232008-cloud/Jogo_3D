using UnityEngine;

public class ControleJogador : MonoBehaviour
{
    public CharacterController controller;

    // Velocidades e força do pulo
    public float velocidade = 6f;
    public float gravidade = -19.62f;
    public float alturaPulo = 1.2f;

    // Checagem de chão por Tag
    public Transform checaChao;
    public float distanciaChao = 0.4f;
    public string tagChao = "Chao";

    Vector3 velocidadeVertical;
    bool estaNoChao;

    void Update()
    {
        // 1. Verifica se há colisores no ponto do pé e se algum tem a Tag correspondente
        estaNoChao = VerificarSeEstaNoChao();

        if (estaNoChao && velocidadeVertical.y < 0)
        {
            velocidadeVertical.y = -2f; // Mantém o jogador levemente colado ao chão
        }

        // 2. Pegar os comandos do teclado (WASD / Setas)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 3. Mover na direção em que o jogador está olhando
        Vector3 mover = transform.right * x + transform.forward * z;
        controller.Move(mover * velocidade * Time.deltaTime);

        // 4. Comando de Pulo
        if (Input.GetButtonDown("Jump") && estaNoChao)
        {
            velocidadeVertical.y = Mathf.Sqrt(alturaPulo * -2f * gravidade);
        }

        // 5. Aplicar a gravidade ao longo do tempo
        velocidadeVertical.y += gravidade * Time.deltaTime;
        controller.Move(velocidadeVertical * Time.deltaTime);
    }

    bool VerificarSeEstaNoChao()
    {
        // Cria uma esfera de colisão invisível na base do jogador
        Collider[] colisores = Physics.OverlapSphere(checaChao.position, distanciaChao);

        foreach (Collider col in colisores)
        {
            // Ignora a colisão do próprio jogador
            if (col.gameObject != gameObject && col.CompareTag(tagChao))
            {
                return true;
            }
        }
        return false;
    }
}