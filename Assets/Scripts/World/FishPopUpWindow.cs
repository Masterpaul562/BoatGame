using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPopUpWindow : MonoBehaviour
{
    [SerializeField] private EnterFishing enterFishing;
    [SerializeField] private GameObject window;
    [SerializeField] private FishInventory fishAmount;
    public bool shouldDisplay;

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

    private void OnTriggerStay2D(Collider2D collider)
    {
       // if (shouldDisplay)
       // {
            window.GetComponent<WindowDisplayText>().textToDisplay = "Fish Pile: " + fishAmount.fishAmountOutside;
      //  }
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
