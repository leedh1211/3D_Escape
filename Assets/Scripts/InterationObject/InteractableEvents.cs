using System;
using UnityEngine;

namespace InterationObject
{
    public static class InteractableEvents
    {
        public static event Action<GameObject> OnTargetFound;
        public static event Action OnTargetLost;
        public static void RaiseTargetFound(GameObject target)
        {
            OnTargetFound?.Invoke(target);
        }

        public static void RaiseTargetLost()
        {
            OnTargetLost?.Invoke();
        }
    }
}