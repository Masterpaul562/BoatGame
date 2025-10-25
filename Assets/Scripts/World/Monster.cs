using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public bool shouldAppear;
    public int spawnChance;
    public float spawnTimer;
    public int feedAmount;
    private int feedAmountMin, feedAmountMax, spawnTimerMin, spawnTimerMax;
    private int randomValue;
    [SerializeField] private GameObject monster;
    [SerializeField] private Camera cam;
    
    private void Start()
    {
        StartCoroutine(SpawnCheck());
        feedAmountMin = 5;
        feedAmountMax = 15;
        spawnTimerMin = 30;
        spawnTimerMax = 120;
    }

    public IEnumerator Spawn()
    {
        
        feedAmount = Random.Range(feedAmountMin, feedAmountMax);    
        monster.SetActive(true);
        yield return null;
    }
    private IEnumerator SpawnCheck()
    {
        while (true)
        {

            if (shouldAppear)
            {
                randomValue = Random.Range(0, spawnChance);
            }

            yield return new WaitForSeconds(spawnTimer);
            spawnTimer = Random.Range(spawnTimerMin, spawnTimerMax);
            if (randomValue == 0)
            {

                if (shouldAppear)
                {
                    StartCoroutine(Spawn());
                }
                yield return null;
            }
            else
            {
                spawnChance--;
                yield return null;
            }

        }
    }


}

