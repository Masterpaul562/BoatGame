using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCamera : MonoBehaviour
{
    public Transform target; //Location to pan to
    public Camera cam; // Cam to move
    public float speed; // speed of pan
   [SerializeField] private bool pan; // should pan or not
    private Vector3 camOGpos; // position to reset to
    [SerializeField] private bool moveY; // if it changes the cams y position
    public PanCamera otherPan;

    private void Start()
    {
        // sets target's y same as cam so it doesn't move y
        if (!moveY)
        {
            target.position = new Vector2(target.position.x, cam.transform.position.y);
        }
        target.position = new Vector3(target.position.x, target.position.y, cam.transform.position.z);
        camOGpos = cam.transform.position;
    }

    private void Update()
    {
        if (this.pan)
        {
            Pan();
        }
    }
    private void Pan()
    {
        float newPosition = Mathf.MoveTowards(cam.transform.position.x, target.position.x,Time.deltaTime* speed);
        //cam.transform.position = Vector2.MoveTowards(cam.transform.position,target.position, Time.deltaTime * speed);
        cam.transform.position = new Vector3(newPosition, cam.transform.position.y, camOGpos.z);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            // camOGpos = cam.transform.position;
            otherPan.StopCoroutine();
            StopAllCoroutines();
            pan = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            pan = false;
            StopAllCoroutines();
            StartCoroutine(Reset());
        }
    }
    private IEnumerator Reset()
    {
        while (cam.transform.position.x != camOGpos.x)
        {
            float reset  = Mathf.MoveTowards(cam.transform.position.x, camOGpos.x, Time.deltaTime * speed);
            cam.transform.position = new Vector3(reset, cam.transform.position.y, camOGpos.z);
            yield return null;
        }
    }
    private void StopCoroutine()
    {
        StopAllCoroutines();
    }
}
