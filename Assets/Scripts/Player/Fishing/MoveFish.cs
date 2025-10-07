using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFish : MonoBehaviour
{
    [SerializeField] private LayerMask interactable;
    [SerializeField] private GameObject window;
    [SerializeField] private int amountToMove;
    private bool displayed;

    private void Awake()
    {
        displayed = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        float vert = Input.GetAxisRaw("Vertical");
       // Debug.Log
        if (vert < 0)
        {
            if (!displayed)
            {
                window.SetActive(true);
                displayed = true;
            }else
            {
                // CHANGE TO INCREMENT ONCE PER PRESS
                amountToMove++;
            }
        }
        window.GetComponent<WindowDisplayText>().textToDisplay = "Move amount: " + amountToMove;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)&&displayed)
        {
            Debug.Log("yay");
            displayed = false;
            window.SetActive(false);
            amountToMove = 0;
        }
    }

    // can be reworked to have you get lock in place and only way to exit menu is to press Z so below code might be unneccasary
    private void OnTriggerExit2D(Collider2D other)
    {
        displayed = false;
        window.SetActive(false);
        amountToMove = 0;
    }


   
}
