using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : MonoBehaviour
{
    public GameObject currentCity;
    [SerializeField] GameObject player;
    private void Update()
    {
        Debug.Log(FindDistance());
    }

    private float FindDistance()
    {
        if (currentCity!=null) 
        {
        
        return Vector2.Distance(currentCity.transform.position, player.transform.position);
        
        }else
        {
            return 0;
        }
    }
}
