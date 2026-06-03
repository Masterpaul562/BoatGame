using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;

    [Header("Parallax")]
    [Tooltip("0 = fixed far background, 1 = matches camera, >1 = moves faster than camera")]
    public float parallaxFactor = 0.5f;

    private Vector3 lastCameraPosition;

    private void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 cameraDelta = cameraTransform.position - lastCameraPosition;

        // Apply parallax (can be > 1 for foreground speed boost)
        Vector3 moveAmount = cameraDelta * parallaxFactor;

        transform.position += new Vector3(moveAmount.x, moveAmount.y, 0f);

        lastCameraPosition = cameraTransform.position;
    }
}