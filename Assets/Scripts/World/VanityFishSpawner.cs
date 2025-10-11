using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanityFishSpawner : MonoBehaviour
{
    public List<GameObject> fish = new List<GameObject>();
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private int maxNumOfFish;
    [SerializeField] private Transform spawnLocationBase;
    public bool shouldBeSpawning;

public void Start()
    {
        Debug.Log("yay");
        shouldBeSpawning = true;
        StartCoroutine(SpawnFish());
    }

    private void SpawnFish(int spawnAmount) {

        for (int i = 0; i < spawnAmount; i++)
        {
            
            Vector2 spawnLocation = new Vector2(spawnLocationBase.position.x + Random.Range(-10,10), spawnLocationBase.position.y+Random.Range(-3,2));
            var fishs = Instantiate(fishPrefab, spawnLocation, Quaternion.identity);
            fishs.GetComponent<Fish>().enabled = false;
            fishs.GetComponent<FloaterMovement>().enabled = true;
            fishs.GetComponent<FloaterMovement>().moveAmount = Random.Range(0.001f, 0.01f);
            fish.Add(fishs);
            fishs.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
        
            
        

        }

    }

    private IEnumerator SpawnFish()
    {
        while (shouldBeSpawning)
        {
            yield return new WaitForSeconds(Random.Range(4, 7));
            if (fish.Count < maxNumOfFish)
            {
                int random = Random.Range(1, 3);
                SpawnFish(random);
            }

        }
        yield return null;
    }
}

