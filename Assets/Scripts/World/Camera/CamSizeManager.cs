using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSizeManager : MonoBehaviour
{
    public float aspect,worldHeight,worldWidth;


        private void Update()
    {
        aspect = (float)Screen.width / Screen.height;
        worldHeight = GetComponent<Camera>().orthographicSize * 2;
        worldWidth = worldHeight * aspect;

    }

}
