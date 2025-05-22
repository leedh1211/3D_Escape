using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private List<Item> items = new();
    public IReadOnlyList<Item> Items => items;
    public event Action OnInventoryChanged;
    
    public void AddItem(Item item)
    {
        items.Add(item);
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        Debug.Log(items.Count);
        OnInventoryChanged?.Invoke();
    }

    public Item GetItem(int index)
    {
        return index >= 0 && index < items.Count ? items[index] : null;
    }
    
    public void UseItem(Item item)
    {
        item.Use(player);
        RemoveItem(item);
        
    }
}