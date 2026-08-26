using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovimentoTerceiraPessoa : MonoBehaviour
{
    private CharacterController controlador;
    private Vector3 direcaoMovimento;
    private float velocidadeVertical;

    [Header("Configurações de Movimento")]
    public float velocidadeMovimento = 6f;
    public float suavizacaoRotacao = 0.1f;
    private float velocidadeRotacao;

    [Header("Configurações de Pulo e Gravidade")]
    public float alturaPulo = 2f;
    public float gravidade = -9.81f;
    public Transform sensorChao;
    public float raioEsferaChao = 0.2f;
    public LayerMask mascaraChao;
    private bool estaNoChao;

    public Transform cameraPrincipal;

    void Start()
    {
        controlador = GetComponent<CharacterController>();
        if (cameraPrincipal == null && Camera.main != null)
        {
            cameraPrincipal = Camera.main.transform;
        }
    }

    void Update()
    {
        estaNoChao = Physics.CheckSphere(sensorChao.position, raioEsferaChao, mascaraChao);

        if (estaNoChao && velocidadeVertical < 0)
        {
            velocidadeVertical = -2f;
        }

        float movimentoHorizontal = Input.GetAxis("Horizontal");
        float movimentoVertical = Input.GetAxis("Vertical");
        Vector3 direcao = new Vector3(movimentoHorizontal, 0f, movimentoVertical).normalized;

        if (direcao.magnitude >= 0.1f)
        {
            float anguloAlvo = Mathf.Atan2(direcao.x, direcao.z) * Mathf.Rad2Deg + cameraPrincipal.eulerAngles.y;
            float angulo = Mathf.SmoothDampAngle(transform.eulerAngles.y, anguloAlvo, ref velocidadeRotacao, suavizacaoRotacao);
            transform.rotation = Quaternion.Euler(0f, angulo, 0f);

            Vector3 direcaoMovimentoRotacionada = Quaternion.Euler(0f, anguloAlvo, 0f) * Vector3.forward;
            controlador.Move(direcaoMovimentoRotacionada.normalized * velocidadeMovimento * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && estaNoChao)
        {
            velocidadeVertical = Mathf.Sqrt(alturaPulo * -2f * gravidade);
        }

        velocidadeVertical += gravidade * Time.deltaTime;
        controlador.Move(new Vector3(0f, velocidadeVertical, 0f) * Time.deltaTime);
    }
}