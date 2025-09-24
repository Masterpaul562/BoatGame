using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastFishingLine : MonoBehaviour
{

  //  [SerializeField] private float power;
    [SerializeField] private float step;
    //[SerializeField] private float maxPower;
    [SerializeField] private Transform[] points;
    [SerializeField] private LineRenderController line;
    [SerializeField] public GameObject bobber;
    [SerializeField] private EnterFishing enterFish;
    [SerializeField] private GameObject bobberHolder;
    public bool hasCast;
    public bool canCast;
    public bool shouldReel;



   
    private void Awake()
    {
        enterFish = GetComponent<EnterFishing>();
    }

    private void Update ()
    {
        if (enterFish.isFishing)
        {
            if (Input.GetKeyDown(KeyCode.Z)&&!hasCast && canCast)
            {
                Cast();
            }
        }
        if (shouldReel && hasCast)
        {
            bobber.GetComponent<Bobber>().rb.simulated = false;
            ExitFishingReel();
        }
    }


    private void FixedUpdate()
    {
       // power = Mathf.PingPong(Time.time/3f, maxPower);    
     
    }

    private void Cast()
    {
        
        bobber.transform.position = new Vector2(transform.position.x,transform.position.y+0.1f);
        line.gameObject.SetActive(true);
        hasCast = true;
        canCast = false;
        line.enabled = true;
        bobber.SetActive(true);
        bobberHolder.transform.parent = null;      
        line.SetUpLine(points);
        bobber.GetComponent<Rigidbody2D>().AddForce(new Vector2(-3, 2f) * 3,ForceMode2D.Impulse);
        
    }
    
    public void ExitFishingReel()
    {      
           
            Vector2 pos = Vector2.MoveTowards(bobber.transform.position, this.transform.position, Time.deltaTime * 10);
            bobber.transform.position = pos;
            float distance = Vector2.Distance(transform.position, bobber.transform.position);
            if (distance < 1)
            {
                bobber.SetActive(false);
                line.gameObject.SetActive(false);
                bobber.GetComponent<Bobber>().rb.simulated = true;
                hasCast = false;
                shouldReel = false;
                canCast = true;
            }

        
    }

}
