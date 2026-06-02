using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogTrigger : MonoBehaviour
{
    public bool hasFogged;


    public GameObject fogObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && !hasFogged)
        {
            hasFogged = true;
            fogObject.SetActive(true);
        }

    }


}

