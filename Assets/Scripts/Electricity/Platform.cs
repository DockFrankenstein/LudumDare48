using UnityEngine;

public class Platform : ElectricDevice
{
    public bool loop;

    public Transform MoovingPart;
    public Vector3 DeactivatedPosition = Vector3.zero;
    public Vector3 ActivatedPosition = Vector3.up;
    public float speed;
    float time;
    private bool reverse;

    private void Update()
    {
        if (DeactivatedPosition == ActivatedPosition) return;
        if (!isOn && loop) return;
        if (!loop) reverse = !isOn;
        time += Time.deltaTime / Mathf.Abs(Vector3.Distance(ActivatedPosition, DeactivatedPosition)) * (reverse ? -1f : 1f) * speed;
        time = Mathf.Clamp(time, loop ? -1f : 0f, 1f);
        MoovingPart.localPosition = Vector3.Lerp(DeactivatedPosition, ActivatedPosition, Mathf.Abs(time));
        if (Mathf.Abs(time) >= 1f) reverse = !reverse;
    }
}
