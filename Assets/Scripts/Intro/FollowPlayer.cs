using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform leftBound;
    public Transform rightBound;
    public Transform target;
    private Vector3 targetPosition;
    [Header("Settings")]
    public float maxSpeed;
    public float xOffset;
    public float speed;
    public float maxTargetSpeed;
    public float targetMoveSpeed;

    private void Start()
    {
        speed = maxSpeed;
        targetMoveSpeed = maxTargetSpeed;   
    }
    

    private void Update()
    {
        targetMoveSpeed = maxTargetSpeed * Vector2.Distance(target.position, player.transform.position);
       if (player.GetComponent<IntroMovement>().isFacingRight)
       {
            if (target.position.x > player.transform.position.x - xOffset)
            {
                targetPosition = targetPosition;
            }
            else
            {
                targetPosition = new Vector3(player.transform.position.x - xOffset, target.position.y, target.position.z);
            }
       }
        else
        {
            if (target.position.x < player.transform.position.x + xOffset)
            {
                targetPosition = targetPosition;
            }
            else
            {
                targetPosition = new Vector3(player.transform.position.x + xOffset, target.position.y, target.position.z);
            }
       }

        target.position = Vector3.MoveTowards(target.position, targetPosition, Time.deltaTime * targetMoveSpeed);

        if(target.transform.position.x > leftBound.position.x && target.transform.position.x < rightBound.position.x)
        {
            Vector3 position = transform.position;
            position.x = Mathf.MoveTowards(position.x, target.transform.position.x, Time.deltaTime* speed);    
            transform.position = position;
            speed = maxSpeed * Vector2.Distance(transform.position, target.transform.position); 
        }
        else
        {
            transform.position = transform.position;
        }
    }


}
