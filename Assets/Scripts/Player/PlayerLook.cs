using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform Axis;

    public float MouseSensitivity = 1f;

    private void Update()
    {
        if (!Active || Time.timeScale == 0f) return;
        Rotate();
    }

    [HideInInspector]
    public Vector2 rotation = new Vector2();

    public static bool Active = true;

    private void Awake()
    {
        CursorManager.ChangeState("global", true);

        if (Axis == null)
        {
            qASIC.qDebug.LogError("Player Y Axis has not been assigned!");
            return;
        }

        rotation = new Vector2(transform.eulerAngles.y, transform.eulerAngles.x);
    }

    private void Rotate()
    {
        rotation.x = rotation.x + Input.GetAxis("Mouse X") * MouseSensitivity;
        rotation.y = Mathf.Clamp(rotation.y - Input.GetAxis("Mouse Y") * MouseSensitivity, -90f, 90f);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotation.x, transform.eulerAngles.z);
        if (Axis != null) Axis.eulerAngles = new Vector3(rotation.y, Axis.eulerAngles.y, Axis.eulerAngles.z);
    }
}
