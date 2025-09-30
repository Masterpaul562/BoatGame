using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] public List<GameObject> fish = new List<GameObject>();
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bobber;
    [SerializeField] private EnterFishing enterFishing;
    [SerializeField] float closestDistance;
    [SerializeField]public GameObject closestFish;
    [SerializeField] private bool hasSpawned;
    [SerializeField] public int closestFishIndex;
    public int numOfFish;
    public bool shouldReel;
    private bool shouldBeSpawning;
    private bool startCoroutine = true;
   // public bool 
    
    //private bool shouldDestroy = true;
    

    private void Update()   
    {
       

        if (bobber.GetComponent<Bobber>().submerged)
        {
    if(startCoroutine){
            shouldBeSpawning = true;
    startCoroutine= false;
    StartCoroutine(FishySpawning());
    Debug.Log("YAY");

        }
           // if (!hasSpawned)
            //{
           //     SpawnFish(numOfFish);
           // }
            FindClosestFish();
            ShouldReelIn();
            

        }
        else if (enterFishing.isFishing == false)
        {
            shouldBeSpawning = false;
            for (int i = 0; i < fish.Count; i++)
            {
                Destroy(fish[i]);
                fish.RemoveAt(i);
            }

            hasSpawned = false;
        }


    }

    public void SpawnFish(int spawnAmount)
    {
        hasSpawned = true;

        for (int i = 0; i < spawnAmount; i++)
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
            fishScript.speed = Random.Range(0.3f, 5);
            fish.Add(fishs);
        }
    }

    public void Bait()
    {
        if (closestFish != null)
        {
            if (closestDistance < 5)
            {
                var script = closestFish.GetComponent<Fish>();
                closestFish.GetComponent<Fish>().shouldSwimToBobber = true;
                if (script.speed > 0.2f)
                {
                    script.speed -= 0.2f;
                }
            }
            else
            {
                var script = closestFish.GetComponent<Fish>();
                if (script.randomY < -2.5)
                {
                    script.randomY += .5f;
                }
                if (script.speed > 0.2f)
                {
                    script.speed -= 0.2f;
                }
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

       
    }
    private IEnumerator FishySpawning(){
        while(shouldBeSpawning){
            yield return new WaitForSeconds(Random.Range(2,5));
            SpawnFish(1);
            Debug.Log("yay");


        }
        yield return null;
    }    


}
