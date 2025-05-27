using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager instance;// CharacterManager�� �ν��Ͻ��� ������ ���� ����
    public static CharacterManager Instance// CharacterManager�� �ν��Ͻ��� ��ȯ�ϴ� ������Ƽ
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return instance;
        }
    }

    public Player player;// ���� �÷��̾� ĳ���͸� ��Ÿ���� Player Ŭ������ �ν��Ͻ�

    public Player Player// �÷��̾� ĳ���͸� �������ų� �����ϴ� ������Ƽ
    {
        get { return player; }
        set { player = value; }
    }

    private void Awake()// CharacterManager�� �ν��Ͻ��� �ʱ�ȭ�ϴ� �޼���, ����� �̱��� ������ ����Ͽ� �ν��Ͻ��� �����մϴ�.
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}