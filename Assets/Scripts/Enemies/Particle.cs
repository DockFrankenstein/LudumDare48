using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    Transform playerTransform;
    public float chaseForceMagnitude;
    public float maxVelocityMagnitude;
    public bool chasing;
    Vector3 velocity;
    Rigidbody rigidbody;
    public Material disarmedMaterial;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody>();
        chasing = true;
    }

    void Update()
    {
        if(chasing)
            UpdatePosition();

    }
    void UpdatePosition()
    {
        Vector3 force = chaseForceMagnitude * (playerTransform.position - transform.position).normalized;
        rigidbody.velocity += force * Time.deltaTime;
        rigidbody.velocity = rigidbody.velocity.normalized * Mathf.Clamp(rigidbody.velocity.magnitude, 0, maxVelocityMagnitude);
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
            if (Random.Range(0, 10) == 2)
            {
                DisarmParticle();
                rigidbody.useGravity = true;
            }
        }

    }
    void DisarmParticle()
    {
        chasing = false;
        GetComponent<MeshRenderer>().material = disarmedMaterial;
    }
}