using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkManager : MonoBehaviour
{
    [SerializeField] public JunkSpawner junkList;
    [SerializeField] private GameObject bobber;
    public GameObject hookedJunk;
    public int hookedJunkIndex;
    public bool canHook;

    private void Awake()
    {
        junkList = GetComponent<JunkSpawner>();
    }
    private void Update()
    {
        if (bobber.GetComponent<Bobber>().submerged)
        {
            FindHookedJunk();
            if(hookedJunk != null)
            {
                HookCheck();
            }
        }
    }
    private void FindHookedJunk()
    {
        for (int i = 0; i < junkList.junk.Count; i++)
        {
            Debug.Log(Vector2.Distance(bobber.transform.position, junkList.junk[i].transform.position));
            if (Vector2.Distance(bobber.transform.position, junkList.junk[i].transform.position) < 3)
            {
               
                hookedJunk = junkList.junk[i];
                hookedJunkIndex = i;
            }
        }
        
    }
    private void HookCheck()
    {
        if(Vector2.Distance(bobber.transform.position, hookedJunk.transform.position) < 3)
        {
            canHook = true;
        }else
        {
            canHook= false;
            hookedJunk = null;
        }
    }
}
