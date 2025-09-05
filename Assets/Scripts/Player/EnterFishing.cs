using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterFishing : MonoBehaviour
{
    [SerializeField] private LayerMask interactable;
    [SerializeField] private Rigidbody2D rb;
    public bool isFishing;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // float vert = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.S))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 10, interactable);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Fishing")
                {
                    StartFishing();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) && isFishing)
        {
            ExitFishing();
        }

    }
    private void StartFishing()
    {
       // rb.constraints = RigidbodyConstraints2D.FreezePositionX|RigidbodyContraints.FreezeRotationZ;
        isFishing = true;

        //doo this to fishing menu/game
       //.SetActive(true) 
    }
    private void ExitFishing()
    {
        //rb.constraints = RigidbodyConstraints2D.FreezeRotationZ;
        isFishing = false;
    }

}
