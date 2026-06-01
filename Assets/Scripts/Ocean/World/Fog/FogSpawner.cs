using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fog;
    [SerializeField] private Camera cam;
    public bool shouldSpawn;

    private void Start()
    {
        shouldSpawn = true;
        StartCoroutine(SpawnFog());
    }

    private IEnumerator SpawnFog()
    {
        while (shouldSpawn)
        {
            yield return new WaitForSeconds(Random.Range(40,80));
            Spawn(Random.Range(1,3));
        }
    }
    private void Spawn(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector2 spawnPos = new Vector2(cam.transform.position.x + cam.GetComponent<CamSizeManager>().worldWidth + Random.Range(2, 10), cam.transform.position.y - 1.5f);
            var fogs = Instantiate(fog, spawnPos, Quaternion.identity);
            var sprite = fogs.GetComponent<SpriteRenderer>();
            int random = Random.Range(0, 2);
            fogs.GetComponent<FogMove>().speed = Random.Range(1, 7);
            fogs.GetComponent<FogMove>().cam = cam;
            if (random == 1)
            {
                sprite.sortingLayerName = "Background";
            }
            else
            {
                sprite.sortingLayerName = "ForegroundOutside";
            }
        }
    }
}
