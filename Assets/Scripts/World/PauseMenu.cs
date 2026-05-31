using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Refrences")]
    public GameObject player;
    public GameObject bird;
   
    public HoleManager holes;
    public SpeedManager speed;
    public HarpoonGun2 harp;
    public KeyCode pauseButton;

    [SerializeField] private KeyCode ogFire;
    [SerializeField] private KeyCode ogInput;
    


    [Header("Info")]
    public bool isPause;



    private void Start()
    {
        ogFire = harp.fireKey;
        ogInput = harp.inputKey;
    }
    private void Update()
    {
        if (Input.GetKeyDown(pauseButton))
        {
            if (!isPause)
            {
                isPause = !isPause;
                Pause();
            }else 
            {
                isPause = !isPause;
                Unpause();
            }
        }
    }


    private void Pause()
    {
        Debug.Log("PAUSE");
        player.GetComponent<PlayerMove>().freeze = true;
        harp.fireKey = KeyCode.None;
        harp.inputKey = KeyCode.None;
        holes.fixCD = true;
       
        Time.timeScale = 0f;
    }
    private void Unpause()
    {
        Debug.Log("UNPAUSE");
        player.GetComponent<PlayerMove>().freeze = false;
        harp.fireKey = ogFire;
        harp.inputKey = ogInput;
        holes.fixCD = false;

        Time.timeScale = 1f;
    }
}
