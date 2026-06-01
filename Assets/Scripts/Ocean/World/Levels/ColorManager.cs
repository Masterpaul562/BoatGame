using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private Color[] currentColor;

    [Header("LevelPalettes")]
    public Color[] industrialZonePalette;
    public Color[] cityPalette;
    public Color[] theProjectPalette;
    public Color[] infestedPalette;

    [Header("ObjectsToChange")]
    public SpriteRenderer[] objects;
    public Material[] waves;

    [Header("Settings")]
    public float shiftSpeed;

    private void Start()
    {
        currentColor = new Color[objects.Length];
    }

    private void Update()
    {
        for (int i = 0; i < objects.Length; i++)
        {
           objects[i].color = ColorShift(industrialZonePalette[i],i);
        }
        
    }

    public Color ColorShift(Color targetColor, int index)
    {
        Vector3 color = new Vector3(currentColor[index].r, currentColor[index].g, currentColor[index].b);
        Vector3 newColor = new Vector3(targetColor.r, targetColor.g, targetColor.b);
        color = Vector3.MoveTowards(color, newColor, Time.deltaTime * shiftSpeed);
        currentColor[index] = new Color(color.x, color.y, color.z);
        Color returnColor = new Color(color.x,color.y,color.z);

        return returnColor;
    }
}
