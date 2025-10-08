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
    
    public int maxNumOfFish;

    private bool shouldBeSpawning;
    private bool startCoroutine = true;





    private void Update()
    {


        if (bobber.GetComponent<Bobber>().submerged)
        {
           // shouldBeSpawning = false;
            if (startCoroutine)
            {
                shouldBeSpawning = true;
                startCoroutine = false;
                maxNumOfFish = 10;
                StartCoroutine(FishySpawning());
            }
        }
        else if (enterFishing.isFishing == false)
        {
            shouldBeSpawning = false;
            startCoroutine = true;
           // for (int i = 0; i < fish.Count; i++)
           // {
           //     Destroy(fish[i]);
          //      fish.RemoveAt(i);
          //  }


        }


    }

    public void SpawnFish(int spawnAmount)
    {


        for (int i = 0; i < spawnAmount; i++)
        {

            int childNum = Random.Range(0, 2);
            int swimBobber = Random.Range(0, 100);

            Vector2 spawnChild = transform.GetChild(childNum).position;

            Vector2 spawnLocation = new Vector2(spawnChild.x + Random.Range(-4, 4), spawnChild.y + Random.Range(-2, 2));

            var fishs = Instantiate(fishPrefab, spawnLocation, Quaternion.identity);
            var fishScript = fishs.GetComponent<Fish>();
            if (swimBobber > 70)
            {
                fishScript.shouldSwimToBobber = true;
            }
            fishScript.bobber = bobber.transform;
            fishScript.GetComponent<FloaterMovement>().enabled = false;
            fishScript.swimDirection = childNum;
            fishScript.leftX = transform.GetChild(0).position.x;
            fishScript.rightX = transform.GetChild(1).position.x;
            fishScript.speed = Random.Range(0.3f, 5);
            fish.Add(fishs);
        }
    }



    private IEnumerator FishySpawning()
    {
        while (shouldBeSpawning)
        {
            yield return new WaitForSeconds(Random.Range(3, 7));
            if (fish.Count < maxNumOfFish)
            {
                SpawnFish(1);
            }

        }
        yield return null;
    }


}
