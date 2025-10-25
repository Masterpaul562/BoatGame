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
    private Transform spawnPos;
    private Transform movePos;
    
    private void Start()
    {
        StartCoroutine(SpawnCheck());
        spawnPos = transform.GetChild(1);
        movePos = transform.GetChild(2);
        feedAmountMin = 5;
        feedAmountMax = 15;
        spawnTimerMin = 30;
        spawnTimerMax = 120;
    }

    public IEnumerator Spawn()
    {
        var camShake = cam.GetComponent<CameraShake>();
        feedAmount = Random.Range(feedAmountMin, feedAmountMax);    
        monster.SetActive(true);
        monster.transform.position = new Vector2 (spawnPos.position.x,spawnPos.position.y);  
        yield return null;
        camShake.rumble = true;
        StartCoroutine(camShake.Rumble(0.1f));
        yield return new WaitForSeconds(2.5f);
        camShake.rumble = false;
        while(monster.transform.position != movePos.position)
        {
            Vector2 move = Vector2.MoveTowards(monster.transform.position, movePos.position, Time.deltaTime * 10);
            monster.transform.position = move;
            yield return null;  
        }
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

