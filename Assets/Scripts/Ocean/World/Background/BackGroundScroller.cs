using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{
    [Header("Base scrolling speed")]
    public float scrollSpeed = 1f;

    [Header("How much boat knots affect scrolling")]
    public float knotsMultiplier = 0.1f;

    [Header("Boat reference")]
    public FishEngineReal boat;

    void Update()
    {
        MoveLeft();
    }

    void MoveLeft()
    {
        float finalSpeed = scrollSpeed + (boat.knots * knotsMultiplier);
       // Vector2 left = new Vector3 (transform.position.x -(finalSpeed*Time.deltaTime),transform.position.y,transform.position.z);
        //transform.position = left; 
        transform.Translate(Vector2.left * finalSpeed * Time.deltaTime);
    }
}