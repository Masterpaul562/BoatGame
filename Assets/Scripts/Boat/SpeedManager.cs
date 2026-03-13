using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    public float currentSpeed;
    public FishEngineReal engine;

    [Header("Speed Modifier")]
    public int stageKnotAmount;
        

    void Start()
    {
      
    }


    void Update()
    {
        CalculateSpeed();
    }

    private void CalculateSpeed()
    {
        currentSpeed = 0;
        currentSpeed += engine.powerStage * stageKnotAmount;
        currentSpeed += (stageKnotAmount * 3 ) * engine.powerSet;
    }
}
