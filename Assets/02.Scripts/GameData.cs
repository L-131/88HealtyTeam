// PlayerData.cs (�� C# ��ũ��Ʈ ����)
using System;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public float playerHealth;
    public float playerStamina;

    // ������: �⺻������ �ʱ�ȭ (������)
    public GameData()
    {
        playerPosition = Vector3.zero; // �⺻ ���� ��ġ�� ���� ����Ʈ�� ���� ����
        playerRotation = Quaternion.identity;
        playerHealth = 100f; // �⺻ ü��
        playerStamina = 100f; // �⺻ ���¹̳�
    }
}