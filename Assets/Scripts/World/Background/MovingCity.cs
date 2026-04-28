using UnityEngine;

public class MovingCity : MonoBehaviour
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

        transform.Translate(Vector2.left * finalSpeed * Time.deltaTime);
    }
}