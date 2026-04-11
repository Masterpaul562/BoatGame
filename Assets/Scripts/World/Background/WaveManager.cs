using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]  private GameObject[] waves;
    

    private void Start()
    {
        waves = new GameObject[this.transform.childCount];

        for(int i = 0; i < waves.Length; i++)
        {
            waves[i] = this.transform.GetChild(i).gameObject;
        }
    }



    public void ShowWaves(bool show)
    {
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].GetComponent<MeshRenderer>().enabled = show;
        }
    }


}
