using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBoat : MonoBehaviour
{
    [SerializeField] private LayerMask interactable;

    
    void Update()
    {
       // float vert = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.S))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 10,interactable);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Enter")
                {
                    Enter();
                }
            }
        }
    }
    private void Enter()
    {
        // enter the boat
        // Add enter Boat Function
        Debug.Log("You did it!");
    }
}
