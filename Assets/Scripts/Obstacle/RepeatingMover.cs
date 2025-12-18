using System;
using UnityEngine;

public class RepeatingMover : MonoBehaviour
{
    [System.Serializable]
    public struct MoveInfo
    {
        public Vector3 localDirection;
        public float distance;
        public float speed;
        public float waitTime;
    }

    [SerializeField] private MoveInfo moveInfo;
    
    private Vector3 startLocalPosition;
    private Vector3 endLocalPosition;

    private bool targetToagle = true; // true : endLocalPosition, false : startLocalPosition 
    private float waitTime;

    private void Start()
    {
        startLocalPosition = transform.localPosition;
        endLocalPosition = startLocalPosition + moveInfo.localDirection * moveInfo.distance;
    }

    private void Update()
    {
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            return;
        }

        Vector3 target = targetToagle ? endLocalPosition : startLocalPosition;
        transform.localPosition =
            Vector3.MoveTowards(
                transform.localPosition, target,
                moveInfo.speed * Time.deltaTime);

        if (Vector3.Distance(transform.localPosition, target) <= 0.01f)
        {
            targetToagle = !targetToagle;
            waitTime = moveInfo.waitTime;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Vector3 currentStart = Application.isPlaying ? startLocalPosition : transform.localPosition;
        Vector3 currentEnd = currentStart + moveInfo.localDirection.normalized * moveInfo.distance;

        Vector3 worldStart = transform.parent != null ? transform.parent.TransformPoint(currentStart) : currentStart;
        Vector3 worldEnd = transform.parent != null ? transform.parent.TransformPoint(currentEnd) : currentEnd;

        Gizmos.DrawLine(worldStart, worldEnd);
        Gizmos.DrawSphere(worldStart, 0.1f);
        Gizmos.DrawSphere(worldEnd, 0.1f);
    }
}
