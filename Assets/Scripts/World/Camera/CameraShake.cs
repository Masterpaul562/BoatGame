using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public bool rumble = false;




    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 ogPosition = transform.position;
        float elapse = 0.0f;

        while (elapse < duration)
        {


            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;
            transform.localPosition = new Vector3(x+ ogPosition.x, y+ogPosition.y, ogPosition.z);

            elapse += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = ogPosition;
    }

    public IEnumerator Rumble(float magnitude)
    {
        Vector3 ogPosition = transform.position;
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
