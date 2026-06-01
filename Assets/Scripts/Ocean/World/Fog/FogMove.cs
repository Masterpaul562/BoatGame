using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogMove : MonoBehaviour
{
    public float speed;
    public Camera cam;
    void Update()
    {
        transform.position = new Vector2(transform.position.x - 0.001f * speed, transform.position.y);
        Vector3 point = cam.WorldToViewportPoint(transform.position);
        if (point.x < -2)
        {
            Destroy(this.gameObject);
        }
    }
}
