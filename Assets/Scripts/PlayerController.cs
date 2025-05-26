using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable //������Ʈ ��ȣ�ۿ��� �����Ͻô� �е��� �ش� �������̽��� ������Ʈ�� ��ũ��Ʈ�� ��ӹ޾Ƽ� ������ �ؿ�.
{
    void OnInteract();
}

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    [SerializeField] private Vector2 curMovementInput;//���� �̵� �Է°�
    [SerializeField] private float jumpForce = 80f;
    public LayerMask groundLayerMask;
    private int _jumpStack;//���� ����, _jumpStack���� �����Ͽ� ��� ȣ�� ����
    public int jumpStack
    {
        get => _jumpStack;
        set => _jumpStack = Mathf.Clamp(value, 0, 1);
    }//���� ������ 0�� 1 ���̷� ����

    [Header("Look")]
    public Transform cameraContainer;
    public float minLookX;
    public float maxLookX;
    [SerializeField] private float camCurXRotation;
    public float camSensitivity;
    [SerializeField] private Vector2 mouseDelta;
    public Camera cam; // �÷��̾� ī�޶�

    // 1. Tab UI ������Ʈ ���� (�ν����Ϳ��� �Ҵ�)
    [SerializeField] private GameObject tabMenuUI;

    private bool isPaused = false;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void FixedUpdate()
    {
        Move();
        // ���� ������� ���� ���� ����
        if (IsGrounded())
        {
            jumpStack = 1;
        }
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = rb.velocity.y;

        rb.velocity = dir;
    }

    void CameraLook()
    {
        camCurXRotation += mouseDelta.y * camSensitivity;
        camCurXRotation = Mathf.Clamp(camCurXRotation, minLookX, maxLookX);
        cameraContainer.localRotation = Quaternion.Euler(-camCurXRotation, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * camSensitivity, 0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (isPaused) return; // ���� �� �Է� ����
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (isPaused) return; // ���� �� �Է� ����
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isPaused) return; // ���� �� �Է� ����
        if (context.phase == InputActionPhase.Performed)
        {
            if (IsGrounded())
            {
                // ���� ����� �� ����
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else if (jumpStack > 0)
            {
                // ���� ����
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpStack--;
            }
        }
    }
    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (isPaused) return; // ���� �� �Է� ����
        if (context.phase == InputActionPhase.Performed)
        {
            // ī�޶� �߾ӿ��� Ray�� ��
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            int mask = ~LayerMask.GetMask("Player"); // �÷��̾� ���̾ ������ ��� ���̾ ���� Raycast
            if (Physics.Raycast(ray, out RaycastHit hit, 30f, mask)) // 30f: ��ȣ�ۿ� �Ÿ�
            {
                var interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.OnInteract();
                }
            }
        }
    }

    public void OnTab(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f;
            if (tabMenuUI != null)
                tabMenuUI.SetActive(isPaused);
        }
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up*0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up*0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up*0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up*0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }
}
