using UnityEngine;
using System.Collections;

public class MoveWithWaves : MonoBehaviour
{
    float y;
    float lastY;
    float lastSprayTime;

    [SerializeField] private WaveDeformer wave;
    [SerializeField] private FishEngineReal speed;

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
        if (speed.knots < 15)
        {
            // Use the continuous wave phase from WaveDeformer
            float waveValue = Mathf.Sin(transform.position.x * wave.frequency + wave.waveTime);
            y = waveValue * wave.amplitude;

            // Move boat vertically
            transform.position = new Vector2(transform.position.x, y + yOffset);

            // Rotate boat based on wave tilt
            Quaternion rot = Quaternion.Euler(0, 0, y * rotationPower);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 2);

            // Spray detection
            float verticalSpeed = y - lastY;
            bool goingUp = verticalSpeed > 0;
            bool nearCrest = waveValue > crestThreshold;
            bool cooldownReady = Time.time > lastSprayTime + sprayCooldown;

            if (goingUp && nearCrest && cooldownReady)
            {
                PlaySpray();
            }

            lastY = y;
        }else
        {
            float waveValue = Mathf.Sin(transform.position.x * wave.frequency + wave.waveTime);
            y = waveValue * wave.amplitude;
        }
    }

    void PlaySpray()
    {
        if (sprayObject == null)
            return;

        lastSprayTime = Time.time;

        StopAllCoroutines(); // Prevent overlapping
        StartCoroutine(SprayRoutine());
    }

    IEnumerator SprayRoutine()
    {
        sprayObject.SetActive(true);

        Animator anim = sprayObject.GetComponent<Animator>();
        if (anim != null)
        {
            anim.Play(0, 0, 0f);
        }

        yield return new WaitForSeconds(sprayDuration);

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