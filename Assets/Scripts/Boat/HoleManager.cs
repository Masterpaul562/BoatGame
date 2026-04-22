using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
    public GameObject[] holes;

    [Header("Refrences")]
    [SerializeField] private GameObject holePrefab;
    [SerializeField] private Collider2D spawnLocation;
    [SerializeField] private Transform boat;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)){
CreateHole();
        }
    }

    public void CreateHole()
    {
       Vector2 spawnPos = FindSpawnLocation(spawnLocation.bounds);
        Instantiate(holePrefab,spawnPos, Quaternion.identity, boat);

    }

    private Vector2 FindSpawnLocation(Bounds bound)
    {
        return new Vector2 (
        Random.Range(bound.min.x,bound.max.x),
        Random.Range(bound.min.y,bound.max.y)
        );
    }

}
