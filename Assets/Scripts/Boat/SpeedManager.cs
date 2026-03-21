using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    public float currentSpeed;
    public FishEngineReal engine;


    [Header("Earwig")]
    [SerializeField] private GameObject earwig;
    [SerializeField] private Transform spawnPos;
    private bool earwigSpawned;
    public float earwigSpeed;
    public float earwigDistance;
    public float maxEarwigDistance;
    

    [Header("Speed Modifier")]
    public int stageKnotAmount;
        

    void Start()
    {
      StartCoroutine(EarwigMove());
        earwigSpawned = false;
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
        earwig.transform.position = spawnPos.position;
        earwigSpawned = true;
    }
    private IEnumerator EarwigMove()
    {
        while (true)
        {
            if (currentSpeed < earwigSpeed)
            {
                //Moving earwig towards boat
                earwigDistance = Mathf.MoveTowards(earwigDistance, 0, Time.deltaTime * (earwigSpeed - currentSpeed) * 3);
                if(earwigDistance < 50 && !earwigSpawned)
                {
                    EarwigSpawn();                   
                }
                if (earwigSpawned)
                {
                    earwig.transform.position = Vector3.MoveTowards(earwig.transform.position, Vector2.right, Time.deltaTime*(earwigSpeed - currentSpeed)*3);
                    
                }

            }
            else
            {
                //Moving away from earwig
                earwigDistance = Mathf.MoveTowards(earwigDistance, maxEarwigDistance, Time.deltaTime * (currentSpeed - earwigSpeed)* 3);
                earwig.transform.position = Vector3.MoveTowards(earwig.transform.position, spawnPos.position, Time.deltaTime * (currentSpeed - earwigSpeed)*3);
                if (earwigDistance > 50 && earwigSpawned)
                {
                    earwig.SetActive(false);
                    earwigSpawned = false;
                }
               
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

}
