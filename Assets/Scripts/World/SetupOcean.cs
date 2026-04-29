using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupOcean : MonoBehaviour
{
    [SerializeField] private GameObject player; // Player Object
    [SerializeField] private GameObject harpoon; // Harpoon Object
    [SerializeField] private GameObject boatCollider; // outside colliders
    [SerializeField] private GameObject boatInside; // inside boat art BG
    [SerializeField] private GameObject insideBG;
    [SerializeField] private GameObject boatInsideCollider;// Inside boat Colliders
    [SerializeField] private GameObject outsideBoatSprite;
    [SerializeField] private GameObject propelor;
    [SerializeField] private GameObject rain;
    [SerializeField] private GameObject sunbeams;
    [SerializeField] private GameObject wakeUp;
    [SerializeField] private WaveManager waves;
    [SerializeField] private HarpoonGun2 harpScript;
    [SerializeField] private Camera cam;
    
    

    [Header("Audio")]
    public AudioSource insideWater;
    public AudioSource insideCreak;
    public AudioSource musicPlayer;

    private bool awakened = false;
    private Animator anim;



    private void Start()
    {
        SetScene();
        anim = wakeUp.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.anyKey && !awakened)
        {
            awakened = true;
            anim.SetTrigger("WakeUp");
        }
    }

    private void SetScene()
    {
        player.SetActive(false);
        boatCollider.SetActive(false);
        propelor.SetActive(false);
        outsideBoatSprite.SetActive(false);
        rain.SetActive(false);
        sunbeams.SetActive(false);
        waves.ShowWaves(false);


        boatInside.SetActive(true);
        boatInsideCollider.SetActive(true);
        wakeUp.SetActive(true);
        insideBG.GetComponent<SpriteRenderer>().enabled = true;

        cam.orthographicSize = player.GetComponent<EnterBoat>().insideZoom;
        cam.transform.position = new Vector3(cam.transform.position.x, boatInside.transform.position.y, cam.transform.position.z);
        //var zoom = cam.GetComponent<CameraZoom>();
        //zoom.targetZoom = 4.7f;
        //zoom.targetPosition = new Vector3(cam.transform.position.x, boatInside.transform.position.y, cam.transform.position.z);
        //zoom.zoomSpeed = 2.5f;

        insideCreak.Play();
        insideWater.Play();
        musicPlayer.Stop();


        player.GetComponent<SpriteRenderer>().sortingLayerName = "Inside";
        harpoon.GetComponent<SpriteRenderer>().sortingLayerName = "Inside";
    }

    public void WakeUp()
    {
        wakeUp.SetActive(false);
        player.SetActive(true);
        player.transform.position = wakeUp.transform.GetChild(0).position;

        var playerAnim = player.GetComponent<Animator>();

        playerAnim.SetBool("isFacingRight", false);
        playerAnim.SetBool("Turn", false);
        playerAnim.SetBool("IsInside", true);

        player.GetComponent<PlayerMove>().isFacingRight = false;
        player.GetComponent<EnterBoat>().inBoat = true;

    }

}
