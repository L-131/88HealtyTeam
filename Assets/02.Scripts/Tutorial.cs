using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * ��� Ʃ�丮���� �ڷ�ƾ���� ����
 * TutorialCoroutine�� ����� Ʃ�丮���� ������ ������ �ش��ϴ� Ʃ�丮���� �����ϴ� �Լ��� �ڷ�ƾ�� �ִ´�
 * �̹� �������� �ڷ�ƾ�� ���� ���, ���� �ڷ�ƾ�� ����ϰ�, TutorialSwitch�� ���� �Լ��� ����� ������ ǥ���ϰ� �ִ� Ʃ�丮���� ȭ�鿡�� �����, ���ο� Ʃ�丮���� ��Ÿ�� �� �ֵ��� �Ѵ�
 * 
 * 1. Ʃ�丮�� UI�� ĵ������ �߰�
 * 2. OnTrigger��, OnCollision�̵� �̺�Ʈ�� �����ϸ� coroutine�� �ش� Ʃ�丮���� �����ϴ� �Լ��� �ִ´�
 * 3. �ڷ�ƾ���� Ȱ��ȭ��ų UI������Ʈ, ������Ʈ�� ������ �ִ´�
 */
/// <summary>
/// �÷��� ���, �Ǵ� ������ Ư¡ ���� �����ϴ� Ʃ�丮�� Ŭ����
/// </summary>
public class Tutorial : MonoBehaviour
{
    private Coroutine coroutine;
    private float waitTime = 5.0f; // Ʃ�丮�� UI�� ǥ�õǴ� �ð�

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.gameObject.name + "�� �浹��");

        switch (this.gameObject.name)
        {
            // memo: ������Ʈ �̸��� �ٲ�� �� ����ġ���� ������ �����ؾ� �Ѵ�. �ٸ� ����� ������?
            case "BasicTutorial":
                break;
            case "LobbyTutorial":
                break;
            case "PoisonMapTutorial":
                break;
            case "PoisonMapPuzzleTutorial":
                break;
            case "BeaconGimmickTutorial":
                break;
            default:
                Debug.Log("�� �� ���� Ʃ�丮�� ������Ʈ�Դϴ�.");
                break;
        }
    }

    /// <summary>
    /// ���� �ð� ��, ���� �������� Ʃ�丮�� ��Ȱ��ȭ
    /// </summary>
    /// <returns></returns>
    IEnumerable DisableTutorialUI()
    {
        yield return new WaitForSeconds(waitTime);

        // ���� Ȱ��ȭ�� Ʃ�丮�� UI�� ��Ȱ��ȭ
    }

    /// <summary>
    /// ���� ǥ������ Ʃ�丮�� UI�� ��� ��Ȱ��ȭ ��Ű��, ���ο� Ʃ�丮�� UI�� ǥ��
    /// </summary>
    private void CancleTutorialUI()
    {

    }
}
