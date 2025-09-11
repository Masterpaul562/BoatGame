using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fishing : MonoBehaviour
{

    [SerializeField] private float power;
    [SerializeField] private float step;
    [SerializeField] private float maxPower;
    [SerializeField] private Transform[] points;
    [SerializeField] private LineRenderController line;
    [SerializeField] private GameObject bobber;
    [SerializeField] private EnterFishing enterFish;
    private bool hasCast;


   
    private void Awake()
    {
        enterFish = GetComponent<EnterFishing>();
    }

    private void Update ()
    {
        if (enterFish.isFishing)
        {
            if (Input.GetKeyDown(KeyCode.E)&&!hasCast)
            {
                Cast();
            }
        }
    }


    private void FixedUpdate()
    {
        power = Mathf.PingPong(Time.time/3f, maxPower);    
      
    }

    private void Cast()
    {
        hasCast = true;
        line.enabled = true;
        bobber.SetActive(true);
       // transform.GetChild(0).transform.GetChild(2).transform.position = Vector2.zero;
        transform.GetChild(0).transform.GetChild(2).transform.parent = null;
       // bobber.transform.parent = null;
        line.SetUpLine(points);
        bobber.GetComponent<Rigidbody2D>().AddForce(new Vector2(-3, 1.5f) * 3,ForceMode2D.Impulse);
        
    }
  
}
