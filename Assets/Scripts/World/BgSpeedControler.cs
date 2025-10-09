using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgSpeedControler : MonoBehaviour
{
    [SerializeField] private BgScroller[] backGrounds; 
    [SerializeField] private EnterFishing enterFishing;
   [SerializeField] private float[] lerpTarget;
   [SerializeField] private float[] ogPos;

    private void Start(){
        for(int i =0; i<lerpTarget.Length;i++){
        ogPos[i] = backGrounds[i].direction; 
        lerpTarget[i] = backGrounds[i].direction; 
        lerpTarget[i] /= 3.3f;
        }
    }
    private void Update() {
        if(enterFishing.isFishing) {
            SlowOceanDown();

        }else if(enterFishing.isFishing == false){
            SpeedOceanUp();
        }


    }
   
     private void SlowOceanDown() {

          for(int i = 0; i < backGrounds.Length;i++){

            
           //lerpTarget[i]/=3.3f;

            backGrounds[i].direction = Mathf.Lerp(backGrounds[i].direction,lerpTarget[i],Time.deltaTime);
        }
        
            
        }
private void SpeedOceanUp(){

 for(int i = 0; i < backGrounds.Length;i++){

            //float lerpTarget = backGrounds[i].direction;
           // lerpTarget[i]*=3.3f;

            backGrounds[i].direction = Mathf.Lerp(backGrounds[i].direction,ogPos[i],Time.deltaTime);
        }


    }
}
