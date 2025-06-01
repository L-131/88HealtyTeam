using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialType
{
    Basic,
    Lobby,
    PoisonMap,
    PoisonMapPuzzle,
    BeaconGimmick
}

/// <summary>
/// �÷��� ���, �Ǵ� ������ Ư¡ ���� �����ϴ� Ʃ�丮�� Ŭ����
/// </summary>
public class Tutorial : MonoBehaviour
{
    private TutorialUI ui;

    [SerializeField] private TutorialType type;

    void Start()
    {
        ui = FindObjectOfType<TutorialUI>();
    }

    void Update()
    {
        
    }

    // �̹� ǥ���� Ʃ�丮���� name�� ����Ʈ�� ������ ��, ���� name�� �浹���� ���, ǥ������ �ʵ��� �Ѵ�
    private void OnTriggerEnter(Collider other)
    {
        if (this.GetComponent<Tutorial>() != null) // Ʃ�丮�� Ʈ���ſ� �浹���� ��
        {
            string tutorialText = "";   // Ʃ�丮�� �ؽ�Ʈ
            float waitTime = 5f; // UIǥ�� �ð�

            switch (this.type)
            {
                case TutorialType.Basic:
                    tutorialText = "�̵�: W, A, S, D\r\nL_Click: ��ȣ�ۿ�\r\nR_Click: ����\r\nL_Shift: �޸���\r\nSpace: ����, ���� ����";
                    waitTime = 10f;

                    break;
                case TutorialType.Lobby:
                    tutorialText = "��� ���� ������ ���۽�Ű�� Ż�ⱸ�� ������ �� �ֽ��ϴ�\r\n��� ���� ������ Ǯ�� Ż���ϼ���";
                    break;
                case TutorialType.PoisonMap:
                    tutorialText = "�͵� �����Դϴ�\r\n���� ��� ü���� �𿩳�����, �͵� �˿� ������ �� ���� ü���� �ҽ��ϴ�";

                    break;
                case TutorialType.PoisonMapPuzzle:
                    tutorialText = "��ȣ�ۿ��� ���� ������ Ǯ �� �ֽ��ϴ�\r\n������ Ŭ���ϸ� ���ڹ����� �������� ���մϴ�\r\n��� ������ ���� �Ȱ��� �ٲٸ� �����Դϴ�";

                    break;
                case TutorialType.BeaconGimmick:
                    tutorialText = "������ ������ ������ Ÿ�� �ö� Ű �������� �տ� ���� �� �ֽ��ϴ�\r\nŰ ������ ���� ��, �����۰� ���� ������ ���ܿ� �־� ���� Ŭ������ �� �ֽ��ϴ�";
                    waitTime = 10f;

                    break;
                default:
                    Debug.Log("�� �� ���� Ʃ�丮�� ������Ʈ�Դϴ�.");
                    break;
            }

            ui.ShowTutorial(type, tutorialText, waitTime);
        }
    }
}
