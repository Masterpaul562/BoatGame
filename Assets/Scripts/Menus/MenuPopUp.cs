using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPopUp : MonoBehaviour
{
   
   private void OnTriggerEnter2D(Collider2D other){
    if(other.gameObject.tag == "Player"){
        transform.GetChild(0).gameObject.SetActive(true);
    }
   }
    private void OnTriggerExit2D(Collider2D other){
    if(other.gameObject.tag == "Player"){
         transform.GetChild(0).gameObject.SetActive(false);
    }
   }
}
