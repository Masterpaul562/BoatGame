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
    [SerializeField] private GameObject rain;
    [SerializeField] private GameObject sunbeams;
    [SerializeField] private WaveManager waves;
    [SerializeField] private GameObject player; // Player Object
    [SerializeField] private GameObject harpoon; // Harpoon Object
    [SerializeField] private HarpoonGun2 harpScript;


    [Header("")]
    [SerializeField] private Transform enterLocation; // Locations for enter and exit 
    [SerializeField] private Transform exitLocation;
     

    [SerializeField] private string insideLayer, outsideLayer; // String to change layers 

    [SerializeField] private Animator animator; // 
    [SerializeField] private Animator doorAnim; // animator of the door

    [Header("Zoom Settings")]
    [SerializeField] private Camera cam;
    public float insideZoom;
    public float zoomSpeed;


    [Header("")]
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
                    Exit();
                }
            }
        }
    }
    private IEnumerator Enter()
    {
        EnterCd = false;
        

        // Zoom and move camera when entering boat. 
        var zoom = cam.GetComponent<CameraZoom>();
        zoom.targetZoom = insideZoom;
        zoom.targetPosition = new Vector3 (cam.transform.position.x, boatInside.transform.position.y, cam.transform.position.z);
        zoom.zoomSpeed = 2.5f;



        //Play animation for door opening
        doorAnim.SetTrigger("Open");
        this.GetComponent<PlayerMove>().freeze = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);

        //Audio
        audioSource.clip = doorCreak;
        audioSource.Play();
        insideCreak.Play();
        insideWater.Play();
        musicPlayer.Stop();

        yield return new WaitForSeconds(.5f);

        this.GetComponent<PlayerMove>().freeze = false;

        //Set stuff active/inactive for inside        
        StartCoroutine(zoom.FadeBG(true, 71));
        waves.ShowWaves(false);
        rain.SetActive(false);
        sunbeams.SetActive(false);
        inBoat = true;
        animator.SetBool("IsInside", true);
        boatCollider.SetActive(false);
        boatInside.SetActive(true);
        boatInsideCollider.SetActive(true);

        // change layers to display correctly
        player.GetComponent<SpriteRenderer>().sortingLayerName= insideLayer;
        harpoon.GetComponent<SpriteRenderer>().sortingLayerName = insideLayer;

        // Set Position and make sure character is facing right
        transform.position = enterLocation.position;
        Vector3 localScale = transform.localScale;
        if (localScale.x < 0)
        {
            localScale.x *= -1f;
        }
        transform.localScale = localScale;


        animator.SetBool("isFacingRight", true);
        animator.SetBool("Turn", false);
        this.GetComponent<PlayerMove>().isFacingRight = true;

        yield return null;
    }
    private void Exit()
    {
        // Zoom and move camera when exiting boat. 
        var zoom = cam.GetComponent<CameraZoom>();
        zoom.targetZoom = zoom.ogZoom;
        zoom.targetPosition = zoom.ogPosition;
        zoom.zoomSpeed = 1f;
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
        rain.SetActive(true);
        sunbeams.SetActive(true);
        waves.ShowWaves(true);
        boatCollider.SetActive(true);
        boatInside.SetActive(false);
        boatInsideCollider.SetActive(false);

        //Play door animation
        doorAnim.SetBool("Open", false);
        doorAnim.SetTrigger("Close");
        
        //Change layer
        player.GetComponent<SpriteRenderer>().sortingLayerName= outsideLayer;
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
   
}
