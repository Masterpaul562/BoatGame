using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonGun : MonoBehaviour
{
    public KeyCode key;
    [SerializeField] CameraShake camShake;
    private Animator animator;
    private float horz;
    private PlayerMove freezePlayer;
    [SerializeField] private GameObject bobber;
    [SerializeField] private GameObject harpoon;
    [SerializeField] private float harpoonPower;
    [SerializeField] private LineRenderController line;
    private bool shouldFire;
    private bool noFire;
    public bool isFishing;
    [SerializeField] private float distance = 100;
    [SerializeField] public Transform harpoonEnd;
    [SerializeField] private FishManager fish;
    [SerializeField] private bool canCast;
    [SerializeField] private bool canReel = true;
    [SerializeField] public EnterBoat enter;
    [SerializeField] private FollowBobber cam;
    [SerializeField] private float rotSpeed;
    [SerializeField] private Transform rot1,rot2;
    private bool canRotate;



    private void Start()
    {
        animator = GetComponent<Animator>();
        freezePlayer = GetComponent<PlayerMove>();
        enter = GetComponent<EnterBoat>();
       
    }


    private void Update()
    {
        if (enter.inBoat)
        {
            line.GetComponent<LineRenderer>().sortingLayerName = "Inside";
            line.GetComponent<LineRenderer>().sortingOrder = 1;
        }else if (!enter.inBoat)
        {
            line.GetComponent<LineRenderer>().sortingLayerName = "Default";
            line.GetComponent<LineRenderer>().sortingOrder = 0;
        }
        horz = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(key))
        {
            if (!isFishing && canCast && horz == 0)
            {               
                canCast = false;
                enter.canEnter = false;
                freezePlayer.freeze = true;
                StartCoroutine(Harpoon());
            }
            else if (isFishing && !canCast && canReel)
            {
                canReel = false;
                StartCoroutine(ReelIn());
            }

        }
        if (Input.GetKeyUp(key))
        {
            if (shouldFire)
            {
                canRotate = false;
                noFire = false;
                shouldFire = false;
                Fire();
                animator.SetTrigger("Fire");
                harpoon.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Fire");
                harpoonEnd.position = new Vector2(harpoonEnd.transform.position.x - 0.1f, line.transform.position.y);
                animator.SetBool("StowHarpoon", false);
                StopCoroutine(Harpoon());
            }
        }

        if(canRotate)
        {
         float vert = Input.GetAxisRaw("Vertical");
           // if(vert <0 ){
          //      harpoon.transform.rotation = Quaternion.Slerp(harpoon.transform.rotation,rot2.rotation,Time.deltaTime);
           //     Debug.Log("Down");
          //  } else if (vert >0 ) {
          //      harpoon.transform.rotation = Quaternion.Slerp(harpoon.transform.rotation,rot1.rotation,Time.deltaTime);
         //       Debug.Log("up");
         //   }
         
            float currentZ = harpoon.transform.rotation.eulerAngles.z;
            float newRotation = currentZ + vert * rotSpeed;
            Debug.Log(newRotation);
            Mathf.Clamp(newRotation,-30,40);
            harpoon.transform.rotation = Quaternion.Euler(0,0,newRotation);
        }
    }

    private IEnumerator Harpoon()
    {
        noFire = true;
        bool shouldStow = true;
        shouldFire = false;
        animator.SetTrigger("PullHarpoonOut");

        freezePlayer.horizontalInput = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        yield return new WaitForSeconds(1f);

        while (Input.GetKey(key) && horz == 0)
        {
            //Debug.Log("Charching");         
            //harpoon.transform.Rotate(0,0,vert*rotSpeed);
            //Vector3 rot = harpoon.transform.rotation.eulerAngles;
            //harpoon.transform.rotation = Quaternion.Euler(rot);
            canRotate = true;
            shouldStow = false;
            shouldFire = true;
            if (harpoonPower < 8)
            {
                harpoonPower++;
            }
            yield return new WaitForSeconds(.3f); ;
        }
        canCast = false;
        yield return new WaitForSeconds(.2f);
        if (horz != 0 && noFire || shouldStow)
        {
           canRotate = false;
            shouldFire = false;
            animator.SetTrigger("StowHarpoon");
            yield return new WaitForSeconds(1f);
            Debug.Log("UnFreeze");
            freezePlayer.freeze = false;
            canCast = true;
            enter.canEnter = true;
            cam.shouldMove = false;
            yield return null;

        }
    }
    private void Fire()
    {
        bobber.SetActive(true);
        cam.shouldMove = true;
        canCast = false;
        Debug.Log("Fired");
        line.gameObject.SetActive(true);
        bobber.GetComponent<Floater>().enabled = false;
        bobber.transform.position = transform.GetChild(0).gameObject.transform.GetChild(0).position;
        if (animator.GetBool("isFacingRight"))
        {
            bobber.GetComponent<Rigidbody2D>().AddForce(harpoon.transform.right*harpoonPower, ForceMode2D.Impulse);
        }
        else
        {
            bobber.GetComponent<Rigidbody2D>().AddForce((harpoon.transform.right * harpoonPower)*-1, ForceMode2D.Impulse);
        }
        bobber.GetComponent<Rigidbody2D>().simulated = true;

        harpoonPower = 1;

    }

    public IEnumerator ReelIn()
    {
        Debug.Log("StartReelIn");

        bobber.GetComponent<Rigidbody2D>().simulated = false;
        distance = 100;
        fish.HookFish();
        while (distance > 0.1f)
        {

            Vector2 pos = Vector2.MoveTowards(bobber.transform.position, harpoonEnd.position, Time.deltaTime * 10);
            bobber.transform.position = pos;
            distance = Vector2.Distance(harpoonEnd.position, bobber.transform.position);
            yield return null;
        }
        bobber.SetActive(false);
        line.gameObject.SetActive(false);
        bobber.GetComponent<Bobber>().rb.simulated = true;
        bobber.GetComponent<Bobber>().submerged = false;
        isFishing = false;
        freezePlayer.freeze = false;
        shouldFire = false;
        if (fish.canHook)
        {
            Debug.Log("catch");
            fish.SecureFish();
        }
        animator.SetTrigger("StowHarpoon");
        cam.shouldMove = false;
        yield return new WaitForSeconds(1f);
        Debug.Log("CAnCast");
        canCast = true;
        canReel = true;
        enter.canEnter = true;
        fish.StopSwimmingToBobber();
        yield return null;

    }
    public void StartReel()
    {
        StartCoroutine(ReelIn());
    }
    public void ActiveHarpoon()
    {
        harpoon.SetActive(true);
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(0, 0, 0);
        harpoon.transform.rotation =rotation;
    }
    public void DeactiveHarpoon()
    {
        harpoon.SetActive(false);
    }

}
