using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentController : MonoBehaviour
{
    Transform playerTransform;
    Rigidbody rb;
    public float chaseForceMagnitude;
    public float maxVelocityMagnitude;
    public float spotDistance;
    bool playerSpotted;
    public bool chasing;
    public Material disarmedMaterial;
    public LayerMask layer;

    void Start()
    {
        playerTransform = PlayerReference.singleton.transform;
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (playerSpotted && chasing)
            UpdatePosition();
        else if (!playerSpotted && IsTriggered())
            SpotPlayer();
    }
    bool IsTriggered()
    {
        Vector3 distanceVector = playerTransform.position - transform.position;
        Physics.Raycast(
            new Ray(transform.position, distanceVector.normalized), 
            out RaycastHit hit, 
            spotDistance,
            layer);
        
        return distanceVector.magnitude <= spotDistance && 
            hit.collider.gameObject.layer == LayerMask.NameToLayer("Player");
    }
    public void SpotPlayer()
    {
        if (playerSpotted) return;
        rb.velocity = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)); 
        playerSpotted = chasing = true;
    }
    void UpdatePosition()
    {
        Vector3 force = chaseForceMagnitude * (playerTransform.position - transform.position).normalized;
        rb.velocity += force * Time.deltaTime;
        rb.velocity = rb.velocity.normalized * Mathf.Clamp(rb.velocity.magnitude, 0, maxVelocityMagnitude);
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 7 && chasing)
        {
            PlayerReference.singleton.damage.Kill();
            Destroy(gameObject);
        }
        else
        {
            chasing = false;
            GetComponent<MeshRenderer>().material = disarmedMaterial;
            rb.useGravity = true;
        }
    }
}