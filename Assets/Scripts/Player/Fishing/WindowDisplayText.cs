using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WindowDisplayText : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private FishInventory fishAmount;

   void Awake()
    {
        text.text = "Fish Pile";
    }
    void Update()
    {
        text.text = "Fish Pile: " + fishAmount.fishAmountOutside;
    }
}
