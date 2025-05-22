using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Mount Item")]
public class MountItem : Item,  ISkillUsable
{
    public float mountMoveSpeed;
    public float jumpChargeSpeed;
    public float upwardJumpForce;
    public float itemDuration;
    public GameObject mountPrefab;
    public MountHandler handler;

    public override void Use(GameObject user)
    {
        handler = user.GetComponent<MountHandler>();
        if (handler != null)
        {
            handler.Mount(this);
        }
    }
    
    public void UseSkill(GameObject user)
    {
        handler.OnMountAction(user);
    }
}