using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpawner : MonoBehaviour
{
    private LevelManager level;

    //private bool spawnedCity;

    [SerializeField] private  GameObject[] scenes;
    private Transform[] originalPos;

    private void Start()
    {
        level = this.GetComponent<LevelManager>();
        originalPos = new Transform[scenes.Length];
        for (int i = 0; i <scenes.Length; i++)
        {
            originalPos[i] = scenes[i].transform;
        }
    }

    private void Update()
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            if (level.currentLevel == i)
            {
                SpawnScene(i);
            }
            else
            {
                DespawnScene(i);
            }
        }
    }

    private void SpawnScene(int index)
    {
        //spawnedCity = true;
        scenes[index].SetActive(true);
    }

    private void DespawnScene(int index)
    {
       // spawnedCity = false;
        scenes[index].SetActive(false);
    }

    public void Restart()
    {
        for(int i = 0; i< scenes.Length; i++)
        {
            scenes[i].transform.position = originalPos[i].position;
        }
    }
}
