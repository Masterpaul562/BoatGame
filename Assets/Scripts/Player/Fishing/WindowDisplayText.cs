using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WindowDisplayText : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    
    public string textToDisplay;

   void Awake()
    {
        text.text =textToDisplay;
    }
    void Update()
    {
        text.text = textToDisplay;
    }


}
