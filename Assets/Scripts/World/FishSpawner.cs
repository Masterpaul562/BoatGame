using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> fish = new List<GameObject>();
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bobber;
    [SerializeField] private EnterFishing enterFishing;
    [SerializeField] float closestDistance;
    [SerializeField] GameObject closestFish;
    [SerializeField] private bool hasSpawned;
    [SerializeField] private int closestFishIndex;
    public int numOfFish;
    public bool shouldReel;
    
    private bool shouldDestroy = true;
    

    private void Update()
    {


        if (bobber.GetComponent<Bobber>().submerged)
        {

            if (!hasSpawned)
            {
                SpawnFish();
            }
            FindClosestFish();
            ShouldReelIn();
            

        }
        else if (enterFishing.isFishing == false)
        {
            for (int i = 0; i < fish.Count; i++)
            {
                Destroy(fish[i]);
                fish.RemoveAt(i);
            }

            hasSpawned = false;
        }


    }

    public void SpawnFish()
    {
        hasSpawned = true;

        for (int i = 0; i < numOfFish; i++)
        {

            int childNum = Random.Range(0, 2);

            Vector2 spawnChild = transform.GetChild(childNum).position;

            Vector2 spawnLocation = new Vector2(spawnChild.x + Random.Range(-4, 4), spawnChild.y + Random.Range(-2, 2));

            var fishs = Instantiate(fishPrefab, spawnLocation, Quaternion.identity);
            var fishScript = fishs.GetComponent<Fish>();
            fishScript.bobber = bobber.transform;
            fishScript.swimDirection = childNum;
            fishScript.leftX = transform.GetChild(0).position.x;
            fishScript.rightX = transform.GetChild(1).position.x;
            fishScript.speed = Random.Range(0, 5);
            fish.Add(fishs);
        }
    }

    public void Bait()
    {
        if (closestFish != null)
        {
            if (closestDistance < 10)
            {
                closestFish.GetComponent<Fish>().shouldSwimToBobber = true;
            }
        }
    }

    private void FindClosestFish()
    {

        closestDistance = 10000000000;
        for (int i = 0; i < fish.Count; i++)
        {
            float distance = Vector2.Distance(bobber.transform.position, fish[i].transform.position);
            if (closestDistance > distance)
            {
                closestDistance = distance;
                closestFish = fish[i];
                closestFishIndex = i;
            }

        }


    }

    private void ShouldReelIn()
    {
        if (closestFish != null)
        {
            if (Vector2.Distance(bobber.transform.position, closestFish.transform.position) < 1)
            {
                shouldReel = true;
                closestFish.transform.position = bobber.transform.position;
                closestFish.transform.parent = bobber.transform;
            }
        }
        //Debug.Log(Vector2.Distance(closestFish.transform.position, player.transform.position));

        if (Vector2.Distance(closestFish.transform.position, player.transform.position) < 2 && shouldDestroy)
        {
            Debug.Log(1);
            shouldDestroy = false;
            fish.RemoveAt(closestFishIndex);
            Destroy(closestFish);            
            FindClosestFish();
           CaughtFish();
        }
    }
    private void CaughtFish()
    {
        shouldDestroy = true;
    }

}
