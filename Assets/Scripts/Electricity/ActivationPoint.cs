using UnityEngine;

namespace Electricity
{
    public class ActivationPoint : MonoBehaviour
    {
        public SpriteRenderer image;

        public float SelectSize = 1.5f;

        public ElectricDevice[] devices;

        public void Select(bool state)
        {
            image.transform.localScale = Vector3.one * (state ? SelectSize : 1f);
        }

        public void Activate(bool state)
        {
            for (int i = 0; i < devices.Length; i++)
                devices[i].isOn = state;
        }
    }
}