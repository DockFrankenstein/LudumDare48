using UnityEngine;

namespace Electricity
{
    public class PlayerPower : MonoBehaviour
    {
        public Transform CastPoint;

        public LayerMask PointMask;
        public float Range = 32f;

        private ActivationPoint currentPoint;
        public bool Active;

        private void FixedUpdate()
        {
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
        }
    }
}