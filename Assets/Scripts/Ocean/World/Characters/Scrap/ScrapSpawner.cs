using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapSpawner : MonoBehaviour
{
    [Header("Refrences")]
    public List<GameObject> scraps = new List<GameObject>();
    [SerializeField] private GameObject scrapPrefab;
    [SerializeField] private Sprite[] scrapSprites;
    [SerializeField] private LevelManager levels;
    [SerializeField] private CamSizeManager size;
    [SerializeField] private Floater floatScript;
    [SerializeField] private FishInventory inventory;


    [Header("Settings")]
    public float spawnTimeMin;
    public float spawnTimeMax;

    [Header("")]
    public float yOffsetMin;
    public float yOffsetMax;
    public float gracePeriod;

    private bool spawning = false;
    private bool canSpawn = false;

    private void Start()
    {
        StartCoroutine(Grace());
        floatScript = GetComponent<Floater>();
    }

    private IEnumerator Grace()
    {
        yield return new WaitForSeconds(gracePeriod);
        canSpawn = true;

    }


    private void Update()
    {
        if (canSpawn && !spawning)
        {
            spawning = true;
            StartCoroutine(SpawnScrap());
        }
        RemoveDestroyed();
    }


    private IEnumerator SpawnScrap()
    {
        float waitTime = Random.Range(spawnTimeMin, spawnTimeMax);
        yield return new WaitForSeconds(waitTime);

        Vector3 spawnLocation = new Vector3((size.worldWidth / 2) + 2, 0, 0);
        var scrap = Instantiate(scrapPrefab, spawnLocation, Quaternion.identity);
        scrap.AddComponent<Floater>();

        var scrapFloater = scrap.GetComponent<Floater>();


        scrapFloater.wave = floatScript.wave;
        scrapFloater.speed = floatScript.speed;

        float yOffset = Random.Range(yOffsetMin, yOffsetMax);
        scrapFloater.yOffset = yOffset;

        scrapFloater.speedMult = floatScript.speedMult;
        scrapFloater.defaultSpeed = floatScript.defaultSpeed;

        scraps.Add(scrap);

        spawning = false;
    }

    public void Restart()
    {
        for (int i = 0; i < scraps.Count; i++)
        {
            Destroy(scraps[i]);
            scraps.RemoveAt(i);
        }
        StartCoroutine(Grace());
    }

    private void RemoveDestroyed()
    {
        for (int i = 0; i < scraps.Count; i++)
        {
            if (scraps[i].gameObject == null)
            {
                scraps.RemoveAt(i);
            }
        }
    }
}

