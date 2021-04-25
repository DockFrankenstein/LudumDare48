using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    Transform playerTransform;
    Rigidbody rb;
    public float chaseForceMagnitude;
    public float maxVelocityMagnitude;
    public float spotDistance;
    bool playerSpotted;
    bool chasing;
    public Material disarmedMaterial;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if(playerSpotted && chasing)
            UpdatePosition();
        else if(!playerSpotted)
        {
            playerSpotted = chasing = HasSpottedPlayer();
        }
    }
    bool HasSpottedPlayer()
    {
        Vector3 distanceVector = playerTransform.position - transform.position;
        Physics.Raycast(new Ray(transform.position, distanceVector.normalized), out RaycastHit hit, spotDistance, ~LayerMask.NameToLayer("Particles"));
        return distanceVector.magnitude <= spotDistance && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player");
    }
    void UpdatePosition()
    {
        Vector3 force = chaseForceMagnitude * (playerTransform.position - transform.position).normalized;
        rb.velocity += force * Time.deltaTime;
        rb.velocity = rb.velocity.normalized * Mathf.Clamp(rb.velocity.magnitude, 0, maxVelocityMagnitude);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
            Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 7)
        { Destroy(gameObject); }
        else
        {
            DisarmParticle();
            rb.useGravity = true;
        }
    }
    void DisarmParticle()
    {
        chasing = false;
        GetComponent<MeshRenderer>().material = disarmedMaterial;
    }
}