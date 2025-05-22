using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image iconImage;
    private Item item;
    private InventorySystem inventorySystem;

    private float lastClickTime = 0f;
    private const float doubleClickDelay = 0.25f;

    public void Setup(Item newItem, InventorySystem inventory)
    {
        inventorySystem = inventory;
        item = newItem;
        iconImage.sprite = item.icon;
        iconImage.enabled = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.time - lastClickTime < doubleClickDelay)
        {
            inventorySystem.UseItem(item);
        }
        lastClickTime = Time.time;
    }
}