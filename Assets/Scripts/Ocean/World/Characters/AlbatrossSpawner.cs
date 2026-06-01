using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbatrossSpawner : MonoBehaviour
{
    [Header("Settings")]

    public int waitMin;
    public int waitMax;
    private bool shouldSpawn = true;

    [Header("Refrences")]
    public GameObject bird;



    private void Update()
    {
        if (!bird.GetComponent<Albatross>().isSpawned && shouldSpawn)
        {
            shouldSpawn = false;
            StartCoroutine(SpawnBird());
        }
    }

   private IEnumerator SpawnBird()
    {
        int waitTime = Random.Range(waitMin, waitMax);
        yield return new WaitForSeconds(waitTime);
        bird.SetActive(true);
        StartCoroutine(bird.GetComponent<Albatross>().Perch());
        bird.GetComponent<Albatross>().isSpawned = true;
        shouldSpawn = true;
    }
}
