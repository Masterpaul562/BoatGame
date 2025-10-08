using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPopUpWindow : MonoBehaviour
{
    [SerializeField] private EnterFishing enterFishing;
    [SerializeField] private GameObject window;
    [SerializeField] private FishInventory fishAmount;
    [SerializeField] private MoveFish moveFish;
    public bool shouldDisplay;

    private void Awake()
    {
        moveFish = GetComponent<MoveFish>();
        window = transform.GetChild(0).gameObject;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player" && fishAmount.fishAmountOutside > 0)
        {

            window.SetActive(true);

        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (moveFish.displayed == false)
        {
            window.GetComponent<WindowDisplayText>().textToDisplay = "Fish Pile: " + fishAmount.fishAmountOutside;
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
