using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MapExplorerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;    // �⺻ �̵� �ӵ�
    public float sprintMultiplier = 1.5f;   // �޸��� ����
    public float jumpHeight = 1.3f;  // ���� ����
    public float gravity = -9.81f;// �߷� ���ӵ�

    [Header("Mouse Look")]
    public float lookSensitivity = 2f;    // ���콺 ����
    public float maxLookX = 90f;  // ���� �Ĵٺ� �ִ� ����
    public float minLookX = -90f;  // �Ʒ��� �Ĵٺ� �ִ� ����

    private CharacterController controller;
    private Transform cameraTransform;
    private float xRotation = 0f;
    private float verticalVel = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovementAndJump();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        // ���� ȸ�� (��ġ)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minLookX, maxLookX);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // ���� ȸ�� (��)
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovementAndJump()
    {
        // ���鿡 ��� ������ ���� �ӵ��� �ణ�� ������ ����
        if (controller.isGrounded && verticalVel < 0f)
            verticalVel = -2f;

        // ���� �Է� (�����̽�)
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            verticalVel = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // �߷� ����
        verticalVel += gravity * Time.deltaTime;

        // WASD �Է�
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        // �޸��� (����Ʈ) üũ
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float speed = moveSpeed * (isSprinting ? sprintMultiplier : 1f);

        // ���� �̵� ����
        Vector3 velocity = move * speed + Vector3.up * verticalVel;
        controller.Move(velocity * Time.deltaTime);
    }
}
