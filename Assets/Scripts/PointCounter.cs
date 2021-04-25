using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCounter : MonoBehaviour
{
    public int points;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
            Destroy(collision.gameObject);
    }
}
