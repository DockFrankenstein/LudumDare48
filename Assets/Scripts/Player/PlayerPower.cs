using UnityEngine;

namespace Electricity
{
    public class PlayerPower : MonoBehaviour
    {
        public LineRenderer Line;

        public Transform CastPoint;

        public LayerMask PointMask;
        public float Range = 32f;

        private ActivationPoint currentPoint;
        public bool Active;

        private void FixedUpdate()
        {
            Debug.Log(Physics.Raycast(CastPoint.position, CastPoint.TransformDirection(Vector3.forward), out RaycastHit test, Range, PointMask) +
                " " + test.transform?.name);
            if (Physics.Raycast(CastPoint.position, CastPoint.TransformDirection(Vector3.forward), out RaycastHit hit, Range, PointMask))
            {
                Debug.DrawRay(CastPoint.position, CastPoint.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                currentPoint = hit.transform.GetComponent<ActivationPoint>();
                currentPoint?.Select(true);
                return;
            }
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