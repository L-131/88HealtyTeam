// LobbyButton.cs
using UnityEngine;

public class LobbyButton : MonoBehaviour
{
    // �� Inspector���� �ݵ�� �Ҵ����ּ���! ��
    public LobbyGameManager lobbyGameManager;
    public Material clearedMaterial;        // �������� Ŭ���� �� ������ ��Ƽ����
    public Material originalMaterial;       // ���� ������ �� ������ ��Ƽ���� (���� �� ��Ƽ����� �ٸ� ��� ���)

    public string targetStageName = "Stage1"; // �� ��ư�� ������ �������� �̸�

    private Renderer buttonRenderer;
    private Material internalOriginalMaterial; // ��ũ��Ʈ ���� ������ ���� ��Ƽ���� �����

    void Start()
    {
        Debug.Log(gameObject.name + " - LobbyButton Start() ȣ���.");

        buttonRenderer = GetComponent<Renderer>();
        if (buttonRenderer == null)
        {
            Debug.LogError(gameObject.name + " - Renderer ������Ʈ�� ã�� �� �����ϴ�! �� ��ũ��Ʈ�� Mesh Renderer�� �ִ� 3D ������Ʈ(��: Cube)�� �־�� �մϴ�.");
            enabled = false; // ������ ������ ��ũ��Ʈ ��Ȱ��ȭ
            return;
        }
        Debug.Log(gameObject.name + " - Renderer ã��: " + buttonRenderer.GetType().Name);

        // ���� ������ ��Ƽ������ ���� ������ ���� (Inspector���� originalMaterial�� �Ҵ��ߴٸ� �װ��� �켱 ���)
        if (originalMaterial != null)
        {
            internalOriginalMaterial = originalMaterial;
            Debug.Log(gameObject.name + " - Inspector�� �Ҵ�� originalMaterial ���: " + internalOriginalMaterial.name);
        }
        else if (buttonRenderer.sharedMaterial != null)
        { // sharedMaterial�� �о�ͼ� �ν��Ͻ��� ������ �ʵ��� ����
            internalOriginalMaterial = buttonRenderer.sharedMaterial; // ������ ��Ƽ����� ��� (���� ��Ƽ����)
            Debug.Log(gameObject.name + " - ���� buttonRenderer.sharedMaterial�� originalMaterial�� ���: " + internalOriginalMaterial.name);
        }
        else
        {
            Debug.LogWarning(gameObject.name + " - originalMaterial�� ������ �� �����ϴ�. Renderer�� ��Ƽ������ �����ϴ�.");
        }


        // LobbyGameManager ���� Ȯ�� (Inspector���� �Ҵ� �ȵ����� ã�ƺ���)
        if (lobbyGameManager == null)
        {
            Debug.LogWarning(gameObject.name + " - LobbyGameManager�� Inspector�� �Ҵ���� �ʾҽ��ϴ�. ������ FindObjectOfType���� ã���ϴ�.");
            lobbyGameManager = FindObjectOfType<LobbyGameManager>();
        }

        if (lobbyGameManager == null)
        {
            Debug.LogError(gameObject.name + " - LobbyGameManager �ν��Ͻ��� ã�� �� �����ϴ�! Update ������ ���� �۵����� �ʽ��ϴ�.");
            enabled = false; // ������ ������ ��ũ��Ʈ ��Ȱ��ȭ
            return;
        }
        Debug.Log(gameObject.name + " - LobbyGameManager ���� ����: " + lobbyGameManager.gameObject.name);

        // ���� ���� �� �ʱ� ��Ƽ���� ���� (originalMaterial�� Inspector�� �����Ǿ� ������ �װ�����, �ƴϸ� ���� ���� ����)
        if (internalOriginalMaterial != null)
        {
            buttonRenderer.material = internalOriginalMaterial; // ���⼭ material�� �Ҵ��ϸ� �ν��Ͻ��� ������
        }
    }

    void Update()
    {
        // Start()���� �ֿ� ������ �� ã������ Update ���� �� �� (������ enabled = false ó��)
        // Debug.Log(gameObject.name + " - LobbyButton Update() ȣ���."); // �ʹ� ���� �����Ƿ� �ʿ�ÿ��� �ּ� ����

        if (lobbyGameManager == null || buttonRenderer == null)
        {
            return; // �ʼ� ���� ������ ���� �ߴ�
        }

        // LobbyGameManager�� isStage1Cleared ���� �ǽð����� �о��
        bool stageIsCurrentlyClearedInManager = lobbyGameManager.isStage1Cleared;

        // (������) ���� ���� ������ �� ������ �α� (�ʿ� ������ �ּ� ó��)
        // Debug.Log(gameObject.name + $" - Update: target='{targetStageName}', LGM.isStage1Cleared='{stageIsCurrentlyClearedInManager}'");

        if (stageIsCurrentlyClearedInManager && targetStageName == "Stage1")
        {
            // ���������� Ŭ����� �����̰�, �� ��ư�� Stage1�� ����� ���
            if (clearedMaterial != null)
            {
                if (buttonRenderer.sharedMaterial != clearedMaterial) // �̹� ����� ��Ƽ������ �ƴϸ� ����
                {
                    buttonRenderer.material = clearedMaterial;
                    Debug.Log(gameObject.name + " - 'CLEARED' ���·� ��Ƽ���� ���� �õ�!");
                }
            }
            else
            {
                // Debug.LogWarning(gameObject.name + " - clearedMaterial�� Inspector�� �Ҵ���� �ʾҽ��ϴ�.");
            }
        }
        else
        {
            // ���������� Ŭ������� �ʾҰų�, �� ��ư�� Stage1 ����� �ƴ� ��� (Stage1�� �ƴ� �ٸ� targetStageName�� ���� ����)
            if (internalOriginalMaterial != null)
            {
                if (buttonRenderer.sharedMaterial != internalOriginalMaterial) // �̹� ����� ��Ƽ������ �ƴϸ� ����
                {
                    buttonRenderer.material = internalOriginalMaterial;
                    Debug.Log(gameObject.name + " - 'NOT CLEARED' ���·� ��Ƽ���� ���� �õ�!");
                }
            }
            else
            {
                // Debug.LogWarning(gameObject.name + " - internalOriginalMaterial�� �������� �ʾҽ��ϴ�.");
            }
        }
    }
}