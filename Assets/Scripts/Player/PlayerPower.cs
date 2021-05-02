using UnityEngine;

namespace Electricity
{
    public class PlayerPower : MonoBehaviour
    {
        public LineRenderer Line;

        public Transform CastPoint;

        public LayerMask DetectionMask;
        public LayerMask PointMask;
        public LayerMask OutOfRangeMask;
        public float Range = 32f;

        private ActivationPoint currentPoint;
        public bool Active;

        bool waitForUp = false;

        private void FixedUpdate()
        {
            if (Physics.Raycast(CastPoint.position, CastPoint.TransformDirection(Vector3.forward), out RaycastHit hit, Range, DetectionMask))
            {
                Debug.DrawRay(CastPoint.position, CastPoint.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if (((1 << hit.transform.gameObject.layer) & PointMask) == 0 || Active)
                {
                    RemovePoint();
                    return;
                }

                currentPoint = hit.transform.GetComponent<ActivationPoint>();
                currentPoint?.Select(true);
                return;
            }
            Debug.DrawRay(CastPoint.position, CastPoint.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            RemovePoint();
        }

        void RemovePoint()
        {
            currentPoint?.Select(false);
            if (!Active)
            {
                currentPoint?.Activate(false);
                currentPoint = null;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0)) waitForUp = false;
            if (waitForUp) return;

            Active = Input.GetMouseButton(0) && currentPoint != null;

            if (!IsStillInRange())
                Active = false;

            currentPoint?.Activate(Active);
            Line.gameObject.SetActive(Active);
            if (Active && currentPoint != null)
            {
                Line.SetPosition(Line.positionCount - 1, currentPoint.transform.position);
                Line.SetPosition(0, Line.transform.position);
            }
        }

        bool IsStillInRange()
        {
            if (!Active) return false;
            Debug.DrawLine(Line.transform.position, currentPoint.transform.position, Color.red);
            bool hits = Physics.Linecast(Line.transform.position, currentPoint.transform.position, OutOfRangeMask);

            bool isInRange = Vector3.Distance(currentPoint.transform.position, CastPoint.position) <= Range;

            bool isActive = isInRange && !hits;
            if(!isActive) waitForUp = true;
            return isActive;
        }
    }
}