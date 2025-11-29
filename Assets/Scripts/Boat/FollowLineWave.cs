using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLineWave : MonoBehaviour
{
    [SerializeField] MixedWaveWithLine line;
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,new Vector3 (transform.position.x,line.closestPoint+0.5f,transform.position.z),Time.deltaTime/2);
        float highPot = Vector3.Distance(line.line.GetPosition(line.closestIndex), line.line.GetPosition(line.closestIndex + 1));
        float adj = (line.closestPoint - line.line.GetPosition(line.closestIndex + 1).y);
        float rotTarget = Mathf.Rad2Deg*Mathf.Acos(adj/highPot);
        //Debug.Log(rotTarget);

        //transform.Rotate(0,0,Mathf.Asin(Mathf.Abs(pos.y - back.y) / Mathf.Abs(pos.x - back.x)));
    }
}
