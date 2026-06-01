using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroHarpHead : MonoBehaviour
{
    public IntroHarpoon harpoon;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Inside")
        {

            StopAllCoroutines();
            harpoon.StartReel();
        }

    }
}
