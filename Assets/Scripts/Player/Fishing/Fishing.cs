using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    [SerializeField] private FishSpawner fishList;
    public bool canBait;
   


   private  void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) {
            fishList.Bait();
        }



    }
}
