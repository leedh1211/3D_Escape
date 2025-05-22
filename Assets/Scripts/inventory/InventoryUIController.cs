using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private Transform slotParent;
    [SerializeField] private GameObject slotPrefab;

    [SerializeField] private InventorySystem inventorySystem;

    [SerializeField] private PlayerInput playerInput;

    private List<ItemSlotUI> slots = new();
    private bool isOpen = false;

    private void OnEnable()
    {
        playerInput.actions["Inventory"].performed += OnInventory;
    }

    private void OnDisable()
    {
        playerInput.actions["Inventory"].performed -= OnInventory;
        inventorySystem.OnInventoryChanged -= RefreshUI;
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isOpen = !isOpen;
            inventoryUI.SetActive(isOpen);

            Cursor.visible = isOpen;
            Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    private void Start()
    {
        inventorySystem.OnInventoryChanged += RefreshUI;
        RefreshUI();
    }
    
    private void RefreshUI()
    {
        foreach (var slot in slots)
        {
            Destroy(slot.gameObject);
        }
        slots.Clear();
        
        for (int i = 0; i < inventorySystem.Items.Count; i++)
        {
            Debug.Log("인벤토리를 갱신했습니다.");
            var gameObject = Instantiate(slotPrefab, slotParent);
            var slot = gameObject.GetComponent<ItemSlotUI>();
            slot.Setup(inventorySystem.GetItem(i), inventorySystem);
            slots.Add(slot);
        }
    }
}