using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpAnimation : MonoBehaviour
{
    [SerializeField] private SetupOcean action;

    public void WakeUp()
    {
        action.WakeUp();
    }
}
