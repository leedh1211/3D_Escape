using UnityEngine;

public class TigerGiver : MonoBehaviour, IInteractable
{
    [SerializeField] private Item itemToGive;

    public void Interact(GameObject interactor)
    {
        Debug.Log("InteractTiger");
        var inventory = interactor.GetComponent<InventorySystem>();
        if (inventory != null)
        {
            inventory.AddItem(itemToGive);
            Debug.Log($"[TigerGiver] {itemToGive.name} 지급 완료");
        }
    }
}