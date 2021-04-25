using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGroup : MonoBehaviour
{
    Transform playerTransform;
    public float spotDistance;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        Vector3 distanceVector = playerTransform.position - transform.position;
        Physics.Raycast(new Ray(transform.position, distanceVector.normalized), out RaycastHit hit, spotDistance, ~LayerMask.NameToLayer("Particles"));
        if (distanceVector.magnitude <= spotDistance && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            foreach (Particle particle in GetComponentsInChildren<Particle>())
                particle.SpotPlayer();
            Destroy(this);
        }
    }
}
