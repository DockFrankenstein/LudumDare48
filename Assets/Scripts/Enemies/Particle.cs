using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    Transform playerTransform;
    Rigidbody rb;
    public float chaseForceMagnitude;
    public float maxVelocityMagnitude;
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
    }
    public void SpotPlayer()
    {
        rb.velocity = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)); 
        playerSpotted = chasing = true;
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
            chasing = false;
            GetComponent<MeshRenderer>().material = disarmedMaterial;
            rb.useGravity = true;
        }
    }
}