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

public class PickupableItem : MonoBehaviour
{
    public PickupableItemData itemData;

    private Renderer renderer;

    private void Start()
    {
        Debug.Log($"create: {itemData.itemName} (ID: {itemData.itemID} COLOR: {itemData.itemColor})");

        renderer = GetComponent<Renderer>();

        renderer.material.color = ChangeColor(itemData.itemColor);
    }

    /// <summary>
    /// �������� ���÷��� ���� �ݹ� �޼ҵ�
    /// </summary>
    private void OnPickup()
    {
        Debug.Log($"Picked up: {itemData.itemName} (ID: {itemData.itemID})");
    }

    /// <summary>
    /// ���ܿ� �������� ��ġ�Ǿ��� ���� �ݹ� �޼ҵ�
    /// </summary>
    private void OnPlace()
    {

    }

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
}
