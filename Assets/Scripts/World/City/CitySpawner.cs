using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySpawner : MonoBehaviour
{
    private LevelManager level;

    private bool spawnedCity;

    [SerializeField] private  GameObject city;

    private void Start()
    {
        level = this.GetComponent<LevelManager>();
    }

    private void Update()
    {
        if (level.currentLevel == 2 && !spawnedCity)
        {
            SpawnCity();
        }else if (level.currentLevel >= 3)
        {
            DespawnCity();
        }
    }

    private void SpawnCity()
    {
        spawnedCity = true;
        city.SetActive(true);
    }

    private void DespawnCity()
    {
        spawnedCity = false;
        city.SetActive(false);
    }
}
