using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesForReal : MonoBehaviour
{
    [SerializeField] float amp,wavelength;
   
    void Update()
    {
        float y = amp*Mathf.Sin(transform.position.x/wavelength-Time.deltaTime/wavelength);
        transform.position = new Vector3(transform.position.x,y,transform.position.z);
    }
}
