using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonStow : MonoBehaviour
{
    [SerializeField] private HarpoonGun2 harpoon;

    public void Stow()
    {
        harpoon.StowHarpoon();
    }
}

