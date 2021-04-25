using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform toFollow;
    public Vector3 offset;

    private void Update()
    {
        if (toFollow == null) return;
        transform.position = toFollow.position + offset;
    }
}
