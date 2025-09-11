
using UnityEngine;

public class BgScroller : MonoBehaviour
{
    public float scrollSpeed;
    private float startPos;
   [SerializeField] private float direction;
    private float length;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x-direction,transform.position.y,transform.position.z);
        if(transform.position.x< startPos - length ){
            transform.position = new Vector2(transform.position.x + length,transform.position.y);
        }
    }
}
