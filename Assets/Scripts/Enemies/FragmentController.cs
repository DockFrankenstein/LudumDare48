using UnityEngine;

public class FragmentController : MonoBehaviour
{
    public static int AILevel = 2;
    public static bool showDebugValues = false;

    public enum FragmentState { idle, chasing, discharged };
    public FragmentState state = FragmentState.idle;

    Transform playerTransform;
    Rigidbody rb;
    public float chaseForceMagnitude;
    public float maxVelocityMagnitude;
    public float spotDistance;

    [Space]
    public Vector3 velocityMultiplayer = Vector3.one;
    public TriggerController trigger;
    public Vector3[] path;
    public float minPathSwitchDistance;
    int pathIndex;
    bool playerInCollider;

    bool usingPaths;

    Vector3 startPosition;
    Vector3 target;

    public UnityEngine.Events.UnityEvent OnDischarge = new UnityEngine.Events.UnityEvent();

    public LayerMask layer;

    [Header("Debug")]
    public TMPro.TextMeshPro debugText;

    private void Update()
    {
        if (debugText == null) return;
        if (showDebugValues != debugText.gameObject.activeSelf) debugText.gameObject.SetActive(showDebugValues);
        if (!showDebugValues) return;

        debugText.text = $"Current state: {state}\n" +
            $"Target: {(usingPaths ? $"path {pathIndex}" : "Player")}\n" +
            $"Distance: {Vector3.Distance(transform.position, target)}";
    }

    private void Awake()
    {
        playerInCollider = trigger == null;
        trigger?.ColliderEnter.AddListener((Collider collider) => HandleCollider(collider, true));
        trigger?.ColliderExit.AddListener((Collider collider) => HandleCollider(collider, false));
    }

    void HandleCollider(Collider collider, bool state)
    {
        if (collider.tag != "Player") return;
        playerInCollider = state;
    }

    void Start()
    {
        playerTransform = PlayerReference.singleton.transform;
        rb = GetComponent<Rigidbody>();
        PointCounter.AddMaxPoint();
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (AILevel == 0) return;

        if (state == FragmentState.chasing)
            UpdatePosition();
        else if (state == FragmentState.idle && checkTriggered())
            SpotPlayer();
    }

    bool checkTriggered()
    {
        Vector3 distanceVector = playerTransform.position - transform.position;
        bool hits = raycast();

        return hits && distanceVector.magnitude <= spotDistance && playerInCollider;
    }

    bool raycast()
    {
        Vector3 distanceVector = playerTransform.position - transform.position;
        bool hits = Physics.Raycast(new Ray(transform.position, distanceVector.normalized), out RaycastHit hit, spotDistance, layer);
        return hits && hit.transform.CompareTag("Player");
    }

    public void SpotPlayer()
    {
        if (state == FragmentState.chasing) return;
        rb.velocity = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        state = FragmentState.chasing;
    }

    void UpdatePosition()
    {
        target = GetTarget();

        Vector3 force = chaseForceMagnitude * (target - transform.position).normalized;
        force.x *= velocityMultiplayer.x;
        force.y *= velocityMultiplayer.y;
        force.z *= velocityMultiplayer.z;

        rb.velocity += force * Time.deltaTime;
        rb.velocity = rb.velocity.normalized * Mathf.Clamp(rb.velocity.magnitude, 0, maxVelocityMagnitude);
    }

    Vector3 GetTarget()
    {
        if (raycast() || pathIndex >= path.Length || AILevel == 1)
        {
            usingPaths = false;
            return playerTransform.position;
        }

        for (; pathIndex < path.Length; pathIndex++)
        {
            Vector3 pathWorldPosition = startPosition + path[pathIndex];
            Physics.Raycast(new Ray(transform.position, path[pathIndex]), float.MaxValue, layer);
            Debug.DrawLine(transform.position, pathWorldPosition, Color.red);

            if (Vector3.Distance(transform.position, pathWorldPosition) >= minPathSwitchDistance)
            {
                usingPaths = true;
                return pathWorldPosition;
            }
        }
        return playerTransform.position;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (state != FragmentState.chasing) return;

        if (col.gameObject.layer == 7)
        {
            if (AILevel != 0) PlayerReference.singleton.damage.Kill();
            Destroy(gameObject);
            return;
        }

        state = FragmentState.discharged;
        OnDischarge.Invoke();
        rb.useGravity = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(target, 1f);
    }

    [ExecuteInEditMode]
    private void OnDrawGizmosSelected()
    {
        if (!Application.isEditor) return;
        for (int i = 0; i < path.Length; i++)
        {
            Gizmos.color = i == pathIndex ? Color.red : Color.blue;
            Gizmos.DrawSphere((Application.isPlaying ? startPosition : transform.position) + path[i], minPathSwitchDistance);
        }
    }
}