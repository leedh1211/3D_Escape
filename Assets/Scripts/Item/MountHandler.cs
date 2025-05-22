using Stat;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MountHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject originEntity;
    
    [SerializeField]
    private EquipmentSystem equipmentSystem;
    
    [SerializeField]
    private Image mountBar;
    
    private PlayerController controller;
    private JumpPowerSystem jumpPowerSystem;
    private Rigidbody rb;
    private GameObject mountPrefab;

    private MountItem currentMount;
    private bool isMounted = false;
    private float mountDuration;
    private float maxMountDuration;
    

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        jumpPowerSystem = GetComponent<JumpPowerSystem>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isMounted)
        {
            mountDuration -= Time.deltaTime;
            UpdateItemUI(mountDuration);
            if (mountDuration <= 0)
            {
                Unmount();
            }    
        }
    }

    public void Mount(MountItem item)
    {
        if (isMounted) Unmount();
        isMounted = true;
        currentMount = item;

        mountDuration = item.itemDuration;
        maxMountDuration = mountDuration;

        mountPrefab = Instantiate(item.mountPrefab);
        mountPrefab.transform.SetParent(transform);
        mountPrefab.transform.position = transform.position;
        mountPrefab.transform.rotation = new Quaternion(0,0,0,0);
        originEntity.transform.position += new Vector3(0,1.15f,0);
        
        equipmentSystem.AddEquippedItem(item);

        controller.moveSpeed = item.mountMoveSpeed;
        jumpPowerSystem.SetChargeSpeed(item.jumpChargeSpeed);
    }

    public void Unmount()
    {
        if (!isMounted) return;
        isMounted = false;

        transform.SetParent(null);

        if (mountPrefab != null)
        {
            Destroy(mountPrefab);
        }
        originEntity.transform.localPosition = Vector3.zero;
        equipmentSystem.RemoveEquippedItem(currentMount);
        
        currentMount = null;
        controller.ResetMoveSpeed();
        jumpPowerSystem.ResetChargeSpeed();
        
    }


    public void OnMountAction(GameObject user)
    {
        rb.AddForce(Vector3.up * currentMount.upwardJumpForce, ForceMode.Impulse);
        Unmount();
    }
    
    private void UpdateItemUI(float currentItemDuration)
    {
        mountBar.fillAmount = currentItemDuration/ maxMountDuration;
    }
}