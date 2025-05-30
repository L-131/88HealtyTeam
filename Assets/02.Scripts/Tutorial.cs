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

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name + "�� �浹��");
    }

    /// <summary>
    /// ���� �ð� ��, ���� �������� Ʃ�丮�� ��Ȱ��ȭ
    /// </summary>
    /// <returns></returns>
    IEnumerable DisableTutorialUI()
    {
        yield return null;
    }

    /// <summary>
    /// ���� ǥ������ Ʃ�丮�� UI�� ��� ��Ȱ��ȭ ��Ű��, ���ο� Ʃ�丮�� UI�� ǥ��
    /// </summary>
    private void CancleTutorialUI()
    {

    }
}
