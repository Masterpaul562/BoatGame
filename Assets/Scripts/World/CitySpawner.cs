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
            float aspect = (float)Screen.width / Screen.height;
            float worldHeight = cam.orthographicSize * 2;
            worldWidth = worldHeight * aspect;
            spawnLocation = new Vector2(worldWidth + Random.Range(30, 65), Random.Range(1, 5.5f));
            var city = Instantiate(cityPrefabs[1], spawnLocation, Quaternion.identity);
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
