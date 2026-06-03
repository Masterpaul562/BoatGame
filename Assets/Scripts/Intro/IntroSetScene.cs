using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSetScene : MonoBehaviour
{
    public GameObject player;
    public Transform playerStartPos;
    public GameObject wakeUp;


    private void Start()
    {
        player.SetActive(false);
        player.transform.position = playerStartPos.position;
        wakeUp.SetActive(true);
        wakeUp.transform.position = playerStartPos.position;
        wakeUp.GetComponent<PlayerWakeUp>().enabled = true;
    }
}
