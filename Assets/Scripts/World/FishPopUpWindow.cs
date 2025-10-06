using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPopUpWindow : MonoBehaviour
{
   [SerializeField] private EnterFishing enterFishing;
    [SerializeField] private GameObject window;

    private void Awake()
    {
        window = transform.GetChild(0).gameObject;
       
    }
   private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
           
            window.SetActive(true);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            window.SetActive(false);

        }
    }
    private void Update()
    {
        if (enterFishing.isFishing)
        {
            window.SetActive(false);
        }
    }
}
