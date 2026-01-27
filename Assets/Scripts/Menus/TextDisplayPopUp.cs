using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class TextDisplayPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    
    public string textToDisplay;

   void Awake()
    {
        text = GetComponent<TextMeshPro>();
        text.text =textToDisplay;
    }
    void Update()
    {
        text.text = textToDisplay;
    }
    
}
