using UnityEngine;

public class Platform : ElectricDevice
{
    public bool loop;

    public PlatformBase Base;
    public Vector3 DeactivatedPosition = Vector3.zero;
    public Vector3 ActivatedPosition = Vector3.up;
    public float speed;
    float time;
    private bool reverse;

    private void Update()
    {
        if (Base == null) return;
        if (DeactivatedPosition == ActivatedPosition || !isOn && loop)
        {
            Base.direction = Vector3.zero;
            return;
        }

        if (!loop) reverse = !isOn;
        time += Time.deltaTime / Mathf.Abs(Vector3.Distance(ActivatedPosition, DeactivatedPosition)) * (reverse ? -1f : 1f) * speed;
        time = Mathf.Clamp(time, loop ? -1f : 0f, 1f);
        Vector3 move = Vector3.Lerp(DeactivatedPosition, ActivatedPosition, Mathf.Abs(time));
        Base.direction = (move - Base.transform.localPosition).normalized * speed;
        Base.transform.localPosition = move;
        if (Mathf.Abs(time) >= 1f) reverse = !reverse;
    }
}
