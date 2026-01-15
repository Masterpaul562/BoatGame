using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEngine : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject lightBar;
    [SerializeField] private LayerMask interactable;
    [SerializeField] private Color whiteBar;
    [SerializeField] private Color redBar; 
    private HarpoonGun fishing;
    private FishInventory inventory;
    private int amountFed;
    public float powerLevel;
    public float maxPowerLevel;
    public float fishPowerAmount;
    private bool feedCD;
    public bool drainPower = true;
    private bool red = false;

    

    private void Start()
    {
        inventory = player.GetComponent<FishInventory>();
        fishing = player.GetComponent<HarpoonGun>();
        powerLevel = maxPowerLevel;
        lightBar.GetComponent<SpriteRenderer>().color = whiteBar;

        StartCoroutine(DrainPower());
    }


    private void Update()
    {
        float vert = Input.GetAxisRaw("Vertical");
        if(vert<0 && !fishing.isFishing)
        {
            Interact();
        }
        Debug.Log(FindFeedAmount());
        if(red){
            lightBar.GetComponent<SpriteRenderer>().color = redBar;
        } else {
            lightBar.GetComponent<SpriteRenderer>().color = whiteBar;
        }

    }

    private void FeedFish()
    {
        //Add power based on fish before feeding
        //might need a cooldown
        if (!feedCD)
        {
            Debug.Log("Fed");
            feedCD = true;
            for (int i = 1; i <= inventory.fishAmountOutside; i++)
            {
                if (i <= FindFeedAmount())
                {

                    amountFed++;
                    
                }
               
            }
            powerLevel += .32f * amountFed;
            if (powerLevel > maxPowerLevel)
            {
                powerLevel = maxPowerLevel;
            }
            inventory.fishAmountOutside -= amountFed;
            amountFed = 0;
            StartCoroutine(FeedCD());
        }
        
    }
    private void Interact()
    {
        Debug.Log("interact");
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector3.forward, 10, interactable);
        if(hit.collider != null)
        {
            if(hit.collider.gameObject.tag == "Engine" && inventory.fishAmountOutside != 0)
            {
                FeedFish();
            }
        }
    }
    private IEnumerator DrainPower()
    {
        while (drainPower)
        {
            yield return new WaitForSeconds(0.1f);
            powerLevel -= 0.001f;
            if(powerLevel <= 0)
            {
                powerLevel = 0;
            }
            if(powerLevel <= powerLevel/4)
            {
                red=true;
            }else {
                red = false;
            }
            lightBar.transform.localScale = new Vector3(powerLevel, lightBar.transform.localScale.y, lightBar.transform.localScale.z);
        }
    }
    private int FindFeedAmount()
    {
        float neededAmount = maxPowerLevel - powerLevel;
        neededAmount = neededAmount / .32f;
        int returnAmount = Mathf.CeilToInt(neededAmount);
        return returnAmount;    

    }
    private IEnumerator FeedCD()
    {
        yield return new WaitForSeconds(1f);
        feedCD = false;
    }
    private IEnumerator Blink() {
        Color temp = whiteBar;
        temp.a = 0; 
        lightBar.GetComponent<SpriteRenderer>().color = temp;
        yield return new WaitForSeconds (0.1f);
        temp.a = 255;
        lightBar.GetComponent<SpriteRenderer>().color= temp;
    }
}
