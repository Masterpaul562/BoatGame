using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public bool rumble = false;


    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector2 ogPositon = transform.localPosition;
        float elapse = 0.0f;

        while (elapse < duration)
        {


            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;
            transform.localPosition = new Vector2(x, y);

            elapse += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = ogPositon;
    }

    public IEnumerator Rumble(float magnitude)
    {
        Vector3 ogPosition = transform.localPosition;
        while (rumble)
        {
            float x = (Random.Range(-1, 1) * magnitude)+ogPosition.x;
            float y = (Random.Range(-1, 1) * magnitude)+ogPosition.y;
            transform.localPosition = new Vector3(x, y,ogPosition.z);
            yield return null;
        }
        transform.localPosition = ogPosition;
    }

}
