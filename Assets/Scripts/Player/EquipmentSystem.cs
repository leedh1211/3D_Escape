using System.Collections.Generic;
using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    public List<Item> equippedItems = new(); // 장착 중인 아이템들

    public void AddEquippedItem(Item item)
    {
        if (!equippedItems.Contains(item))
            equippedItems.Add(item);
    }

    public void RemoveEquippedItem(Item item)
    {
        equippedItems.Remove(item);
    }

    public void UseAllSkills(GameObject user)
    {
        foreach (var item in new List<Item>(equippedItems))
        {
            if (item is ISkillUsable skillItem)
            {
                skillItem.UseSkill(user);
            }
        }
    }
}