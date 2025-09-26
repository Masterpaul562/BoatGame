using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> fish = new List<GameObject>();
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private GameObject bobber;
    [SerializeField] private EnterFishing enterFishing;
    private bool hasSpawned;
    public int numOfFish;

    private void Update()
    {
        
        
        if (bobber.GetComponent<Bobber>().submerged)
        {
            if (!hasSpawned)
            {
                SpawnFish();
            }

        }
        else if (enterFishing.isFishing == false){
            for (int i = 0; i < fish.Count; i++)
            {
                Destroy(fish[i]);
                fish.RemoveAt(i);
            }
        }


    }

    public void SpawnFish()
    {
        hasSpawned = true;

        for (int i = 0; i < numOfFish; i++)
        {

            int childNum = Random.Range(0, 3);
            Vector2 spawnChild = transform.GetChild(childNum).position;

            Vector2 spawnLocation = new Vector2(spawnChild.x + Random.Range(-4, 4), spawnChild.y + Random.Range(-2, 2));

            var fishs = Instantiate(fishPrefab,spawnLocation, Quaternion.identity);
            var fishScript = fishs.GetComponent<Fish>();
            fishScript.bobber = bobber.transform;
            fish.Add(fishs);
        }
    }

    public void Bait()
    {
        for (int i = 0; i < fish.Count; i++)
        {
            float distance = Vector2.Distance(bobber.transform.position, fish[i].transform.position);
            if(distance <5)
            {
                fish[i].GetComponent<Fish>().shouldSwimToBobber = true;
            }

        }
    }

}
