using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public int index;


    private void OnTriggerEnter2D(Collider2D other)
    { 
        if(other.tag == "Player")
        {
           
        }
    }
}
