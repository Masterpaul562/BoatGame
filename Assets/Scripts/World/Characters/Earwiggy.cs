using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earwiggy : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private CameraShake camShake;
    [SerializeField] private SpeedManager speedMan;
    [SerializeField] private HoleManager hole;



    [Header("Info")]
    public bool hasAttack;
    public bool isSwiming;


    private void Start()
    {
        animator = GetComponent<Animator>();
        hasAttack = false;
    }


    public void EarwigAttack()
    {
        //camShake.rumble = true;
        hasAttack = true;
        StartCoroutine(camShake.Shake(0.7f, 0.5f));
        animator.SetTrigger("Attack");
        hole.CreateHole();
        
    }
   

    public IEnumerator SwimAway()
    {
        camShake.rumble = false;
        isSwiming = true;
        speedMan.earwigDistance = 100f;
        yield return null;
        while(Vector3.Distance(this.transform.position, camShake.transform.position) < 25)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x - 0.5f, transform.position.y - 1f, transform.position.z),Time.deltaTime);
            yield return null;
        }
        hasAttack = false;
        this.gameObject.SetActive(false);
        isSwiming = false;
        speedMan.earwigSpawned = false;
        yield return null;
       // hasAttack = false;
    }
}
