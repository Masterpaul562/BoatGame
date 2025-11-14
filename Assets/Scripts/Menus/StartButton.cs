using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private char screens;

    void Start() {
        screens ='s';
    }
    void Update()
    {
        switch(screens){
            case 'p':
         SceneManager.LoadScene("Ocean");
         break;


        }
    }
    
    private void OnMouseDown(){
        screens = 'p';       
    }
}
