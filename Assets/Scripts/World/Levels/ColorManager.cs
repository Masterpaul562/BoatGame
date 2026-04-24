using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private Color currentColor;

    [Header("LevelColors")]
    public Color[] industrialZoneColor;
    public Color[] cityColor;
    public Color[] theProjectColor;
    public Color[] infestedZone;

    [Header("ObjectsToChange")]
    public SpriteRenderer[] objects;
    public Material[] waves;

    [Header("Settings")]
    public float shiftSpeed;



    private void Update()
    {
        ColorShift(industrialZoneColor[0]);
        waves[1].color = currentColor;
    }

    public void ColorShift(Color targetColor)
    {
        Vector3 color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
        Vector3 newColor = new Vector3(targetColor.r, targetColor.g, targetColor.b);
        color = Vector3.MoveTowards(color, newColor, Time.deltaTime * shiftSpeed);
        currentColor = new Color(color.x,color.y,color.z);
    }
}
