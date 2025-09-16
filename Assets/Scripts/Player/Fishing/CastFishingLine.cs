using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastFishingLine : MonoBehaviour
{

    [SerializeField] private float power;
    [SerializeField] private float step;
    [SerializeField] private float maxPower;
    [SerializeField] private Transform[] points;
    [SerializeField] private LineRenderController line;
    [SerializeField] public GameObject bobber;
    [SerializeField] private EnterFishing enterFish;
    [SerializeField] private GameObject bobberHolder;
    public bool hasCast;


   
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
         if(!enterFish.isFishing&& hasCast){
        Vector2 pos = Vector2.MoveTowards(bobber.transform.position, this.transform.position, Time.deltaTime * 10);
        bobber.transform.position = pos;
        float distance = Vector2.Distance(transform.position,bobber.transform.position);
        if(distance <1){
            bobber.SetActive(false);
            line.gameObject.SetActive(false);
            bobber.GetComponent<Bobber>().rb.simulated = true;
            hasCast = false;
        }
      }
    }


    private void FixedUpdate()
    {
        power = Mathf.PingPong(Time.time/3f, maxPower);    
     
    }

    private void Cast()
    {
        bobber.transform.position = new Vector2(transform.position.x,transform.position.y+0.1f);
        line.gameObject.SetActive(false);
        hasCast = true;
        line.enabled = true;
        bobber.SetActive(true);
       // transform.GetChild(0).transform.GetChild(2).transform.position = Vector2.zero;
        bobberHolder.transform.parent = null;
       // bobber.transform.parent = null;
        line.SetUpLine(points);
        bobber.GetComponent<Rigidbody2D>().AddForce(new Vector2(-3, 1.5f) * 3,ForceMode2D.Impulse);
        
    }
  
}
