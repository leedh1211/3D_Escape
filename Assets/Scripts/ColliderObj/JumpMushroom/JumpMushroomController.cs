using Unity.VisualScripting;
using UnityEngine;
using Utill;

namespace ColliderObj.JumpMushroom
{
    public class JumpMushroomController : MonoBehaviour
    {
        [SerializeField] float jumpForce = 300f;
        private GameObject JumpMushroom;
        private GameObject _player;
        
        private void OnCollisionEnter(Collision collision)
        {
            _player = collision.gameObject;
            if (_player.layer == LayerMask.NameToLayer("Player"))
            {
                Rigidbody rb = _player.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 jumpDirection = Vector3.up;
                    rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
                }
            }
        }
    }
}