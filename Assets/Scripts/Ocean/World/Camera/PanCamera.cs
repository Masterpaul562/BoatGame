using System.Collections;
using UnityEngine;

public class PanCamera : MonoBehaviour
{
    [Header("References")]
    public Transform target;
    public Camera cam;
    public PanCamera otherPan;
    [SerializeField] private CameraZoom cameraZoom;

    [Header("Settings")]
    [SerializeField] private bool moveY = false;
    [SerializeField] private bool pan = false;
    [SerializeField] private float smoothTime = 0.3f;

    private Vector3 camVelocity;

    private void Update()
    {
        if (pan)
        {
            Pan();
        }
    }

    private void Pan()
    {
        Vector3 desiredPos = new Vector3(
            target.position.x,
            moveY ? target.position.y : cam.transform.position.y,
            cameraZoom.ogPosition.z
        );

        cam.transform.position = Vector3.SmoothDamp(
            cam.transform.position,
            desiredPos,
            ref camVelocity,
            smoothTime
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        otherPan?.StopCoroutine();

        StopAllCoroutines();
        camVelocity = Vector3.zero;
        pan = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        pan = false;
        StopAllCoroutines();
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        camVelocity = Vector3.zero;

        while (Vector3.Distance(cam.transform.position, cameraZoom.ogPosition) > 0.01f)
        {
            cam.transform.position = Vector3.SmoothDamp(
                cam.transform.position,
                cameraZoom.ogPosition,
                ref camVelocity,
                smoothTime
            );

            yield return null;
        }

        cam.transform.position = cameraZoom.ogPosition;
        camVelocity = Vector3.zero;
    }

    public void StopCoroutine()
    {
        pan = false;
        camVelocity = Vector3.zero;
        StopAllCoroutines();
    }
}
