using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public float speed;
    public float displacement;
    public float depthBeforeSub;
    public Rigidbody2D rb;

   private void FixedUpdate()
    {
        float displacementMultiplier = Mathf.Clamp01(-transform.position.y / depthBeforeSub) * displacement;
        rb.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y)*displacementMultiplier, 0f), ForceMode2D.Force);
    }
}
