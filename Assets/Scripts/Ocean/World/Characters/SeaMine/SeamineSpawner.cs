using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeamineSpawner : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] public List<GameObject> mines = new List<GameObject>();
    [SerializeField] private GameObject SeaminePrefab;
    [SerializeField] private LevelManager levels;
    [SerializeField] private CamSizeManager size;
    //private AnimatorController animController;
    public Floater floatScript;
    [Header("Settings")]
    public float spawnTimeMin;
    public float spawnTimeMax;

    [Header("")]
    public float yOffsetMin;
    public float yOffsetMax;
    public float gracePeriod;
    // Work stuff
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
            StartCoroutine(SpawnMine());
        }
        RemoveDestroyed();
    }
    

    private IEnumerator SpawnMine()
    {
        float waitTime = Random.Range(spawnTimeMin, spawnTimeMax);
        yield return new WaitForSeconds(waitTime);

        Vector3 spawnLocation = new Vector3((size.worldWidth / 2) + 2, 0, 0);
        var mine = Instantiate(SeaminePrefab, spawnLocation, Quaternion.identity);
        mine.AddComponent<Floater>();

        var mineFloater = mine.GetComponent<Floater>();


        mineFloater.wave = floatScript.wave;
        mineFloater.speed = floatScript.speed;

        float yOffset = Random.Range(yOffsetMin, yOffsetMax);
        mineFloater.yOffset = yOffset;

        mineFloater.speedMult = floatScript.speedMult;  
        mineFloater.defaultSpeed = floatScript.defaultSpeed;

        mines.Add(mine);

        spawning = false;
    }


    public void Restart()
    {
        for (int i = 0; i < mines.Count; i++)
        {
            Destroy(mines[i]);
            mines.RemoveAt(i);
        }
        StartCoroutine(Grace());
    }
    private void RemoveDestroyed()
    {
        for(int i = 0; i < mines.Count; i++)
        {
            if (mines[i].gameObject == null)
            {
                mines.RemoveAt(i);
            }
        }
    }
}
