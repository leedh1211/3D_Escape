using UnityEngine;

namespace InterationObject
{
    public class InteractableUIController : MonoBehaviour
    {
        [SerializeField] private GameObject UIobject;
        [SerializeField] private TextMesh messageText;
        [SerializeField] private Vector3 offset = new Vector3(0, 1.5f, 0);

        private void OnEnable()
        {
            InteractableEvents.OnTargetFound += HandleTargetFound;
            InteractableEvents.OnTargetLost += HandleTargetLost;
        }

        private void OnDisable()
        {
            InteractableEvents.OnTargetFound -= HandleTargetFound;
            InteractableEvents.OnTargetLost -= HandleTargetLost;
        }

        private void HandleTargetFound(GameObject target)
        {
            if (UIobject == null)
            {
                Debug.LogWarning("UIobject가 비어 있습니다.");
                return;
            }

            Vector3 worldPosition = target.transform.position + offset;
            UIobject.transform.position = worldPosition;

            UIobject.transform.LookAt(Camera.main.transform);
            UIobject.transform.Rotate(0, 180f, 0);
            
            Show();
        }

        private void HandleTargetLost()
        {
            Hide();
        }

        public void Show()
        {
            UIobject.SetActive(true);
        }

        public void Hide()
        {
            UIobject.SetActive(false);
        }
    }
}