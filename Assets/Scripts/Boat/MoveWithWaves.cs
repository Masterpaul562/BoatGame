using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithWaves : MonoBehaviour
{
    float y;
    [SerializeField] private WaveDeformer wave;
    public float yOffset;
    public float rotationPower;

    // Update is called once per frame
    void Update()
    {
        float time = Time.time * wave.speed;
         y = Mathf.Sin(transform.TransformPoint(transform.position).x * wave.frequency + time) * wave.amplitude;
        transform.position = new Vector2(transform.position.x, y+ yOffset);
        Quaternion rot = Quaternion.Euler(0, 0, y * rotationPower);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 2);
    }
}
