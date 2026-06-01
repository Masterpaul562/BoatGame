using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonAnimation : MonoBehaviour
{
    [SerializeField] private HarpoonGun2 harpoon;

    public void Stow()
    {
        harpoon.StowHarpoon();
    }
    public void Fire()
    {
        harpoon.Fire();
    }
}

