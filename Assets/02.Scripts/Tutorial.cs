using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��� ���, �Ǵ� ������ Ư¡ ���� �����ϴ� Ʃ�丮�� Ŭ����
/// </summary>
public class Tutorial : MonoBehaviour
{
    private TutorialUI ui;
    private Coroutine coroutine;
    private List<string> alreadyShowTutorials = new List<string>(); // �̹� ǥ���� Ʃ�丮�� ����Ʈ

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
            if (alreadyShowTutorials.Contains(this.gameObject.name))
                return; // �̹� ǥ���� Ʃ�丮���� �ٽ� ǥ������ ����

            float waitTime = 5f; // UIǥ�� �ð�

            if (coroutine != null)
            {
                StopCoroutine(coroutine); // �̹� �������� �ڷ�ƾ�� �ִٸ� ����
            }

            ui.gameObject.SetActive(true);

            switch (this.gameObject.name)
            {
                // memo: ������Ʈ �̸��� �ٲ�� �� ����ġ���� ������ �����ؾ� �Ѵ�. �ٸ� ����� ������?
                case "BasicTutorial":
                    ui.tutorialText.text = "�̵�: W, A, S, D\r\nL_Click: ��ȣ�ۿ�\r\nR_Click: ����\r\nL_Shift: �޸���\r\nSpace: ����, ���� ����";
                    waitTime = 10f;
                    break;
                case "LobbyTutorial":
                    ui.tutorialText.text = "��� ���� ������ ���۽�Ű�� Ż�ⱸ�� ������ �� �ֽ��ϴ�\r\n��� ���� ������ Ǯ�� Ż���ϼ���";
                    break;
                case "PoisonMapTutorial":
                    ui.tutorialText.text = "�͵� �����Դϴ�\r\n���� ��� ü���� �𿩳�����, �͵� �˿� ������ �� ���� ü���� �ҽ��ϴ�";

                    break;
                case "PoisonMapPuzzleTutorial":
                    ui.tutorialText.text = "��ȣ�ۿ��� ���� ������ Ǯ �� �ֽ��ϴ�\r\n������ Ŭ���ϸ� ���ڸ������ ������ ���� ���մϴ�\r\n��� ������ ���� �Ȱ��� �ٲٸ� �����Դϴ�";

                    break;
                case "BeaconGimmickTutorial":
                    ui.tutorialText.text = "������ ������ ������ Ÿ�� �ö� Ű �������� �տ� ���� �� �ֽ��ϴ�\r\nŰ ������ ���� ��, �����۰� ���� ������ ���ܿ� �־� ���� Ŭ������ �� �ֽ��ϴ�";

                    break;
                default:
                    Debug.Log("�� �� ���� Ʃ�丮�� ������Ʈ�Դϴ�.");
                    break;
            }

            alreadyShowTutorials.Add(this.gameObject.name); // ǥ���� Ʃ�丮���� �ٽ� ǥ������ �ʵ��� ����Ʈ�� ����
            coroutine = StartCoroutine(DisableTutorialUI(waitTime));
        }
    }

    /// <summary>
    /// ���� �ð� ��, ���� �������� Ʃ�丮�� ��Ȱ��ȭ
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisableTutorialUI(float duration)
    {
        yield return new WaitForSeconds(duration);

        // ���� Ȱ��ȭ�� Ʃ�丮�� UI�� ��Ȱ��ȭ
        ui.gameObject.SetActive(false);
    }
}
