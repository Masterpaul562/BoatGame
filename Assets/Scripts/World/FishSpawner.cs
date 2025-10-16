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
    [SerializeField] private Camera cam;
    [SerializeField] private Vector2 spawnLocation;
    public int maxNumOfFish;

    [SerializeField] public bool shouldBeSpawning;
   
    public bool isCity;





    private void Update()
    {
       



        if (bobber.GetComponent<Bobber>().submerged)
        {
          
        }
        


    }

    public void SpawnFish(int spawnAmount)
    {


        for (int i = 0; i < spawnAmount; i++)
        {

            int side = Random.Range(0, 2);
            int swimBobber = Random.Range(0, 100);

           

            if (side == 0)
            {
                float outside = cam.transform.position.x - cam.GetComponent<CamSizeManager>().worldWidth / 2;
                spawnLocation = new Vector2(outside - 4, Random.Range(-7, -1));
            }
            else
            {
                float outside = cam.transform.position.x + cam.GetComponent<CamSizeManager>().worldWidth / 2;
                spawnLocation = new Vector2(outside + 4, Random.Range(-7, -1));
            }
            var fishs = Instantiate(fishPrefab, spawnLocation, Quaternion.identity);
            var fishScript = fishs.GetComponent<Fish>();
            if (swimBobber > 70)
            {
                fishScript.shouldSwimToBobber = true;
            }
            fishScript.bobber = bobber.transform;
            fishScript.GetComponent<FloaterMovement>().enabled = false;
            fishScript.swimDirection = side;
            fishScript.speed = Random.Range(0.3f, 5);
            fishScript.cam = cam;
            fishs.GetComponent<FloaterMovement>().cam = cam;
            fish.Add(fishs);
        }
    }

    public void StartFishy()
    {
        shouldBeSpawning = true;
        
        maxNumOfFish = 10;
        StartCoroutine(FishySpawning());

    }

    public IEnumerator FishySpawning()
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
