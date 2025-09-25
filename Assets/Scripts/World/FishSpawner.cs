using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private GameObject bobber;
    [SerializeField] private EnterFishing enterFishing;
    List<GameObject> fish = new List<GameObject>();

    private void Update() 
    {
 if (enterFishing.isFishing) 
{
    SpawnFish();
}

    }

    public void SpawnFish()
    {
       // Vector2 position = new Vector2 (random(0,10),random(0,10));
    //Instantiate(fishPrefab,0,0, Quaternion.identity);

    }
}
