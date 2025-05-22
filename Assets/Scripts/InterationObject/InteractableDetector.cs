using UnityEngine;
using System.Collections;

namespace InterationObject
{
    public class InteractableDetector : MonoBehaviour
    {
        [SerializeField] private GameObject Player;
        [SerializeField] private LayerMask interactableLayer;

        public GameObject CurrentTarget { get; private set; }

        private void Start()
        {
            StartCoroutine(CheckInteractionObj());
        }

        IEnumerator CheckInteractionObj()
        {
            while (true)
            {
                GameObject target = GetRaycastInteractable();

                if (target != null && target != CurrentTarget)
                {
                    InteractableEvents.RaiseTargetFound(target);
                }
                else if (target == null && CurrentTarget != null)
                {
                    InteractableEvents.RaiseTargetLost();
                }

                CurrentTarget = target;
                yield return new WaitForSeconds(0.1f);
            }
        }

        private GameObject GetRaycastInteractable()
        {
            Ray ray = new Ray(Player.transform.position + Vector3.up * 0.5f, Player.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 2f, interactableLayer))
            {
                return hit.collider.gameObject;
            }
            return null;
        }
    }
}