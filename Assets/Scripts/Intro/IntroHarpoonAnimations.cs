using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroHarpoonAnimations : MonoBehaviour
{

    [SerializeField] private IntroHarpoon harpoon;

    public void Stow()
    {
        harpoon.StowHarpoon();
    }
    public void Fire()
    {
        harpoon.Fire();
    }
}
