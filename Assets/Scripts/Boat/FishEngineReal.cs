using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEngineReal : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask interactable;
    [SerializeField] private GameObject lightBar;
    private HarpoonGun fishing;
    private FishInventory inventory;
    [SerializeField] private float powerLevel;
    [SerializeField] private int powerStage = 3;
    [SerializeField] private int powerSet =0;
    [SerializeField] private float maxScale;
    [SerializeField] private bool shouldDrain = true;
    [SerializeField] private bool canFeed = true;
    public float drainSpeed;
    private bool feedCD = false;



    private void Start()
    {
        inventory = player.GetComponent<FishInventory>();
        fishing = player.GetComponent<HarpoonGun>();
        maxScale = lightBar.transform.localScale.x;
        powerLevel = 100;
        SetLightBar();
    }
    private void Update()
    {

        float vert = Input.GetAxisRaw("Vertical");
        if (vert < 0 && !fishing.isFishing)
        {
            Interact();
        }


        if (shouldDrain)
        {
            DrainPower();
        }
    }
    private void DrainPower()
    {
        powerLevel = Mathf.MoveTowards(powerLevel, 0, Time.deltaTime * drainSpeed);
        if (powerLevel <= 0)
        {
            powerLevel = 100;
            powerStage--;
            SetLightBar();
        }
        if (powerStage <= 0)
        {
            if (powerSet <= 0)
            {
                shouldDrain = false;
                canFeed = false;
                StartCoroutine(Blink());
            }
            else
            {
                powerSet--;
                powerStage = 3;
                powerLevel = 100;
            }
        }
    }
    private void Interact()
    {

        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector3.forward, 10, interactable);
        
        if (hit.collider != null)
        {
            
            if (hit.collider.gameObject.tag == "Engine" && inventory.fishAmountOutside != 0 && canFeed && !feedCD)
            {
                
                FeedFish();
            }
        }
    }
    private IEnumerator Blink()
    {
        lightBar.transform.localScale = new Vector3(maxScale, lightBar.transform.localScale.y, lightBar.transform.localScale.z);
        var sprite = lightBar.GetComponent<SpriteRenderer>();
        Color color = Color.red;
        color.a = 255;
        sprite.color = color;
        for (int i = 1; i < 7; i++)
        {
            if (i%2 == 1)
            {
                color.a = 255;
            }else
            {
                color.a = 0;
            }
            sprite.color = color;
            yield return new WaitForSeconds(.5f);
        }
    }
    private void SetLightBar()
    {
        float scale = 0;
        for (int i = 0; i < powerStage; i++)
        {
            scale += maxScale / 3;
        }
        
        lightBar.transform.localScale = new Vector3(scale, lightBar.transform.localScale.y, lightBar.transform.localScale.z);
    }
    private void FeedFish()
    {
        powerLevel = 100;
        powerStage++;
        inventory.fishAmountOutside--;
        feedCD = true;
        StartCoroutine(FeedCD());

        if (powerStage > 3)
        {
            powerStage = 1;
            powerSet++;

            var sprite = lightBar.GetComponent<SpriteRenderer>();
            sprite.color = new Color(Random.Range(0,2), Random.Range(0, 2), Random.Range(0, 2), 255);
        }
        SetLightBar();
    }
    private IEnumerator FeedCD()
    {
        yield return new WaitForSeconds(1f);
        feedCD = false;
    }
}
