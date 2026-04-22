using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
    public GameObject[] holes;

    [Header("Refrences")]
    [SerializeField] private GameObject holePrefab;
    [SerializeField] private SpriteRenderer spawnLocation;

    private void Update()
    {
        Debug.Log(spawnLocation.bounds.size.x);
    }

    public void CreateHole()
    {

    }

}
