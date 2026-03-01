using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCamera : MonoBehaviour
{
    public Transform target; //Location to pan to
    public Camera cam; // Cam to move
    public float speed; // speed of pan
    private bool pan;
    private Vector3 camOGpos;
    [SerializeField] private bool moveY;

    private void Start()
    {
        if (!moveY)
        {
            target.position = new Vector2(target.position.x, cam.transform.position.y);
        }
        target.position = new Vector3(target.position.x, target.position.y, cam.transform.position.z);
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
        cam.transform.position = Vector2.MoveTowards(cam.transform.position,target.position, Time.deltaTime * speed);
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, camOGpos.z);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            camOGpos = cam.transform.position;
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
        while (cam.transform.position != camOGpos)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, camOGpos, Time.deltaTime * speed);
            yield return null;
        }
    }
}
