using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBoat : MonoBehaviour
{
    [SerializeField] private LayerMask interactable;
    [SerializeField] private GameObject boatCollider;
    [SerializeField] private GameObject boatInside;
    [SerializeField] private GameObject boatInsideCollider;
    [SerializeField] private Transform enterLocation;
    [SerializeField] private Transform exitLocation;
    [SerializeField] private ZoomCamera camZoom;
    
    void Start()
    {
        camZoom = GetComponent<ZoomCamera>();
    }


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
        if (Input.GetKeyDown(KeyCode.W))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 10, interactable);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Exit")
                {
                    Exit();
                }
            }
        }
    }
    private void Enter()
    {
        StopAllCoroutines();
        transform.position = enterLocation.position;
        boatCollider.SetActive(false);
        boatInside.SetActive(true);
        boatInsideCollider.SetActive(true);
       
        StartCoroutine(camZoom.ZoomCam("1"));
        
    }
    private void Exit()
    {
        StopAllCoroutines();
        transform.position = exitLocation.position;
        boatCollider.SetActive(true);
        boatInside.SetActive(false);
        boatInsideCollider.SetActive(false);
        
        StartCoroutine(camZoom.ZoomCam("0"));
        
    }
   
}
