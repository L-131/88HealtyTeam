using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemColor
{
    RED,
    BLUE,
    GREEN,
    YELLOW,
}

public class PickupableItem : MonoBehaviour, IInteractable
{
    public ObjectData objectData;

    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();

        renderer.material.color = ChangeColor(objectData.objectColor);
    }

    /// <summary>
    /// �������� ���÷��� ���� �ݹ� �޼ҵ�
    /// </summary>
    public void OnPickup()
    {
        Debug.Log($"Picked up: {objectData.objectName} (ID: {objectData.objectID})");
    }

    /// <summary>
    /// ���ܿ� �������� ��ġ�Ǿ��� ���� �ݹ� �޼ҵ�
    /// </summary>
    public void OnPlace()
    {
        Debug.Log($"Placed {objectData.objectName} on");
    }

    // memo : Beacon�� ChangeColor�ڵ�� �ߺ��̴�. ��ӹ޾Ƽ� ���� ���� ���� �� �ѵ�..Item, Ȥ�� Object��� Ŭ������ ����� ��ӹ޴°� ���� ������?
    private Color ChangeColor(ItemColor itemColor)
    {
        switch (itemColor)
        {
            case ItemColor.RED:
                return Color.red;
            case ItemColor.BLUE:
                return Color.blue;
            case ItemColor.GREEN:
                return Color.green;
            case ItemColor.YELLOW:
                return Color.yellow;
            default:
                return Color.white;
        }
    }

    public ObjectData GetInteractableInfo()
    {
        return objectData;
    }
}
