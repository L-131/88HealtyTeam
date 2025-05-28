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
    public PickupableItemData itemData;

    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();

        renderer.material.color = ChangeColor(itemData.itemColor);
    }

    /// <summary>
    /// �������� ���÷��� ���� �ݹ� �޼ҵ�
    /// </summary>
    public void OnPickup()
    {
        Debug.Log($"Picked up: {itemData.itemName} (ID: {itemData.itemID})");
    }

    /// <summary>
    /// ���ܿ� �������� ��ġ�Ǿ��� ���� �ݹ� �޼ҵ�
    /// </summary>
    public void OnPlace()
    {
        Debug.Log($"Placed {itemData.itemName} on");
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

    public InteractableController GetInteractableInfo()
    {
        InteractableController interactableInfo = new InteractableController
        {
            name = itemData.itemName,
            description = itemData.description,
        };

        return interactableInfo;
    }
}
