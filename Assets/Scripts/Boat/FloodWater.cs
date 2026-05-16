using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodWater : MonoBehaviour
{
    [Header("Refrence")]
    [SerializeField] private MoveWithWaves tilt;
    [SerializeField] private HoleManager hole;

    [Header("Settings")]
    [SerializeField] private float rotationPower;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (hole.isSinking)
        {
            Quaternion rot = Quaternion.Euler(0, 0, tilt.transform.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 2);
        }
        else
        {
            Quaternion rot = Quaternion.Euler(0, 0, -tilt.transform.eulerAngles.z * rotationPower);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 2);
        }
    }
}
