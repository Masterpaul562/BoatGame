using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
   [SerializeField] private float waveMax;
   [SerializeField] private float waveMin;
   [SerializeField] private Vector2 wave;

   private void FixedUpdate()
    { 
          // float wave = (Mathf.PingPong(Time.time, waveMax));
          // Debug.Log((Mathf.PingPong(Time.time, waveMax)));
            wave = Vector2.Lerp(new Vector2(0,waveMin),new Vector2(0,waveMax),Time.deltaTime);
         transform.position = Vector2.Lerp(transform.position,new Vector2 (-0.25f,Mathf.PingPong(Time.time/10, wave.y)),Time.deltaTime);
    }
}
