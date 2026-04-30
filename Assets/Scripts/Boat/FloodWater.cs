using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodWater : MonoBehaviour
{
    [Header("Refrence")]
    [SerializeField] private MoveWithWaves tilt;

    [Header("Settings")]
    [SerializeField] private float rotationPower;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        Quaternion rot = Quaternion.Euler(0, 0, -tilt.transform.eulerAngles.z * rotationPower);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 2);
    }
}
