using System.Collections;
using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    [Header("Boat Speed")]
    public float currentSpeed;
    public FishEngineReal engine;

    [Header("Earwig")]
    [SerializeField] private GameObject earwig;
    [SerializeField] private Transform spawnPos;
    public bool earwigSpawned;
    public float earwigSpeed;
    public float earwigDistance;
    public float maxEarwigDistance = 100f;
    public float attackDistance;

    private void Start()
    {
        earwigSpawned = false;
        StartCoroutine(EarwigMove());
    }

    private void Update()
    {
        CalculateSpeed();
    }

    private void CalculateSpeed()
    {
        // Pull speed directly from engine
        currentSpeed = engine.knots;
    }

    private void EarwigSpawn()
    {
        earwig.SetActive(true);
        earwig.transform.position = spawnPos.position;
        earwigSpawned = true;
    }

    private IEnumerator EarwigMove()
    {
        while (true)
        {
            var earwigScript = earwig.GetComponent<Earwiggy>();
            float speedDifference = Mathf.Abs(currentSpeed - earwigSpeed);
            if (earwigScript.isSwiming)
            {

            }
            else if (currentSpeed < earwigSpeed)
            {
                // Earwig catches up
                earwigDistance = Mathf.MoveTowards(
                    earwigDistance,
                    0,
                    Time.deltaTime * speedDifference * 3
                );

                if (earwigDistance < 50 && !earwigSpawned)
                {
                    EarwigSpawn();
                }

                if (earwigSpawned)
                {
                    earwig.transform.position = Vector3.MoveTowards(
                        earwig.transform.position,
                        transform.position,
                        Time.deltaTime * speedDifference * 3
                    );
                }
                
                if (earwigDistance < attackDistance && !earwigScript.hasAttack)
                {
                   earwigScript.EarwigAttack();
                }
            }
            else
            {
                // Boat outruns earwig
                earwigDistance = Mathf.MoveTowards(
                    earwigDistance,
                    maxEarwigDistance,
                    Time.deltaTime * speedDifference * 3
                );

                if (earwigSpawned)
                {
                    earwig.transform.position = Vector3.MoveTowards(
                        earwig.transform.position,
                        spawnPos.position,
                        Time.deltaTime * speedDifference * 3
                    );
                }

                if (earwigDistance > 50 && earwigSpawned)
                {
                    earwig.SetActive(false);
                    earwigSpawned = false;
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}