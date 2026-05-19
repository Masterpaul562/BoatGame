using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] SetupOcean setter;
    [SerializeField] LevelManager level;
    [SerializeField] SceneSpawner spawner;
    [SerializeField] FishEngineReal engine;
    [SerializeField] SpeedManager speed;
    [SerializeField] HoleManager hole;
    [SerializeField] Albatross bird;
    [SerializeField] PanCamera panLeft;
    [SerializeField] PanCamera panRight;

    [SerializeField] GameObject boat;
    private Transform ogBoatPos;

    private void Start()
    {
        ogBoatPos = boat.transform;
    }






    public IEnumerator RestartLevel(float waitTime, bool shouldWait)
    {
        if (shouldWait)
        {
            yield return new WaitForSeconds(waitTime);
        }

        boat.transform.position = ogBoatPos.position;

        Quaternion rotation = Quaternion.Euler(0f, 0f, ogBoatPos.transform.rotation.z);
        boat.transform.rotation = rotation;
        yield return null;

        panLeft.StopCoroutine();
        panRight.StopCoroutine();
        hole.Restart();
        setter.SetScene();
        level.Restart();
        spawner.Restart();
        engine.Restart();
        speed.Restart();
        bird.Restart();
       

       

    }
}
