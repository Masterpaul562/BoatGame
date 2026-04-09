using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonGun : MonoBehaviour
{
    [SerializeField] CameraShake camShake;
    [SerializeField] private GameObject bobber;
    [SerializeField] private GameObject harpoon;
    [SerializeField] private float harpoonPower;
    [SerializeField] private LineRenderController line;
    [SerializeField] private float distance = 100;
    [SerializeField] public Transform harpoonEnd;
    [SerializeField] private FishManager fish;
    [SerializeField] private bool canCast;
    [SerializeField] public EnterBoat enter;
    [SerializeField] private float rotSpeed;
    [SerializeField] private Transform rot1, rot2;
    private bool canRotate;
    private bool shouldFire;
    private Animator animator;
    private float horz;
    private PlayerMove freezePlayer;
    public KeyCode key;
    public bool isReeling;
    public bool isFishing;



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
        }
        else if (!enter.inBoat)
        {
            line.GetComponent<LineRenderer>().sortingLayerName = "Default";
            line.GetComponent<LineRenderer>().sortingOrder = 0;
        }
        horz = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(key))
        {
            if (canCast && horz == 0)
            {
                canCast = false;
                isFishing = true;
                enter.canEnter = false;
                freezePlayer.freeze = true;
                StartCoroutine(Harpoon());
            }


        }
        if (Input.GetKeyUp(key))
        {
            if (shouldFire)
            {
                canRotate = false;

                shouldFire = false;
                Fire();
                animator.SetTrigger("Fire");
                harpoon.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Fire");
                animator.SetBool("StowHarpoon", false);
                StopCoroutine(Harpoon());
            }
        }

        if (canRotate)
        {
            float vert = Input.GetAxisRaw("Vertical");
            if (vert < 0)
            {
                harpoon.transform.rotation = Quaternion.Slerp(harpoon.transform.rotation, rot2.rotation, Time.deltaTime * 2.5f);
            }
            else if (vert > 0)
            {
                harpoon.transform.rotation = Quaternion.Slerp(harpoon.transform.rotation, rot1.rotation, Time.deltaTime * 2.5f);
            }

        }
    }

    private IEnumerator Harpoon()
    {

        bool shouldStow = true;
        shouldFire = false;
        animator.SetTrigger("PullHarpoonOut");
        freezePlayer.canFlip = false;
        freezePlayer.horizontalInput = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        yield return new WaitForSeconds(2f);

        // holding harpoon. let go to fire. if move stow harpoon
        while (Input.GetKey(key) && horz == 0)
        {

            canRotate = true;
            shouldStow = false;          
            Debug.Log("yay");
            yield return null;
        }
        canCast = false;
        if (horz != 0 || shouldStow)
        {
            canRotate = false;
            shouldFire = false;
            animator.SetTrigger("StowHarpoon");
            yield return new WaitForSeconds(2f);
            freezePlayer.canFlip = true;
            Debug.Log("UnFreeze");
            freezePlayer.freeze = false;
            canCast = true;

            enter.canEnter = true;
            isFishing = false;
            yield return null;
        }

    }
    private void Fire()
    {
        // shoot bobber out 
        bobber.SetActive(true);
        canCast = false;
        Debug.Log("Fired");
        line.gameObject.SetActive(true);
        bobber.transform.position = transform.GetChild(0).gameObject.transform.GetChild(0).position;
        if (animator.GetBool("isFacingRight"))
        {
            bobber.GetComponent<Rigidbody2D>().AddForce(harpoon.transform.right * harpoonPower, ForceMode2D.Impulse);
        }
        else
        {
            bobber.GetComponent<Rigidbody2D>().AddForce((harpoon.transform.right * harpoonPower) * -1, ForceMode2D.Impulse);
        }
        bobber.GetComponent<Rigidbody2D>().simulated = true;



    }

    public IEnumerator ReelIn()
    {
        Debug.Log("StartReelIn");
        isReeling = true;
        bobber.GetComponent<Rigidbody2D>().simulated = false;
        distance = 100;
        while (distance > 1f)
        {
            Vector2 pos = Vector2.MoveTowards(bobber.transform.position, harpoonEnd.position, Time.deltaTime * 20);
            bobber.transform.position = pos;
            distance = Vector2.Distance(harpoonEnd.position, bobber.transform.position);
            yield return null;
        }
        animator.SetTrigger("StowHarpoon");
        bobber.SetActive(false);
        line.gameObject.SetActive(false);
        bobber.GetComponent<Bobber>().rb.simulated = true;
        shouldFire = false;
        //fish.SecureFish();

        yield return new WaitForSeconds(1f);
        freezePlayer.freeze = false;
        isFishing = false;
        Debug.Log("CAnCast");
        canCast = true;
        enter.canEnter = true;
        isReeling = false;
        animator.SetBool("Turn", false);
        freezePlayer.canFlip = true;
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
        harpoon.transform.rotation = rotation;
        shouldFire = true;
    }
    public void DeactiveHarpoon()
    {
        harpoon.SetActive(false);
        shouldFire = false;
    }

}
