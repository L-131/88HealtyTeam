using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;// �÷��̾��� �̵� �� ī�޶� ȸ���� ����ϴ� PlayerController Ŭ������ �ν��Ͻ�
    public PlayerCondition condition; // �÷��̾��� ���¸� ��Ÿ���� Condition Ŭ������ �ν��Ͻ�


    private void Awake()
    {
        CharacterManager.Instance.Player = this;// CharacterManager�� Player �Ӽ��� ���� Player �ν��Ͻ��� �Ҵ�
        controller = GetComponent<PlayerController>();// PlayerController ������Ʈ�� �����ͼ� controller ������ �Ҵ�
        condition = GetComponent<PlayerCondition>();// PlayerCondition ������Ʈ�� �����ͼ� condition ������ �Ҵ�
    }
}

