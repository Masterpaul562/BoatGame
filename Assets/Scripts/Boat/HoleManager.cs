using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{

    [Header("Info")]
    public List<GameObject> holes = new List<GameObject>();
    public float waterLevel;

    [Header("Settings")]
    public float waterSpeed;
    public float sinkLevel;
    public float sinkSpeed;
    public float sinkRotSpeed;

    [Header("Refrences")]
    [SerializeField] private GameObject holePrefab;
    [SerializeField] private Collider2D spawnLocation;
    [SerializeField] private Transform boat;
    [SerializeField] private Sprite spriteHole1;
    [SerializeField] private Sprite spriteHole2;
    [SerializeField] private GameObject floodWater;

    

    private void Update()
    {
        if( Input.GetMouseButtonDown(0))
        {
        CreateHole();
        }
        if(holes.Count > 0)
        {
            FloodBoat();
        }

        if(waterLevel> sinkLevel)
        {
            SinkBoat();
        }
        
    }

    public void CreateHole()
    {

        Vector2 spawnPos = FindSpawnLocation(spawnLocation.bounds);
        bool shouldSpawn = false;
        if (holes.Count > 0)
        {
            for (int i = 0; i < holes.Count; i++)
            {
                if (spawnPos.x > holes[i].transform.position.x - 1 || spawnPos.x < holes[i].transform.position.x + 1)
                {
                    shouldSpawn = true;
                    i = 1000;
                }
                else
                {
                    Debug.Log("Failed");
                }
            }
        }

        if (shouldSpawn || holes.Count<=0)
        {


            if (Random.Range(0, 2) == 1)
            {
                holePrefab.GetComponent<SpriteRenderer>().sprite = spriteHole1;
            }
            else
            {
                holePrefab.GetComponent<SpriteRenderer>().sprite = spriteHole2;
            }

            var hole = Instantiate(holePrefab, spawnPos, Quaternion.identity, boat);
            holes.Add(hole);
            floodWater.SetActive(true);
        }
        else
        {
            Debug.Log("Failed to Spawn");
        }
    }

    private Vector2 FindSpawnLocation(Bounds bound)
    {
        return new Vector2 (
        Random.Range(bound.min.x,bound.max.x),
        Random.Range(bound.min.y,bound.max.y)
        );
    }


    private void FloodBoat()
    {
        waterLevel += waterSpeed * holes.Count * Time.deltaTime;
        floodWater.transform.localScale = new Vector2(floodWater.transform.localScale.x, waterLevel);
    }
    private void SinkBoat()
    {
        var wave = this.gameObject.GetComponent<MoveWithWaves>();
        wave.yOffset -= sinkSpeed * waterLevel * Time.deltaTime;

        wave.rotationOffset += sinkRotSpeed * Time.deltaTime;
    }

}
