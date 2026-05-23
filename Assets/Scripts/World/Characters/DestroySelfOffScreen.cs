using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfOffScreen : MonoBehaviour
{
    public Camera cam;

    void Update()
    {
        if (DestroyCheck())
        {
            Destroy(this.gameObject);
        }  
    }

   private bool DestroyCheck()
    {
        Vector3 point = cam.WorldToViewportPoint(transform.position);
        if (point.x < -0.1f)
        {
            return true;
        }
        else { return false; }
    }
}
