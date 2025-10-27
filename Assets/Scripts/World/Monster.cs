using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public bool shouldAppear;
    public bool isHere;
    public int spawnChance;
    public float spawnTimer;
    public int feedAmount;
    public int feedAmountMin, feedAmountMax, spawnTimerMin, spawnTimerMax;
    private int randomValue;
    [SerializeField] private GameObject monster;
    [SerializeField] private FishInventory inventory;
    [SerializeField] private Camera cam;
    [SerializeField] private FishManager fishManager;
    [SerializeField] private EnterFishing fishing;
    [SerializeField] private BgSpeedControler bgSpeed;
    private Transform spawnPos;
    private Transform movePos;
    private bool startCo;

    private void Start()
    {
        StartCoroutine(SpawnCheck(true));
        spawnPos = transform.GetChild(1);
        movePos = transform.GetChild(2);
        feedAmountMin = 5;
        feedAmountMax = 15;
        spawnTimerMin = 30;
        spawnTimerMax = 60;
        startCo = false;
    }
    private void Update()
    {
        if(startCo)
        {
            StartCoroutine(SpawnCheck(true));
            startCo = false;
        }
    }

    public IEnumerator Spawn()
    {
        fishing.ExitFishing();
        StartCoroutine(bgSpeed.SlowDownOcean());
        bgSpeed.inEvent = true;
        fishManager.MoveVanityFishOff();
        var camShake = cam.GetComponent<CameraShake>();
        fishManager.inEvent = true;
        isHere = true;
        feedAmount = Random.Range(feedAmountMin, feedAmountMax);
        monster.SetActive(true);
        monster.transform.position = new Vector2(spawnPos.position.x, spawnPos.position.y);
        yield return null;
        fishManager.MoveVanityFishOff();
        camShake.rumble = true;
        StartCoroutine(camShake.Rumble(0.1f));
        yield return new WaitForSeconds(1.5f);
        camShake.rumble = false;
        while (monster.transform.position != movePos.position)
        {
            Vector2 move = Vector2.MoveTowards(monster.transform.position, movePos.position, Time.deltaTime * 10);
            monster.transform.position = move;
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Feed());
        yield return null;
    }
    public IEnumerator Leave()
    {
        isHere = false;
        StartCoroutine(bgSpeed.SpeedUpOcean());
        fishManager.inEvent = false;
        startCo = true;
        yield return null;
        while (monster.transform.position != spawnPos.position)
        {
            Vector2 move = Vector2.MoveTowards(monster.transform.position, spawnPos.position, Time.deltaTime * 10);
            monster.transform.position = move;
            yield return null;
        }
        monster.SetActive(false);
        yield return null;
    }
    private IEnumerator SpawnCheck(bool shouldTry)
    {
        while (shouldTry)
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
                    shouldTry = false;
                    
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
    private IEnumerator Feed()
    {
        feedAmount -= inventory.fishAmountOutside;
        inventory.fishAmountOutside = 0;
        yield return null;
        if(feedAmount > 0)
        {
            Debug.Log("You lost");
        }
        yield return null;
        //DO stuff here like display text/give item or reward for feed
        yield return new WaitForSeconds(4.5f);
        StartCoroutine(Leave());
        yield return null;
    }

}

