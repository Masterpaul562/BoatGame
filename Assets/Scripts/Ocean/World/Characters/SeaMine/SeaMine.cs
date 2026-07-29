using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaMine : MonoBehaviour
{
    public Animator animator;
    public AudioSource source;
    public float size;
    public bool shouldDestroy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        transform.localScale = new Vector2 (size, size);
        animator.SetTrigger("Explode");
        source.Play();
    }
    private void DestorySelf()
    {
       Destroy(this.gameObject);
        
    }
}
