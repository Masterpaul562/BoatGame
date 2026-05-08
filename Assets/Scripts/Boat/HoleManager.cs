using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{

    [Header("Info")]
    public List<GameObject> holes = new List<GameObject>();
    public float waterLevel;
    private bool fixCD;
    private int failedHoleSpawn;

    [Header("Settings")]
    public float waterSpeed;
    public float sinkLevel;
    public float sinkSpeed;
    public float sinkRotSpeed;
    public float fixCDTime;
    public bool shouldDrain;
    

    [Header("Refrences")]
    [SerializeField] private GameObject holePrefab;
    [SerializeField] private Collider2D spawnLocation;
    [SerializeField] private Transform boat;
    [SerializeField] private Sprite spriteHole1;
    [SerializeField] private Sprite spriteHole2;
    [SerializeField] private GameObject floodWater;
    [SerializeField] private GameObject player;
    [SerializeField] private HarpoonGun2 fishing;
    [SerializeField] private LayerMask interactable;

    

    private void Update()
    {
        if( Input.GetMouseButtonDown(0))
        {
        CreateHole();
        }
        if(holes.Count > 0 && shouldDrain)
        {
            FloodBoat();
        }

        if(waterLevel> sinkLevel)
        {
            SinkBoat();
        }

        float vert = Input.GetAxisRaw("Vertical");

        if (vert < 0 && !fishing.isFishing && holes.Count > 0 && !fixCD)
        {
            FixHole();
        }

    }

    public void CreateHole()
    {

        Vector2 spawnPos = FindSpawnLocation(spawnLocation.bounds);
        bool shouldSpawn = false;
        if (holes.Count > 0)
        {
            for (int i = 0; i < holes.Count; i++)
            {
                if (spawnPos.x > holes[i].transform.position.x - 0.1f && spawnPos.x < holes[i].transform.position.x + 0.1f)
                {
                  Debug.Log("Failed");
                  shouldSpawn = false;
                    
                  i = 10000;
                }
                else
                {
                    shouldSpawn = true;
                    
                }
            }
        }

        if (shouldSpawn || holes.Count<=0)
        {

            failedHoleSpawn = 0;
            if (Random.Range(0, 2) == 1)
            {
                holePrefab.GetComponent<SpriteRenderer>().sprite = spriteHole1;
            }
            else
            {
                holePrefab.GetComponent<SpriteRenderer>().sprite = spriteHole2;
            }

            var hole = Instantiate(holePrefab, spawnPos, Quaternion.identity, boat);
            holes.Add(hole);
            floodWater.SetActive(true);
        }
        else
        {
            if (failedHoleSpawn < 4)
            {
                Debug.Log("Failed to Spawn");
                CreateHole();
                failedHoleSpawn++;
            }
        }
    }

    private Vector2 FindSpawnLocation(Bounds bound)
    {
        return new Vector2 (
        Random.Range(bound.min.x,bound.max.x),
        Random.Range(bound.min.y,bound.max.y)
        );
    }

    private void FixHole()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector3.forward, 10, interactable);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Hole")
            {
                int index = FindHole(hit.collider.gameObject);

                player.GetComponent<PlayerMove>().freeze = true;
                player.GetComponent<Animator>().SetTrigger("FixHole");

                Destroy(holes[index]);
                holes.RemoveAt(index);

                fixCD = true;
                StartCoroutine(FixCooldown());
            }
        }
    }

    private int FindHole(GameObject targetObj)
    {
        for(int i = 0; i < holes.Count; i++)
        {
            if (holes[i].gameObject == targetObj)
            {
                return i;
            }
        }
        return -1;
    }

    private void FloodBoat()
    {
        waterLevel += waterSpeed * holes.Count * Time.deltaTime;
        floodWater.transform.localScale = new Vector2(floodWater.transform.localScale.x, waterLevel);
    }
    private void SinkBoat()
    {
        var wave = this.gameObject.GetComponent<MoveWithWaves>();
        wave.yOffset -= sinkSpeed * waterLevel * Time.deltaTime;

        wave.rotationOffset += sinkRotSpeed * Time.deltaTime;
    }
    private IEnumerator FixCooldown()
    {
        yield return new WaitForSeconds(fixCDTime);
        fixCD = false;
    }

}
