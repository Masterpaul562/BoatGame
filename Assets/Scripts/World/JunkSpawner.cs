using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkSpawner : MonoBehaviour
{
    public List<GameObject> junk = new List<GameObject>();
    [SerializeField] private Transform spawnLocationBase;
    [SerializeField] private GameObject junkPrefab;
    public bool shouldBeSpawning;

    public bool shouldSpawn;
   
    void Update()
    {
        
    }
    private void Spawn(int amount)
    {
        for (int i = 0; i < amount; i++)
        {

            Vector2 spawnLocation = new Vector2(spawnLocationBase.position.x, spawnLocationBase.position.y );
            var junks = Instantiate(junkPrefab, spawnLocation, Quaternion.identity);

            junk.Add(junks);





        }

    }
    // Needs to be started when close to city
    private IEnumerator Spawning()
    {
        while (shouldBeSpawning)
        {
            yield return new WaitForSeconds(Random.Range(13, 20));
                Spawn(1);
            
        }
        yield return null;

    }
}
