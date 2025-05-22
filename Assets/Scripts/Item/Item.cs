using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    public virtual void Use(GameObject user)
    {
        Debug.Log($"{itemName} 아이템 사용");
    }
}