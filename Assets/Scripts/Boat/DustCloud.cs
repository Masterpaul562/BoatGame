using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustCloud : MonoBehaviour
{
    public void Spawn()
    {
    this.gameObject.SetActive(true);
    }
    public void Despawn()
    {
        this.gameObject.SetActive(false);
    }
}
