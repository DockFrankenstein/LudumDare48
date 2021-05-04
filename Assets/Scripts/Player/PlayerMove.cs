using UnityEngine;
using qASIC.InputManagement;

public class PlayerMove : MonoBehaviour
{
    [HideInInspector] public CharacterController charControl;
    float velocity;

    [Header("Gravity")]
    public Transform GroundPoint;
    public float CheckRadious = 0.5f;

    public static bool noclip;

    public LayerMask GroundLayer;
    public float Gravity = 30f;
    public float GroundVelocity = 2f;
    public float JumpHeight = 10f;
    public bool isGround;
    public bool walking;
    public bool running;

    [Header("Walking")]
    public PlatformDetection platformDetection;
    public float Speed = 6f;
    public float RunSpeed = 6f;

    public static bool Active = true;

    [Header("Debug")]
    public bool debug;

    private void Awake()
    {
        charControl = GetComponent<CharacterController>();
        Active = true;
    }

    private void Update()
    {
        if (debug) qASIC.Displayer.InfoDisplayer.DisplayValue("player velocity", velocity.ToString(), "debug");

        if (!Active) return;
        Vector3 path = new Vector3();

        if (CursorManager.GlobalState)
        {
            float x = InputManager.GetAxis("WalkRight", "WalkLeft");
            float z = InputManager.GetAxis("WalkUp", "WalkDown");
            running = InputManager.GetInput("Sprint");
            path = (transform.right * x + transform.forward * z).normalized * (running ? RunSpeed : Speed);
            walking = path == Vector3.zero;
        }

        CalculateGravity();
        path.y = velocity;

        path *= Time.deltaTime;
        charControl.Move(platformDetection.platformMove);
        charControl.Move(path);
    }

    private void CalculateGravity()
    {
        switch (noclip)
        {
            case true:
                velocity = 0f;
                if(InputManager.GetInput("Jump") && CursorManager.GlobalState) velocity += Mathf.Sqrt(JumpHeight * 2f * Gravity);
                if(Input.GetKey(KeyCode.LeftControl)) velocity -= Mathf.Sqrt(JumpHeight * 2f * Gravity);
                break;
            default:
                bool lastGround = isGround;
                isGround = Physics.CheckSphere(GroundPoint.position, CheckRadious, GroundLayer) || platformDetection.isPlatform;

                if (!lastGround && isGround) PlayerReference.singleton?.damage?.HandleVelocity(velocity);

                velocity -= Gravity * Time.deltaTime;
                if (isGround && charControl.isGrounded) velocity = -GroundVelocity;
                if (isGround && platformDetection.isPlatform) velocity = 0f;
                if (isGround && InputManager.GetInput("Jump") && CursorManager.GlobalState) velocity = Mathf.Sqrt(JumpHeight * 2f * Gravity);
                break;
        }
    }
}
