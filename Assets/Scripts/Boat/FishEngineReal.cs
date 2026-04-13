using System.Collections;
using UnityEngine;

public class FishEngineReal : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask interactable;
    [SerializeField] private GameObject lightBar;
    [SerializeField] private HarpoonGun2 fishing;

    private FishInventory inventory;
    private SpriteRenderer sprite;

    [Header("Power")]
    [SerializeField] private float powerLevel = 100f;
    [SerializeField] private float drainSpeed = 10f;
    [SerializeField] private bool shouldDrain = true;
    [SerializeField] private bool canFeed = true;

    private bool feedCD = false;

    [Header("Bar")]
    private const float MAX_BAR_SCALE = 1.687302f;

    [Header("Engine Levels")]
    public int powerStage = 0; // 0–3 bar sections
    public int powerSet = 0;   // engine level
    public float knots;

    private void Start()
    {
        shouldDrain = true;
        canFeed = true;
        inventory = player.GetComponent<FishInventory>();
        sprite = lightBar.GetComponent<SpriteRenderer>();

        SetLightBar();
        UpdateKnots();
    }

    private void Update()
    {
        float vert = Input.GetAxisRaw("Vertical");

        if (vert < 0 && !fishing.isFishing)
        {
            Interact();
        }

        if (shouldDrain)
        {
            DrainPower();
        }
    }

    private void DrainPower()
    {
        powerLevel = Mathf.MoveTowards(powerLevel, 0, Time.deltaTime * drainSpeed);

        if (powerLevel <= 0)
        {
            powerLevel = 100f;

            if (powerStage > 1)
            {
                powerStage--;
                SetLightBar();
            }
            else
            {
                if (powerSet > 0)
                {
                    powerSet--;

                    if (powerSet > 0)
                    {
                        powerStage = 3; // full bar after level drops
                        powerLevel = 100f;

                        UpdateKnots();
                        UpdateBarColor();
                        SetLightBar();
                    }
                    else
                    {
                        shouldDrain = false;
                        StartCoroutine(Blink());    
                    }
                }
               // else
            //    {
                //    shouldDrain = true;
                 //   canFeed = true;
                //    StartCoroutine(Blink());
              //  }
            }
        }
    }

    private void Interact()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            player.transform.position,
            Vector3.forward,
            10,
            interactable
        );

        if (hit.collider != null &&
            hit.collider.CompareTag("Engine") &&
            inventory.fishAmountOutside > 0 &&
            canFeed &&
            !feedCD)
        {
            FeedFish();
        }
    }

    private void FeedFish()
    {
        inventory.fishAmountOutside--;
        shouldDrain = true;
        powerLevel = 100f;
        powerStage++;

        if (powerStage > 3)
        {
            powerStage = 1;
            powerSet++;

            UpdateKnots();
            UpdateBarColor();
        }

        SetLightBar();

        feedCD = true;
        StartCoroutine(FeedCD());
    }

    private void UpdateKnots()
    {
        knots = powerSet * 5f;
    }

    private void SetLightBar()
    {
        float scale = 0f;

        for (int i = 0; i < powerStage; i++)
        {
            scale += MAX_BAR_SCALE / 3f;
        }

        lightBar.transform.localScale = new Vector3(
            scale,
            lightBar.transform.localScale.y,
            lightBar.transform.localScale.z
        );
    }

    private void UpdateBarColor()
    {
        float red = 1f;
        float green = Mathf.Clamp01(1f - powerSet * 0.2f);
        float blue = Mathf.Clamp01(0.8f - powerSet * 0.25f);

        sprite.color = new Color(red, green, blue, 1f);
    }

    private IEnumerator Blink()
    {
        lightBar.transform.localScale = new Vector3(
            MAX_BAR_SCALE,
            lightBar.transform.localScale.y,
            lightBar.transform.localScale.z
        );

        Color color = sprite.color;

        for (int i = 0; i < 6; i++)
        {
            color.a = (i % 2 == 0) ? 1f : 0f;
            sprite.color = color;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator FeedCD()
    {
        yield return new WaitForSeconds(0.5f);
        feedCD = false;
    }
}