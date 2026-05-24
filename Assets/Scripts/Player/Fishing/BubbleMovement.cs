using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{


    private void Start()
    {
        StartCoroutine(DestroySelf());
    }

    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime); 
       
    }
    private IEnumerator DestroySelf()
    { 
        yield return new WaitForSeconds(2f);
            Destroy(this.gameObject);
        
    }
}
