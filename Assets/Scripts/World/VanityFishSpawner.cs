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

private void Start()
    {
        shouldBeSpawning = true;
        StartCoroutine(SpawnFish());
    }

    private void SpawnFish(int spawnAmount) {

        for (int i = 0; i < spawnAmount; i++)
        {
            int random = Random.Range(0, 2);
            Vector2 spawnLocation = new Vector2(spawnLocationBase.position.x + Random.Range(-10,10), spawnLocationBase.position.y+Random.Range(-3,2));
            var fishs = Instantiate(fishPrefab, spawnLocation, Quaternion.identity);
            fishs.GetComponent<Fish>().enabled = false;
            fishs.GetComponent<FloaterMovement>().enabled = true;
            fishs.GetComponent<FloaterMovement>().moveAmount = Random.Range(0.001f, 0.01f);
            fish.Add(fishs);
            if(random == 0)
            {
                fishs.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
            }
            Debug.Log(random);

        }

    }

    private IEnumerator SpawnFish()
    {
        while (shouldBeSpawning)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));
            if (fish.Count < maxNumOfFish)
            {
                int random = Random.Range(1, 5);
                SpawnFish(random);
            }

        }
        yield return null;
    }
}

