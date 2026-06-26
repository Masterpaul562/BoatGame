using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSounds : MonoBehaviour
{
    public AudioClip insideRain;
    public AudioClip outsideRain;
    public AudioSource source;
    public EnterBoat enter;

    public bool switched;


    void Update()
    {
        if (enter.inBoat && !switched)
        {
            switched = true;
            source.clip = insideRain;
            source.Play();
            
        }
        else if(!enter.inBoat && !switched)
        {
            switched = true;
            source.clip = outsideRain;
            source.Play();
        }
    }

    public void Restart()
    {
        source.clip = insideRain;  
        source.Play();
    }
}
