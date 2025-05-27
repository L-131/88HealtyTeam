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
    [SerializeField] private float dashSpeed; // ��� �ӵ� ����
    [SerializeField] private float jumpForce = 80f;// ���� ��
    public bool isDashing = false;// ��� ���θ� ��Ÿ���� ����
    public bool IsDashing => isDashing; // �б� ���� ������Ƽ�� ����
    [Header("Ground Check")]
    public LayerMask groundLayerMask;
    [Header("Jump")]
    private int _jumpStack;//���� ����, _jumpStack���� �����Ͽ� ��� ȣ�� ����
    [SerializeField] private int maxJumpStack;// 1�� ��� ���� ���� ����
    public int jumpStack
    {
        get => _jumpStack;
        set => _jumpStack = Mathf.Clamp(value, 0, maxJumpStack);
    }//���� ������ 0�� maxJumpStack ���̷� ����

    [Header("Look")]
    public Transform cameraContainer;
    public float minLookX;
    public float maxLookX;
    private float camCurXRotation;
    public float camSensitivity;
    private Vector2 mouseDelta;
    public Camera cam; // �÷��̾� ī�޶�

    [Header("UI")]
    [SerializeField] private GameObject tabMenuUI;// Tab UI ������Ʈ ���� (�ν����Ϳ��� �Ҵ�)
    [SerializeField] private GameObject rightClickUI; // ��Ŭ�� UI ������Ʈ (�ν����Ϳ��� �Ҵ�, ������Ʈ ������ ǥ���ϱ� ���� UI)

    private bool isPaused = false;

    private Rigidbody rb;
    private PlayerCondition playerCondition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        playerCondition = GetComponent<PlayerCondition>();
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
            jumpStack = maxJumpStack;
        }
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    void Move()
    {
        float speed = isDashing ? moveSpeed * dashSpeed : moveSpeed;
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= speed;
        dir.y = rb.velocity.y;

        rb.velocity = dir;
    }

    void CameraLook()
    {
        if (isPaused) return; // ���� �� ī�޶� ȸ�� ����
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
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (playerCondition != null && playerCondition.Stamina <= 0) return; // ���¹̳��� ������ ��� �Ұ�
        if (context.phase == InputActionPhase.Performed)
        {
            isDashing = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isDashing = false;
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
            if (Physics.Raycast(ray, out RaycastHit hit, 10f, mask)) // 10f: ��ȣ�ۿ� �Ÿ�
            {
                var interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    rightClickUI.SetActive(true); // ��Ŭ���� UI Ȱ��ȭ
                }
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            rightClickUI.SetActive(false); // ��Ŭ�� UI ��Ȱ��ȭ
        }
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (isPaused) return; // ���� �� �Է� ����
        // ������Ʈ�� �����ǰ� ���� ���� ����
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
                Debug.DrawRay(rays[i].origin, rays[i].direction * 0.2f, Color.red, 1f);
                return true;
            }
        }

        return false;
    }
}
