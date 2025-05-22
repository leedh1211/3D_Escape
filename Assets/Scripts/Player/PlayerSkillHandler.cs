using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkillInputHandler : MonoBehaviour
{
    private EquipmentSystem equipmentSystem;

    private void Awake()
    {
        equipmentSystem = GetComponent<EquipmentSystem>();
    }

    public void OnSkill(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            equipmentSystem.UseAllSkills(gameObject);
        }
    }
}