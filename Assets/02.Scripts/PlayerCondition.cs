using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerCondition : MonoBehaviour
{
    public float health;// �÷��̾��� ���� ü��
    public float Stamina;// �÷��̾��� ���� ���¹̳�
    public float staminaDecreasePerSec = 20f;// ��� �� �ʴ� ���¹̳� ���ҷ�
    public float staminaRegenPerSec = 10f;// ���¹̳� �ʴ� ȸ����

    private bool isEmergencyBGMPlaying = false;

    private PlayerController playerController;

    [SerializeField] private GameObject DieUI;
    private bool isDead = false;


    public event Action onTakeDamage;//���ظ� �Ծ��� �� �߻��ϴ� ��������Ʈ �̺�Ʈ

    void Start()
    {
        if (DieUI != null)
            DieUI.SetActive(false);//���� ������ �� UI ��Ȱ��ȭ
    }

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    void Update()
    {
        if (!isDead && health <= 0)
        {
            Die();
        }

        //  ü���� 30 ������ �� ��� BGM���� ��ȯ
        if (!isEmergencyBGMPlaying && health <= 30)
        {
            SoundManager.Instance.PlayStageBGM("Stage", true);
            isEmergencyBGMPlaying = true;
        }

        // ü���� 31 �̻����� ȸ���Ǹ� �Ϲ� BGM���� ���� (���� ����)
        if (isEmergencyBGMPlaying && health > 30)
        {
            SoundManager.Instance.PlayStageBGM("Stage", false);
            isEmergencyBGMPlaying = false;
        }

        StaminaAmountOfChange();
    }
    void Die()
    {
        if (isDead) return;//�̹� ���� ���¶�� �Լ� ����
        isDead = true;//���� ���·� ����
        // �÷��̾ �׾��� �� �޼��� ���� ���� �ȵ�
    }

    public void TakePhysicalDamage(int damage)
    {
        health -= damage; // health�� ���� ���� damage�� ����
        health = Mathf.Clamp(health, 0, 100); // health�� 0~100 ���̷� ����
        onTakeDamage?.Invoke();//���ظ� �Ծ��� �� �߻��ϴ� �̺�Ʈ ȣ��, ��������Ʈ�� �ؾ� Ȯ�强 ����
    }
    private void StaminaAmountOfChange()
    {
        if (playerController != null)
        {
            if (Stamina <= 0)
            {
                Coroutine dashCoroutine = StartCoroutine(playerController.DashCooldown()); // ��� ��Ÿ�� �ڷ�ƾ ����
            }
            if (playerController.IsDashing)
            {
                Stamina -= staminaDecreasePerSec * Time.deltaTime;
            }
            else
            {
                Stamina += staminaRegenPerSec * Time.deltaTime;
            }
            Stamina = Mathf.Clamp(Stamina, 0, 100);
        }
    }
}

