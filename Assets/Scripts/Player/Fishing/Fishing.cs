using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    [SerializeField] private FishSpawner fishList;
    [SerializeField] private GameObject bobber;
    [SerializeField] private LineRenderController line;
    [SerializeField] private CastFishingLine castScript;
    public bool canBait;




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && bobber.GetComponent<Bobber>().submerged)
        {

            fishList.Bait();
        }
        if (fishList.shouldReel)
        {
            ReelIn();
        }
    }

    private void ReelIn()
    {
        Vector2 pos = Vector2.MoveTowards(bobber.transform.position, this.transform.position, Time.deltaTime * 10);
        bobber.transform.position = pos;
        float distance = Vector2.Distance(transform.position, bobber.transform.position);
        if (distance < 1)
        {
            bobber.SetActive(false);
            line.gameObject.SetActive(false);
            bobber.GetComponent<Bobber>().rb.simulated = true;
            bobber.GetComponent<Bobber>().submerged = false;
            castScript.hasCast = false;
            fishList.shouldReel = false;
            castScript.canCast = true;
            SecureFish();
        }

    }
    private void SecureFish()
    {
       // shouldDestroy = false;
        fishList.fish.RemoveAt(fishList.closestFishIndex);
        Destroy(fishList.closestFish);

        //CaughtFish();
    }
}
