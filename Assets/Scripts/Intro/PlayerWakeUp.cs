using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWakeUp : MonoBehaviour
{
    public GameObject player;

    private bool awaken = false;

    private void Update()
    {
        if (Input.anyKey&& !awaken)
        {
            awaken = true;
            TriggerWake();
        }
    }

    public void TriggerWake()
    {
        GetComponent<Animator>().SetTrigger("Wake");
    }

   public void WakeUp()
    {
        this.gameObject.SetActive(false);
        player.SetActive(true);
        this.enabled = false;
    }
}
