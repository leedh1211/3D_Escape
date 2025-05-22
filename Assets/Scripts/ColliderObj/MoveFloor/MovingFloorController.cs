using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace ColliderObj.MoveFloor
{
    public class MovingFloorController : MonoBehaviour
    {
        [SerializeField] MovingScriptableObj mountData;
        
        private void Start()
        {
            StartCoroutine(movingPath());
        }

        private IEnumerator movingPath()
        {
            bool isPlus = true;
            int currentIndex = 0;
            Vector3 startPosition = transform.localPosition;

            while (true)
            {
                // 현재 타겟 위치 계산: 현재 기준 시작 위치 + 상대 오프셋
                Vector3 currentOffset = mountData.positionRoadList[currentIndex];
                Vector3 currentTarget = startPosition + currentOffset;

                Vector3 direction;
                if (currentIndex == 0)
                {
                    direction = (currentTarget - startPosition).normalized;
                }
                else
                {
                    Vector3 previousTarget = startPosition + mountData.positionRoadList[isPlus ? currentIndex - 1 : currentIndex + 1];
                    direction = (currentTarget - previousTarget).normalized;
                }

                // 이동 시작
                while (Vector3.Distance(transform.localPosition, currentTarget) > 0.01f)
                {
                    float step = mountData.moveSpeed * Time.deltaTime;
                    Vector3 nextPosition = transform.localPosition + direction * step;

                    if (Vector3.Distance(nextPosition, currentTarget) > Vector3.Distance(transform.localPosition, currentTarget))
                    {
                        nextPosition = currentTarget;
                    }

                    transform.localPosition = nextPosition;
                    yield return null;
                }

                // 방향 반전
                if (isPlus && currentIndex == mountData.positionRoadList.Length - 1)
                {
                    isPlus = false;
                }
                else if (!isPlus && currentIndex == 0)
                {
                    isPlus = true;
                }

                // 인덱스 변경
                if (isPlus)
                {
                    currentIndex += 1;
                }
                else
                {
                    currentIndex -= 1;
                }
            }
        }
        
        // private void OnTriggerEnter(Collider other)
        // {
        //     Debug.Log(other.gameObject.name);
        //     if (other.CompareTag("Player"))
        //     {
        //         other.transform.SetParent(transform);
        //     }
        // }
        //
        // private void OnTriggerExit(Collider other)
        // {
        //     Debug.Log(other.gameObject.name);
        //     if (other.CompareTag("Player"))
        //     {
        //         other.transform.SetParent(null);
        //     }
        // }
    }
}