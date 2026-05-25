using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatAnimations : MonoBehaviour
{
    public GameObject player;
    private void ShowPlayer()
    {
        player.SetActive(true);
        this.gameObject.SetActive(false);
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<PlayerMove>().freeze = false;
    }
    private void Exit()
    {
        player.GetComponent<EnterBoat>().StartExit();

    }

    //Add function to start second enter function in player
}
