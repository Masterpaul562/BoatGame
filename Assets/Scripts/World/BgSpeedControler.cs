using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgSpeedControler : MonoBehaviour
{
    [SerializeField] private BgScroller[] backGrounds; 
    [SerializeField] private EnterFishing enterFishing;
   [SerializeField] private float[] lerpTarget;
   [SerializeField] private float[] ogPos;
    public bool inEvent;

    private void Start(){
        for(int i =0; i<lerpTarget.Length;i++){
        ogPos[i] = backGrounds[i].direction; 
        lerpTarget[i] = backGrounds[i].direction; 
        lerpTarget[i] /= 3.3f;
        }
    }
   

    public IEnumerator SpeedUpOcean()
    {

        for (int i = 0; i < backGrounds.Length; i++)
        {
            while(backGrounds[i].direction < ogPos[i] - 0.001f)
            {
                backGrounds[i].direction = Mathf.Lerp(backGrounds[i].direction, ogPos[i], Time.deltaTime);
                yield return null;
            }
            yield return null;
        }
    }
    public IEnumerator SlowDownOcean()
    {
        for (int i = 0; i < backGrounds.Length; i++)
        {
            while (backGrounds[i].direction > lerpTarget[i] + 0.001f)
            {
                Debug.Log("yay");
                backGrounds[i].direction = Mathf.Lerp(backGrounds[i].direction,lerpTarget[i], Time.deltaTime);
                float yay = backGrounds[i].direction = Mathf.Lerp(backGrounds[i].direction, lerpTarget[i], Time.deltaTime);
                Debug.Log(yay + "   " + i);
                yield return null;
            }
            yield return null;
        }
    }
}
