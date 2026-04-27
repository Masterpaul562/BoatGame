using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Albatross : MonoBehaviour
{
    private Animator anim;

    [Header("Refrences")]
    [SerializeField] private GameObject player;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
}
