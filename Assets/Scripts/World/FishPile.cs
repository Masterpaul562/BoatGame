using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPile : MonoBehaviour
{
    [SerializeField] private FishInventory fishCount;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Sprite[] fishPile;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = null;
    }
    private void Update()
    {
        if (fishCount.fishAmountOutside > 0)
        {
            if(fishCount.fishAmountOutside < 8)
            {
                sprite.sprite = fishPile[fishCount.fishAmountOutside - 1];
            }
            else
            {
                sprite.sprite = fishPile[7];
            }
        }
    }
}
