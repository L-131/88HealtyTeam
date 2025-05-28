using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private Coroutine poisoning;

    private void Start()
    {
        oriSpeed = CharacterManager.Instance.Player.controller.moveSpeed;
    }


    public void ApplyEffect(Player player)
    {
        if (type == HazardType.SLOWING_LIQUID)
        {
            effectValue = 2.0f;
            player.controller.moveSpeed /= effectValue;
            Debug.Log($"�ӵ� ����: {player.controller.moveSpeed}");
        }
        else if (type == HazardType.POISON_GAS_AREA)
        {
            effectValue = 1.0f;
            poisoning = StartCoroutine(Poisoning((int)effectValue));
        }
    }

    public void RemoveEffect(Player player)
    {
        if (type == HazardType.SLOWING_LIQUID)
        {
            player.controller.moveSpeed = oriSpeed;
            Debug.Log($"�ӵ� ����ȭ: {player.controller.moveSpeed}");
        }
        else if (type == HazardType.POISON_GAS_AREA)
        {
            StopCoroutine(poisoning);
            Debug.Log($"�ߵ� ����, ����ü��{player.condition.health}");
        }
    }

    public IEnumerator Poisoning(int damage)
    {
        while(true)
        {
            Debug.Log($"{damage}��ŭ�� ����!");
            CharacterManager.Instance.Player.condition.TakePhysicalDamage(damage);
            yield return new WaitForSeconds(2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>(); 
        if (player != null)
        {
            ApplyEffect(player);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>(); 
        if (player != null)
        {
            RemoveEffect(player);
        }
    }

}
