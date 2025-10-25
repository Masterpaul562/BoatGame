using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : MonoBehaviour
{
    public GameObject currentCity;
    [SerializeField] JunkSpawner junk; 
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;
    [SerializeField] private int camSize;
    [SerializeField] private FishManager fishManager;
   [SerializeField] public bool justEnteredCity = true;
    public bool inCity;
    public bool shouldZoom;
    [SerializeField] private bool startCo = true;

    private void Update()
    {
       // Debug.Log(FindDistance());

        if (currentCity != null)
        {
            if (currentCity.GetComponent<FloaterMovement>().DestroyThis() == true)
            {
                OutsideCity();
                if( cam.orthographicSize < 12)
                {
                    Destroy(currentCity );
                    justEnteredCity = true;
                    startCo = true;
                }
            }
            else
            if (FindDistance() < 50)
            {
                InCity();
            }
            
        }
    }

    private float FindDistance()
    {
        if (currentCity != null)
        {

            return Vector2.Distance(currentCity.transform.position, player.transform.position);

        }
        else
        {
            return 10000000;
        }
    }
    private void InCity()
    {
        camSize = 18;
        player.GetComponent<EnterBoat>().shouldZoom = false;
        if (shouldZoom)
        {
            if (justEnteredCity)
            {
                cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, camSize, Time.deltaTime);
                if(cam.orthographicSize == 18)
                {
                    justEnteredCity = false;
                }
            }
            else
            {
                cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, camSize, Time.deltaTime*10);
            }
        }
        inCity = true;
        if(startCo)
        {
 
            startCo = false;
            junk.shouldBeSpawning = true;
            StartCoroutine(junk.Spawning());
        }
    }
    private void OutsideCity()
    {
        camSize = 11;
        
        if (shouldZoom)
        {
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, camSize, Time.deltaTime);
        }
        junk.shouldBeSpawning = false;  
        inCity = false;
    }
}
