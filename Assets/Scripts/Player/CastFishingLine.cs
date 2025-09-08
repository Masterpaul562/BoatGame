using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fishing : MonoBehaviour
{

    [SerializeField] private float power;
    [SerializeField] private float step;
    [SerializeField] private float maxPower;
    [SerializeField] private GameObject bar;

    void Start()
    {

 //       StartCoroutine(Increase());
    }


    private void FixedUpdate()
    {
        power = Mathf.PingPong(Time.time/3f, maxPower);    
        bar.transform.localScale = new Vector3(power*10,1,0);
    }
   // private IEnumerator Increase()
   // {
     //   bool yeah = true;
     //   while (yeah)
   //     {
     //       power += 1;
     //       slider.value = power;
    //        yield return new WaitForSeconds(1f);
    //
     //   }
  //  }
}
