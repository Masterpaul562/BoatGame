using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Albatross : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private GameObject player;
    private Animator animator;

    [Header("Settings")]
    public float waitMin;
    public float waitMax;

    [Header("Info")]
    public bool perched;


    private void Start()
    {
        animator = GetComponent<Animator>();
        perched = true;
        StartCoroutine(Emote());
    }




    private IEnumerator Emote()
    {
        while (perched)
        {
            float time = Random.Range(waitMin, waitMax);
            yield return new WaitForSeconds(time);

            int emote = Random.Range(0, 2);
            
            if (emote == 0 )
            {
                animator.SetTrigger("Peck");
            }else if (emote == 1)
            {
                animator.SetTrigger("Ruffle");
            }
        }
    }

}
