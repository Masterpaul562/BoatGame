using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earwiggy : MonoBehaviour
{
    private Animator animator;

    [Header("Refrence")]
    [SerializeField] private CameraShake camShake;
    [SerializeField] private CamSizeManager camSize;
    [SerializeField] private SpeedManager speedMan;
    [SerializeField] private HoleManager hole;
    [SerializeField] private DustCloud impactCloud;

    [Header("Settings")]
    public float swimAwaySpeed;
    public float swimAwayAnimationSpeed;

    [Header("Info")]
    public bool hasAttack;
    public bool isSwiming;


    private void Start()
    {
        animator = GetComponent<Animator>();
        hasAttack = false;
    }
    
    private void Update()
    {
        if(!isSwiming){
            animator.SetFloat("Speed",1);
        }
    }


    public void EarwigAttack()
    {
        //camShake.rumble = true;
        hasAttack = true;
        animator.SetTrigger("Attack");
        
        
    }

    private void AttackDamage()
    {
        
        StartCoroutine(camShake.Shake(0.7f, 0.5f));
        hole.CreateHole();
        impactCloud.Spawn();
    }
   

    public IEnumerator SwimAway()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetFloat("Speed",swimAwayAnimationSpeed);
        camShake.rumble = false;
        isSwiming = true;
        speedMan.earwigDistance = 100f;
        yield return null;
        while(transform.position.y > -camSize.gameSpaceBottom - 4)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x - 0.5f, transform.position.y - 1f, transform.position.z),Time.deltaTime*swimAwaySpeed);
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
