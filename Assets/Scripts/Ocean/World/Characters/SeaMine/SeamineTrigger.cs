using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeamineTrigger : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private HoleManager hole;
    [SerializeField] private CameraShake camShake;
    [SerializeField] private DustCloud impactCloud;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Bomb")
        {
            int random = Random.Range(1, 3);
            hole.CreateHole(random);
            StartCoroutine(camShake.Shake(0.7f, 0.5f));
            impactCloud.Spawn();
        }
    }

    }
