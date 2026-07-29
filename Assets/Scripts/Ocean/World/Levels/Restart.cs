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
    [SerializeField] HarpoonGun2 gun;
    [SerializeField] CameraZoom cam;
    [SerializeField] RainSounds rain;
    [SerializeField] SeamineSpawner mines;

    [SerializeField] GameObject boat;
    public Vector3 ogBoatPos;
    public Vector3 ogBoatRot;

    private void Start()
    {
        ogBoatPos = boat.transform.position;
        ogBoatRot = boat.transform.rotation.eulerAngles;
    }






    public IEnumerator RestartLevel(float waitTime, bool shouldWait)
    {
        if (shouldWait)
        {
            yield return new WaitForSeconds(waitTime);
        }

        cam.drown = false;
        boat.transform.position = ogBoatPos;

        Quaternion rotation = Quaternion.Euler(0f, 0f, ogBoatPos.z);
        boat.transform.rotation = rotation;
        yield return null;

        panLeft.StopCoroutine();
        panRight.StopCoroutine();
        gun.Restart();
        hole.Restart();
        setter.SetScene();
        level.Restart();
        spawner.Restart();
        engine.Restart();
        speed.Restart();
        bird.Restart();
        rain.Restart();
        mines.Restart();
       

       

    }
}
