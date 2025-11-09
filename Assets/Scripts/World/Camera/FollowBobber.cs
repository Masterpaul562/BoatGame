using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBobber : MonoBehaviour
{
    public bool shouldMove;
    private Vector3 ogPosition;
    [SerializeField] private Transform player;
    [SerializeField] private Transform bobber;

    private void Start()
    {
        ogPosition = transform.position;
    }
    public Vector3 Middle(Transform point1, Transform point2)
    {
        Vector3 middle =new Vector3 (point1.position.x - point2.position.x,ogPosition.y,ogPosition.z);
        return middle;
       
    }
    private void moveToMiddle()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, Middle(bobber, player), Time.deltaTime*10);
        transform.position = pos;   
    }
    private void Update()
    {
        
        if (shouldMove)
        {
            moveToMiddle();
        }
    }
}
