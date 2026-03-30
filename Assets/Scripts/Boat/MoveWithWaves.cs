using UnityEngine;
using System.Collections;
public class MoveWithWaves : MonoBehaviour
{
    float y;
    float lastY;
    float lastSprayTime;

    [SerializeField] private WaveDeformer wave;

    [Header("Boat Movement")]
    public float yOffset;
    public float rotationPower;

    [Header("Spray Settings")]
    public GameObject sprayObject;     // Assign in Inspector
    public float crestThreshold = 0.7f; // How close to peak
    public float sprayCooldown = 0.6f;  // Seconds between sprays
    public float sprayDuration = 0.5f;  // How long spray stays visible

    void Update()
    {
        float time = Time.time * wave.speed;
        float waveValue = Mathf.Sin(transform.position.x * wave.frequency + time);
        y = waveValue * wave.amplitude;

        transform.position = new Vector2(transform.position.x, y + yOffset);

        Quaternion rot = Quaternion.Euler(0, 0, y * rotationPower);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 2);

        float verticalSpeed = y - lastY;
        bool goingUp = verticalSpeed > 0;
        bool nearCrest = waveValue > crestThreshold;
        bool cooldownReady = Time.time > lastSprayTime + sprayCooldown;

        if (goingUp && nearCrest && cooldownReady)
        {
            PlaySpray();
        }

        lastY = y;
    }

    void PlaySpray()
    {
        if (sprayObject == null)
            return;

        lastSprayTime = Time.time;

        StopAllCoroutines(); // Important: prevents overlap bugs

        StartCoroutine(SprayRoutine());
    }

    IEnumerator SprayRoutine()
    {
        // Enable
        sprayObject.SetActive(true);

        // Restart animation
        Animator anim = sprayObject.GetComponent<Animator>();
        if (anim != null)
        {
            anim.Play(0, 0, 0f);
        }

        // Wait for duration
        yield return new WaitForSeconds(sprayDuration);

        // Disable
        sprayObject.SetActive(false);
    }

    void HideSpray()
    {
        if (sprayObject != null)
        {
            sprayObject.SetActive(false);
        }
    }
}