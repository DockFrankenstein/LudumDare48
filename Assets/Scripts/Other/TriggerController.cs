using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public class CollisionEvent : UnityEvent<Collision> { }
    public class ColliderEvent : UnityEvent<Collider> { }

    public CollisionEvent CollisionEnter = new CollisionEvent();
    public CollisionEvent CollisionStay = new CollisionEvent();
    public CollisionEvent CollisionExit = new CollisionEvent();

    public ColliderEvent ColliderEnter = new ColliderEvent();
    public ColliderEvent ColliderStay = new ColliderEvent();
    public ColliderEvent ColliderExit = new ColliderEvent();

    private void OnCollisionEnter(Collision collision) => CollisionEnter.Invoke(collision);
    private void OnCollisionStay(Collision collision) => CollisionStay.Invoke(collision);
    private void OnCollisionExit(Collision collision) => CollisionExit.Invoke(collision);
    private void OnTriggerEnter(Collider other) => ColliderEnter.Invoke(other);
    private void OnTriggerStay(Collider other) => ColliderStay.Invoke(other);
    private void OnTriggerExit(Collider other) => ColliderExit.Invoke(other);
}
