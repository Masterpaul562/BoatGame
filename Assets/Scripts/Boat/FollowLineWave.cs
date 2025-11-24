using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLineWave : MonoBehaviour
{
    [SerializeField] MixedWaveWithLine line;
    void Update()
    {
        transform.position = new Vector3 (transform.position.x,line.closestPoint,transform.position.z);
       // Vector3 pos = line.GetPosition(7);
       // Vector3 back = line.GetPosition(6);

        //transform.Rotate(0,0,Mathf.Asin(Mathf.Abs(pos.y - back.y) / Mathf.Abs(pos.x - back.x)));
    }
}
