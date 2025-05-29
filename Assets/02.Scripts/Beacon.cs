using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : MonoBehaviour
{
    public BeaconData beaconData;

    private new Renderer renderer;
    [SerializeField] private bool isActivated = false;
    public IBeaconActivate beaconActivate;
    public GameObject door;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        beaconActivate = door.GetComponent<IBeaconActivate>();
        renderer.material.color = ChangeColor(beaconData.beaconColor);
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        PickupableItem item = collision.gameObject.GetComponent<PickupableItem>();

        if (item != null)
        {
            if (ReceiveItem(item))
            {
                item.OnPlace(); // �������� ���ܿ� ��ġ�Ǿ��� ��, ������ �۵� �޼ҵ� ȣ��

                ActivateGimmick();
            }
            else
            {
                Debug.Log("�ùٸ� Ű �������� �ƴմϴ�.");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        PickupableItem item = collision.gameObject.GetComponent<PickupableItem>();

        if (item != null)
        {
            isActivated = false; // ��Ȱ��ȭ ���·� ����

            ActivateGimmick();
        }
    }

    /// <summary>
    /// �������� ���ܿ� ��ġ�Ǿ��� ��, �ùٸ� ��ȣ�ۿ� ���������� ������ ���� �Ǻ�
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private bool ReceiveItem(PickupableItem item)
    {
        bool result = false;

        if (item.itemData.itemColor == beaconData.beaconColor)
        {
            result = true;

            isActivated = true;
        }

        return result;
    }
    
    /// <summary>
    /// ���ܰ� ����� ����� ���۽�Ű�� �޼ҵ�
    /// </summary>
    /// memo : ����� ����� ���ܿ� �����ų �ʿ䰡 �ִ�. ����� ����Ǿ� ���� ����
    private void ActivateGimmick()
    {
        if (isActivated && beaconActivate != null)
        {
            beaconActivate.ActivateBeacon();
        }
        else
        {
            beaconActivate.DeactivateBeacon();
        }
    }

    // memo : PickupableItem�� ChangeColor�ڵ�� �ߺ��̴�. ��ӹ޾Ƽ� ���� ���� ���� �� �ѵ�..Item, Ȥ�� Object��� Ŭ������ ����� ��ӹ޴°� ���� ������?
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
