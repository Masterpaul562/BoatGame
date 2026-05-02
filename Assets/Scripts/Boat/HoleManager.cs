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

    [Header("Refrences")]
    [SerializeField] private GameObject holePrefab;
    [SerializeField] private Collider2D spawnLocation;
    [SerializeField] private Transform boat;
    [SerializeField] private Sprite spriteHole1;
    [SerializeField] private Sprite spriteHole2;
    [SerializeField] private GameObject floodWater;

    

    private void Update()
    {
       if( Input.GetMouseButtonDown(0)){
        CreateHole();
       }
       if(holes.Count > 0)
        {
            FloodBoat();
        }
    }

    public void CreateHole()
    {
        
       Vector2 spawnPos = FindSpawnLocation(spawnLocation.bounds);
       for(int i = 0; i < holes.Count; i++){

       }

       if(Random.Range(0,2) == 1){
       holePrefab.GetComponent<SpriteRenderer>().sprite = spriteHole1;
       }else
       {
         holePrefab.GetComponent<SpriteRenderer>().sprite = spriteHole2;
       }

        var hole = Instantiate(holePrefab,spawnPos, Quaternion.identity, boat);
        holes.Add(hole);
        floodWater.SetActive(true);

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

}
