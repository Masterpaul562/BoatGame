using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkSpawner : MonoBehaviour
{
    public List<GameObject> junk = new List<GameObject>();
    [SerializeField] private Transform spawnLocationBase;
    [SerializeField] private GameObject junkPrefab;
    [SerializeField] private Camera cam;
    public bool shouldBeSpawning;

    public bool shouldSpawn;

    void Update()
    {
        for (int i = 0; i < junk.Count; i++)
        {
            if (junk[i].GetComponent<FloaterMovement>().DestroyThis())
            {

                Destroy(junk[i]);
                junk.RemoveAt(i);
            }
        }
    }
    private void Spawn(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            float x = cam.transform.position.x + cam.GetComponent<CamSizeManager>().worldWidth / 2;
            float y = 0.6f;
            Vector2 spawnLocation = new Vector2(x, y);
            var junks = Instantiate(junkPrefab, spawnLocation, Quaternion.identity);
            junks.GetComponent<FloaterMovement>().cam = cam;

            junk.Add(junks);





        }

    }
    // Needs to be started when close to city
    public IEnumerator Spawning()
    {
        while (shouldBeSpawning)
        {

            yield return new WaitForSeconds(Random.Range(13, 20));
            Spawn(1);

        }
        yield return null;

    }
}
