using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Refrences")]
    public GameObject player;
    public GameObject bird;
    public GameObject[] waves;
    public HoleManager holes;
    public SpeedManager speed;
    public KeyCode pauseButton;
    


    [Header("Info")]
    public bool isPause;


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
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].GetComponent<WaveDeformer>().enabled = false;
        }
        Time.timeScale = 0f;
    }
    private void Unpause()
    {
        Debug.Log("UNPAUSE");
        player.GetComponent<PlayerMove>().freeze = false;
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].GetComponent<WaveDeformer>().enabled = true;
        }
        Time.timeScale = 1f;
    }
}
