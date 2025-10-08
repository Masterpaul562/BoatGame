using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFish : MonoBehaviour
{
    [SerializeField] private LayerMask interactable;
    [SerializeField] private GameObject window;
    [SerializeField] private FishInventory inventory;
    public bool displayed;
    bool canMove = false;

    private void Awake()
    {
        displayed = false;
        window = transform.GetChild(0).gameObject;
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        float vert = Input.GetAxisRaw("Vertical");
        if (vert == 0)
        {
            canMove = true;
        }

        // Debug.Log
        if (vert < 0)
        {
            if (!displayed)
            {
                canMove = false;
                displayed = true;
            }
        }
        if (displayed)
        {

            if (vert < 0&& canMove)
            {
                FishMove();
            }
            window.GetComponent<WindowDisplayText>().textToDisplay = "Move Fish?";           
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && displayed)
        {
            //  Debug.Log("yay");
            displayed = false;


        }
    }

    // can be reworked to have you get lock in place and only way to exit menu is to press Z so below code might be unneccasary
    private void OnTriggerExit2D(Collider2D other)
    {
        displayed = false;
        window.SetActive(false);

    }
    private void FishMove()
    {
        inventory.fishAmountInside += inventory.fishAmountOutside;
        inventory.fishAmountOutside = 0;
        window.SetActive(false );


    }


}
