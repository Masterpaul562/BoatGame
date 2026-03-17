using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpriteMask : MonoBehaviour
{
    private SpriteRenderer render;
    private SpriteMask mask;



    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        mask = GetComponent<SpriteMask>();
    }  
    void Update()
    {
        mask.sprite = render.sprite;
    }
}
