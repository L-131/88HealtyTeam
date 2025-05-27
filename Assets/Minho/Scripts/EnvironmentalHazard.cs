using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HazardType
{
    SLOWING_LIQUID,
    POISON_GAS_AREA
}


public class EnvironmentalHazard : MonoBehaviour
{
    public HazardType type;
    private float oriSpeed;
    private float effectValue;


    private void Start()
    {
        // �÷��̾��� ���� �ӵ��� �Ҵ�
    }


    public void ApplyEffect()
    {
        if (type == HazardType.SLOWING_LIQUID)
        {
            // �÷��̾� �ӵ� ����
        }
        else if (type == HazardType.POISON_GAS_AREA)
        {
            // �÷��̾� �ߵ�
        }
    }

    public void RemoveEffect()
    {
        if (type == HazardType.SLOWING_LIQUID)
        {
            // �÷��̾� �ӵ� ����
        }
        else if (type == HazardType.POISON_GAS_AREA)
        {
            // �÷��̾� �ߵ�����
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }

}
