using UnityEngine;

namespace ColliderObj.MoveFloor
{
    [CreateAssetMenu(fileName = "MovingFloor", menuName = "Scriptable Objects/Moving Floor")]
    public class MovingScriptableObj : ScriptableObject
    {
       public float moveSpeed;
       public Vector3[] positionRoadList;
    }
}