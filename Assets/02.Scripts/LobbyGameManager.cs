// LobbyGameManager.cs
using UnityEngine;

public class LobbyGameManager : MonoBehaviour
{
    public bool isStage1Cleared = false; // ����: �������� 1 Ŭ���� ����
    // (������) �̱������� ����� �ٸ� ������ ���� �����ϰ� �� �� �ֽ��ϴ�.
    // public static LobbyGameManager Instance;
    // void Awake() { if (Instance == null) Instance = this; else Destroy(gameObject); }

    public void UpdateStageStatus(string stageName, bool cleared)
    {
        if (stageName == "Stage1") // MVP������ �������� 1�� ����
        {
            isStage1Cleared = cleared;
            Debug.Log("LobbyGameManager: Stage 1 clear status updated to " + cleared);

            // TODO: �� ���� ���濡 ���� �κ� ��ư�� �ð��� ������Ʈ�� ��û�ϴ� ���� �߰� (3�ܰ迡��)
        }
    }
}