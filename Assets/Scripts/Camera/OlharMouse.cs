using UnityEngine;

public class OlharMouse : MonoBehaviour
{
    public float sensibilidadeMouse = 100f;
    public Transform alvo; // O objeto que a câmera vai seguir (o Jogador)
    public Vector3 deslocamento = new Vector3(0f, 2f, -5f); // Distância atrás e acima do jogador

    float rotY = 0f;
    float rotX = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        // 1. Leitura do movimento do mouse
        rotY += Input.GetAxis("Mouse X") * sensibilidadeMouse * Time.deltaTime;
        rotX -= Input.GetAxis("Mouse Y") * sensibilidadeMouse * Time.deltaTime;

        // Limita o ângulo de visão vertical para a câmera não girar demais por baixo/cima
        rotX = Mathf.Clamp(rotX, -20f, 60f);

        // 2. Calcula a rotação da câmera
        Quaternion rotacao = Quaternion.Euler(rotX, rotY, 0f);

        // 3. Atualiza a posição e faz a câmera olhar sempre para o jogador
        transform.position = alvo.position + rotacao * deslocamento;
        transform.LookAt(alvo.position + Vector3.up * 1.5f);

        // 4. Faz o jogador girar na direção horizontal que a câmera está apontando
        alvo.rotation = Quaternion.Euler(0f, rotY, 0f);
    }
}