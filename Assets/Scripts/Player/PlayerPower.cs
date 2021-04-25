using UnityEngine;

namespace Electricity
{
    public class PlayerPower : MonoBehaviour
    {
        public LineRenderer Line;

        public Transform CastPoint;

        public LayerMask DetectionMask;
        public LayerMask PointMask;
        public float Range = 32f;

        private ActivationPoint currentPoint;
        public bool Active;

        private void FixedUpdate()
        {
            if (Physics.Raycast(CastPoint.position, CastPoint.TransformDirection(Vector3.forward), out RaycastHit hit, Range, DetectionMask))
            {
                Debug.Log(((1 << hit.transform.gameObject.layer) & PointMask));
                if (((1 << hit.transform.gameObject.layer) & PointMask) == 0)
                {
                    RemovePoint();
                    return;
                }

                Debug.DrawRay(CastPoint.position, CastPoint.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                currentPoint = hit.transform.GetComponent<ActivationPoint>();
                currentPoint?.Select(true);
                return;
            }
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
            Active = Input.GetMouseButton(0) && currentPoint != null;

            if (Active && Vector3.Distance(currentPoint.transform.position, CastPoint.position) > Range) Active = false;

            currentPoint?.Activate(Active);
            Line.gameObject.SetActive(Active);
            if (Active && currentPoint != null)
            {
                Line.SetPosition(Line.positionCount - 1, currentPoint.transform.position);
                Line.SetPosition(0, Line.transform.position);
            }
        }
    }
}