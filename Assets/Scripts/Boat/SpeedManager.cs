using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    public float currentSpeed;
    public FishEngineReal engine;


    [Header("Earwig")]
    [SerializeField] private GameObject earwig;
    public float earwigSpeed;
    public float earwigDistance;
    public float maxEarwigDistance;
    

    [Header("Speed Modifier")]
    public int stageKnotAmount;
        

    void Start()
    {
      StartCoroutine(EarwigMove()); 
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

    private void EarwigSpawn()
    {
        earwig.SetActive(true);
    }
    private IEnumerator EarwigMove()
    {
        while (true)
        {
            if (currentSpeed < earwigSpeed)
            {
                earwigDistance = Mathf.MoveTowards(earwigDistance, 0, Time.deltaTime * (earwigSpeed - currentSpeed) * 3);
                

            }
            else
            {
                earwigDistance = Mathf.MoveTowards(earwigDistance, maxEarwigDistance, Time.deltaTime * (currentSpeed - earwigSpeed)* 3);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
