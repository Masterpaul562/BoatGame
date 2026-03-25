using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Arrow : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    private float baseY;
    

    private void Start()
    {
        baseY = Mathf.Abs(arrow.transform.position.y-transform.position.y);   
    }
    private void Update()
    {
        MoveArrow();

    }

    private void MoveArrow()
    {
        
        arrow.transform.position = new Vector3(arrow.transform.position.x,Mathf.PingPong(Time.time/100,0.1f)+(transform.position.y+baseY),arrow.transform.position.y);
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        arrow.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        arrow.SetActive(false);
    }
}
