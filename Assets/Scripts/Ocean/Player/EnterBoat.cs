using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBoat : MonoBehaviour
{
    [SerializeField] private LayerMask interactable;

    [Header("RefrenceObj")]

    [SerializeField] private GameObject boatCollider; // outside colliders
    [SerializeField] private GameObject boatInside; // inside boat art BG
    [SerializeField] private GameObject boatInsideCollider; // Inside boat Colliders
    [SerializeField] private GameObject outsideBoatSprite;
    [SerializeField] private GameObject propelor;
    [SerializeField] private GameObject rain;
    [SerializeField] private GameObject sunbeams;
    [SerializeField] private GameObject player; // Player Object
    [SerializeField] private GameObject playerAnimations;
    [SerializeField] private GameObject harpoon; // Harpoon Object
    [SerializeField] private GameObject earwig;

    [SerializeField] private WaveManager waves;
    [SerializeField] private HarpoonGun2 harpScript;
    [SerializeField] private FishManager fish;

    [SerializeField] private Transform enterLocation; // Locations for enter and exit 
    [SerializeField] private Transform exitLocation;
    [SerializeField] private Transform insideAnimationExitPos;
    [SerializeField] private Transform insideAnimationEnterPos;


    [SerializeField] private string insideLayer, outsideLayer; // String to change layers 

    [SerializeField] private Animator animator; // 
    [SerializeField] private Animator doorAnim; // animator of the door

    [SerializeField] private Camera cam;
    [SerializeField] private Camera secondaryCam;

    [Header("Zoom Settings")]
    public float insideZoom;
    public float zoomSpeed;


    [Header("Info")]
    public bool canEnter = true;
    public bool inBoat;
    private bool EnterCd = true;


    [Header("Audio")]
    private AudioSource audioSource; 
    [SerializeField] private AudioClip doorCreak;
    public AudioSource insideWater;
    public AudioSource insideCreak;
    public AudioSource musicPlayer;

  
    
    void Start()
    {
        player = this.gameObject;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }


    void Update()
    {
       
        float vert = Input.GetAxisRaw("Vertical");
        
   
        if (vert < 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 10,interactable);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Enter"&&canEnter && EnterCd && !harpScript.isFishing)
                {
                    StopAllCoroutines();
                    StartCoroutine(Enter());
                    //Enterone();
                   
                }
            }
        }
        if (vert >0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 10, interactable);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Exit"&&canEnter && !harpScript.isFishing)
                {
                    StopAllCoroutines();
                    ExitAnimation();
                    //Exit();
                }
            }
        }

        if(inBoat){
            fish.HideFish(false);
        }else {
            fish.HideFish(true);
        }
    }
    private IEnumerator Enter()
    {

        // for outside enter:
        // move hiding player sprite up
        //Change into 2 diffrent functions, first one starts on button down and starts animation, other plays after outside enter is done and starts inside enter


        //First function
        // private void Enter()
        //{
        EnterCd = false;
        

        // Zoom and move camera when entering boat. 
        var zoom = cam.GetComponent<CameraZoom>();
        zoom.targetZoom = insideZoom;
        zoom.targetPosition = new Vector3 (cam.transform.position.x, boatInside.transform.position.y, cam.transform.position.z);
        zoom.zoomSpeed = 2.5f;
       // secondaryCam.orthographicSize = zoom.targetZoom;
       // secondaryCam.transform.position = zoom.targetPosition;

       
        earwig.GetComponent<SpriteMask>().enabled = false;

        //Play animation for door opening
        doorAnim.SetTrigger("Open");


       // player.SetActive(false);
        
        this.GetComponent<PlayerMove>().freeze = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);

        //Audio
        audioSource.clip = doorCreak;
        audioSource.Play();
        insideCreak.Play();
        insideWater.Play();
        musicPlayer.Stop();


        //}

        yield return new WaitForSeconds(.5f); // change to when enter for outside animation done


        //Second Function

        // private void Enter()
        //{

        player.GetComponent<SpriteRenderer>().enabled = false;
        playerAnimations.SetActive(true);
        playerAnimations.GetComponent<Animator>().SetTrigger("InsideEnter");
        playerAnimations.transform.position =  insideAnimationEnterPos.position; 

        //Set stuff active/inactive for inside        
        StartCoroutine(zoom.FadeBG(true, 71));
        waves.ShowWaves(false);
        rain.SetActive(false);
        propelor.SetActive(false);
        outsideBoatSprite.SetActive(false);
        sunbeams.SetActive(false);
        inBoat = true;
        animator.SetBool("IsInside", true);
        boatCollider.SetActive(false);
        boatInside.SetActive(true);
        boatInsideCollider.SetActive(true);
        secondaryCam.enabled = false;

        // change layers to display correctly
        player.GetComponent<SpriteRenderer>().sortingLayerName= insideLayer;
        playerAnimations.GetComponent<SpriteRenderer>().sortingLayerName = insideLayer;
        harpoon.GetComponent<SpriteRenderer>().sortingLayerName = insideLayer;

        // Set Position and make sure character is facing right
        transform.position = enterLocation.position;
        playerAnimations.transform.position = enterLocation.position;
        Vector3 localScale = transform.localScale;
        if (localScale.x < 0)
        {
            localScale.x *= -1f;
        }
        transform.localScale = localScale;


        animator.SetBool("isFacingRight", true);
        animator.SetBool("Turn", false);
        this.GetComponent<PlayerMove>().isFacingRight = true;



        //}
        yield return null;
    }



    private void ExitAnimation()
    {
        player.SetActive(false);
        playerAnimations.SetActive(true);
        playerAnimations.GetComponent<Animator>().SetTrigger("InsideExit");
        playerAnimations.transform.position = insideAnimationExitPos.transform.position;
    }

    private void Exit()
    {
        player.SetActive(true);
        playerAnimations.SetActive(false);

        // Zoom and move camera when exiting boat. 
        var zoom = cam.GetComponent<CameraZoom>();
        zoom.targetZoom = zoom.ogZoom;
        zoom.targetPosition = zoom.ogPosition;
        zoom.zoomSpeed = 1f;
       //secondaryCam.orthographicSize = zoom.targetZoom;
       // secondaryCam.transform.position = zoom.targetPosition;
        StartCoroutine(zoom.FadeBG(false, 42));

        // Audio
        audioSource.clip = doorCreak;
        audioSource.Play();
        insideCreak.Stop();
        insideWater.Stop();
        musicPlayer.Play();

        //Set stuff active/inactive for outside
        EnterCd = true;
        inBoat = false;
        animator.SetBool("IsInside", false);
        propelor.SetActive(true);
        outsideBoatSprite.SetActive(true);
        rain.SetActive(true);
        sunbeams.SetActive(true);
        waves.ShowWaves(true);
        boatCollider.SetActive(true);
        boatInside.SetActive(false);
        boatInsideCollider.SetActive(false);
        secondaryCam.enabled =true;



        earwig.GetComponent<SpriteMask>().enabled = true;

        //Play door animation
        doorAnim.SetBool("Open", false);
        doorAnim.SetTrigger("Close");
        
        

        //Change layer
        player.GetComponent<SpriteRenderer>().sortingLayerName= outsideLayer;
        playerAnimations.GetComponent<SpriteRenderer>().sortingLayerName = outsideLayer;
        harpoon.GetComponent<SpriteRenderer>().sortingLayerName = outsideLayer;
      

        // move outside and make character face left
        transform.position = exitLocation.position;
        Vector3 localScale = transform.localScale;
        if (localScale.x > 0)
        {
            localScale.x *= -1f;
        }
        transform.localScale = localScale;

        animator.SetBool("isFacingRight", false);
        animator.SetBool("Turn", false);
        this.GetComponent<PlayerMove>().isFacingRight = false;

    }
   
    public void StartExit()
    {
        Exit();
    }
    
}
