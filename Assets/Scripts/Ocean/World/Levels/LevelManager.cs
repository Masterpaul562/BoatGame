using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Info")]
    public int currentLevel;
    public float levelDistance;
    public float totalDistance;


    [Header("Settings")]
    public float maxLevelDistance;


    [Header("Refrence")]
    [SerializeField] private SpeedManager speed;
    private SetupOcean setter;

    private void Start()
    {
        currentLevel = 0;
        levelDistance = maxLevelDistance;
    }

    private void Update()
    {
        MoveTowardsLevel();
        
    }


    private void MoveTowardsLevel()
    {
         levelDistance = Mathf.MoveTowards(levelDistance, 0f, Time.deltaTime* speed.currentSpeed);
        UpdateTotalDistance();
        if(levelDistance <= 0f)
        {
            levelDistance = maxLevelDistance;
            ChangeLevel(currentLevel+1);
        }
    }
    private void UpdateTotalDistance()
    {
        totalDistance += Time.deltaTime * speed.currentSpeed;

    }

    private void ChangeLevel(int levelNum)
    {
        currentLevel = levelNum;
    }

    public void Restart()
    {
        currentLevel = 0;
        totalDistance = 0f;
        levelDistance = maxLevelDistance;

    }
}
