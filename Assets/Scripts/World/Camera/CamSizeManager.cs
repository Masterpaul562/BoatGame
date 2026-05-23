using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSizeManager : MonoBehaviour
{
    public float aspect,worldHeight,worldWidth;
    public float gameSpaceBottom, gameSpaceTop;


        private void Update()
    {
        aspect = (float)Screen.width / Screen.height;
        worldHeight = GetComponent<Camera>().orthographicSize * 2;
        worldWidth = worldHeight * aspect;
        gameSpaceBottom = (worldHeight/2) - transform.position.y;
        gameSpaceTop = (worldHeight / 2) + transform.position.y;
    }

}
