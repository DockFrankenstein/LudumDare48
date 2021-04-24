using UnityEngine;
using qASIC.InputManagement;

public class PlayerMove : MonoBehaviour
{
    CharacterController charControl;
    float velocity;

    [Header("Gravity")]
    public Transform GroundPoint;
    public Vector3 CheckRadious = Vector3.one;

    public LayerMask GroundLayer;
    public float Gravity = 30f;
    public float GroundVelocity = 2f;
    public float JumpHeight = 10f;
    bool isGround;

    [Header("Walking")]
    public PlatformDetection platformDetection;
    public float Speed = 6f;
    public float RunSpeed = 6f;

    private void Awake()
    {
        charControl = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float x = InputManager.GetAxis("WalkRight", "WalkLeft");
        float z = InputManager.GetAxis("WalkUp", "WalkDown");
        Vector3 path = (transform.right * x + transform.forward * z).normalized * (InputManager.GetInput("Sprint") ? RunSpeed : Speed);

        CalculateGravity();
        path.y = velocity;

        path *= Time.deltaTime;
        path += platformDetection.platformMove;
        charControl.Move(path);
    }

    private void CalculateGravity()
    {
        isGround = Physics.CheckBox(GroundPoint.position, CheckRadious, Quaternion.Euler(0f, 0f, 0f), GroundLayer);
        velocity -= Gravity * Time.deltaTime;
        if (isGround) velocity = -GroundVelocity;
        if (isGround && InputManager.GetInput("Jump")) velocity = Mathf.Sqrt(JumpHeight * 2f * Gravity);
    }
}
