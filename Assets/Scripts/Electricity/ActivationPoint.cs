using UnityEngine;

namespace Electricity
{
    public class ActivationPoint : MonoBehaviour
    {
        public SpriteRenderer image;
        public float SelectSize = 1.5f;

        public float MaxColliderSize = 5f;
        public AnimationCurve CollisionDistance;

        public ElectricDevice[] devices;

        SphereCollider coll;

        private void Awake()
        {
            coll = GetComponent<SphereCollider>();
        }

        private void Update()
        {
            if (PlayerReference.singleton == null) return;
            coll.radius = CollisionDistance.Evaluate(Vector3.Distance(PlayerReference.singleton.transform.position, transform.position) / MaxColliderSize) * MaxColliderSize;
        }

        public void Select(bool state)
        {
            image.transform.localScale = Vector3.one * (state ? SelectSize : 1f);
        }

        public void Activate(bool state)
        {
            Debug.Log(state);
            for (int i = 0; i < devices.Length; i++)
                devices[i].isOn = state;
        }
    }
}