using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanityFishSpawner : MonoBehaviour
{
    public List<GameObject> fish = new List<GameObject>();
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private int maxNumOfFish;
    private Vector2 spawnLocation;
    [SerializeField] private Camera cam;
    public bool shouldBeSpawning;

public void Start()
    {
       
        shouldBeSpawning = true;
        StartCoroutine(SpawnFish());
    }

    private void SpawnFish(int spawnAmount) {

        for (int i = 0; i < spawnAmount; i++)
        {



            
            float outside = cam.transform.position.x + cam.GetComponent<CamSizeManager>().worldWidth / 2;
            
            spawnLocation = new Vector2(outside + 4, Random.Range(-7, -1));
            var fishs = Instantiate(fishPrefab, spawnLocation, Quaternion.identity);
            fishs.GetComponent<Fish>().enabled = false;
            fishs.GetComponent<FloaterMovement>().enabled = true;
            fishs.GetComponent<FloaterMovement>().cam = cam;
            fishs.GetComponent<FloaterMovement>().moveAmount = Random.Range(0.05f, 0.1f);
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

