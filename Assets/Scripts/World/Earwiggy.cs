using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earwiggy : MonoBehaviour
{
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void EarwigAttack()
    {
        animator.SetTrigger("Attack");
    }
}
