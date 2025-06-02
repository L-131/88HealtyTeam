// PauseUI.cs
using UnityEngine;
using UnityEngine.UI; // Button, Slider �� UI ��� ����� ���� �ʿ�
using UnityEngine.SceneManagement; // �� ��ȯ�� ���� �ʿ� (��: ���� �޴��� ����)

public class PauseUI : MonoBehaviour
{
    public static PauseUI Instance; // �ٸ� ��ũ��Ʈ���� ���� �����ϱ� ���� �̱��� (������)

    [Header("UI Panels")]
    public GameObject pauseMenuPanel; // �Ͻ����� �޴� ��ü �г�

    [Header("Buttons")]
   
    public Button saveButton;           // ���� ��ư
    public Button loadButton;           // �ҷ����� ��ư
   

    public static bool isGamePaused = false; // ���� ������ �Ͻ����� �������� Ȯ���ϴ� static ����

    void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // PauseUI�� �� ��ȯ �ÿ��� �����Ǿ�� �Ѵٸ�
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // ���� �ÿ��� �Ͻ����� �޴��� ��Ȱ��ȭ
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }
        isGamePaused = false; // �ʱ� ���� ���´� '�Ͻ����� �ƴ�'
        Time.timeScale = 1f;    // ���� �ð� ���� �ӵ�

        // �� ��ư�� ��� ����
        
        if (saveButton != null)
        {
            saveButton.onClick.AddListener(OnSaveButtonClicked);
        }
        if (loadButton != null)
        {
            loadButton.onClick.AddListener(OnLoadButtonClicked);
        }
       
    }

    void Update()
    {
        // ESC Ű�� ������ �Ͻ����� �޴� ���
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f; // ���� �ð� ����
        isGamePaused = true;
        Cursor.lockState = CursorLockMode.None; // ���콺 Ŀ�� ���� ����
        Cursor.visible = true; // ���콺 Ŀ�� ���̱�

        if (GameManager.Instance != null)
        {
            GameManager.Instance.IsGamePaused = true; // GameManager���� �Ͻ����� ���� �˸�
        }
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f; // ���� �ð� ����ȭ
        isGamePaused = false;
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ����
        Cursor.visible = false; // ���콺 Ŀ�� �����

        if (GameManager.Instance != null)
        {
            GameManager.Instance.IsGamePaused = false; // GameManager�� �Ͻ����� ���� �˸�
        }
        Debug.Log("Game Resumed");
    }

    void OnSaveButtonClicked()
    {
        Debug.Log("Save Button Clicked from Pause Menu!");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SaveCurrentGameData(); // GameManager�� ���� �Լ� ȣ��
            // TODO: "���� �Ϸ�!" ���� �ǵ�� UI ǥ��
        }
        else
        {
            Debug.LogError("GameManager.Instance not found. Cannot save game.");
        }
    }

    void OnLoadButtonClicked()
    {
        Debug.Log("Load Button Clicked from Pause Menu!");
        if (GameManager.Instance != null)
        {
            // �ε� �Ŀ��� ������ �簳�Ǿ�� �ϹǷ� Time.timeScale�� �ٽ� 1�� �����ؾ� ��
            // LoadAndApplyGameData ���� ResumeGame�� ȣ���ϰų�, ���⼭ ���� ó��
            
            GameManager.Instance.LoadAndApplyGameData(); // GameManager�� �ε� �� ���� �Լ� ȣ��
            // TODO: "�ε� �Ϸ�!" ���� �ǵ�� UI ǥ��
        }
        else
        {
            Debug.LogError("GameManager.Instance not found. Cannot load game.");
        }
    }

  
}