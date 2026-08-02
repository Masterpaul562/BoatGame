using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("yay");
        if (other.gameObject.tag == "Bobber")
        {

            var script = other.GetComponent<HarpoonHead>();
            if (!script.harpoon.fishHooked && !script.harpoon.isReeling)
            {
                GetComponent<Floater>().enabled = false;
                script.harpoon.scrapHooked = true;
                script.harpoon.fishHooked = true;
                script.harpoon.hookedFish = this.gameObject;
                transform.parent = other.gameObject.transform;
                transform.position = other.transform.position;
            }
        }

    }
}
