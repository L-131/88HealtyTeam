using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MapExplorerController : MonoBehaviour
{
    [Header("�̵� ����")]
    public float moveSpeed = 5f;   // �̵� �ӵ�

    [Header("���콺 �� ����")]
    public float lookSensitivity = 2f;   // ���콺 ����
    public float maxLookX = 90f; // ���� �Ĵٺ� �ִ� ����
    public float minLookX = -90f; // �Ʒ��� �Ĵٺ� �ִ� ����

    private CharacterController controller;
    private Transform cameraTransform;
    private float xRotation = 0f;

    void Start()
    {
        // �ݵ�� �÷��̾� ��ü���� CharacterController�� �پ� �־�� �մϴ�.
        controller = GetComponent<CharacterController>();

        // ���� MainCamera �±� �޸� ī�޶� �ڽ����� �ΰ� ����ϰų�,
        // MainCamera �±׷� ã�Ƽ� cameraTransform�� �Ҵ�
        cameraTransform = Camera.main.transform;

        // ���콺 Ŀ���� ȭ�� �߾ӿ� �����ϰ� ����ϴ�.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // ������ ���콺 �� ��������������������������������������������������������������
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        // ��ġ(pitch): ���Ʒ� ������ ������Ŭ����
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minLookX, maxLookX);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // ��(yaw): �÷��̾� ��ü ȸ��(�¿�)
        transform.Rotate(Vector3.up * mouseX);

        // ������ WASD �̵� ��������������������������������������������������������������
        float x = Input.GetAxis("Horizontal");   // A/D, ��/��
        float z = Input.GetAxis("Vertical");     // W/S, ��/��

        // ���� ��/�� �������� �̵� ���� ����
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}
