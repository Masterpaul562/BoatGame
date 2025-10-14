using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySpawner : MonoBehaviour
{
    public int waitTime;
    private int randomValue;
    [SerializeField] private int randomMax;
    [SerializeField] GameObject[] cityPrefabs;

    
    private void Start()
    {
        StartCoroutine(SpawnCityCoolDown());

    }


    private void SpawnCity()
    {
        Debug.Log("Spawned City");
    }

    private IEnumerator SpawnCityCoolDown()
    {
        while (true)
        {
            
            
            randomValue = Random.Range(0, randomMax);
            Debug.Log(randomValue);
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
