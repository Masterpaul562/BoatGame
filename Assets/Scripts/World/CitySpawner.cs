using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySpawner : MonoBehaviour
{
    public int waitTime;
    private int randomValue;
    [SerializeField] private int randomMax;
    [SerializeField] Camera cam;
    [SerializeField] EnterFishing fishing;
    [SerializeField] GameObject[] cityPrefabs;
    [SerializeField] Vector2 spawnLocation;
    private CityManager manager;
    private float worldWidth;
    public bool shouldSpawn;
     

    
    private void Start()
    {
        StartCoroutine(SpawnCityCoolDown());
        manager = GetComponent<CityManager>();

    }


    private void SpawnCity()
    {
        if (shouldSpawn)
        {
            Debug.Log("Spawned City");
            shouldSpawn = false;
            
            spawnLocation = new Vector2(cam.GetComponent<CamSizeManager>().worldWidth + Random.Range(30, 65), Random.Range(1, 5.5f));
            var city = Instantiate(cityPrefabs[1], spawnLocation, Quaternion.identity);
            city.GetComponent<FloaterMovement>().cam = cam; 
            manager.currentCity = city;
        }
    }

    private IEnumerator SpawnCityCoolDown()
    {
        while (true)
        {
            
            
            randomValue = Random.Range(0, randomMax);
            
            yield return new WaitForSeconds(waitTime);
            if (randomValue == 0)
            {
               
                SpawnCity();
                yield return null;
            }
            else
            {
                yield return null;
            }
        }
    }
}
