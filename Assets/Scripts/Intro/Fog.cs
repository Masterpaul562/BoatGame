using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public float speed;
    public Camera cam;

    private void Update()
    {
        transform.Translate(Vector2.left*speed);
        if(Vector2.Distance(transform.position, cam.transform.position) > 50f)
        {
            Destroy(this.gameObject);
        }
    }
   
}
