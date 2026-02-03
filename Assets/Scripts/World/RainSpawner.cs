using UnityEngine;

public class RainSplashSpawner : MonoBehaviour
{
    public WaveDeformer wave;
    public ParticleSystem splashes;

    public float spawnWidth = 10f;
    public float spawnRate = 20f;

    public float frontOffset = 0f;
    public float backOffset = 0.3f;

    float timer;

    void Update()
    {
        if (wave == null || splashes == null) return;

        timer += Time.deltaTime;

        float interval = 1f / spawnRate;

        while (timer >= interval)
        {
            timer -= interval;
            SpawnSplash();
        }
    }

    void SpawnSplash()
    {
        float x = Random.Range(-spawnWidth / 2f, spawnWidth / 2f);

        // Sample front & back wave
        float front = wave.GetWaveHeight(x + frontOffset);
        float back = wave.GetWaveHeight(x + backOffset);

        float height = Mathf.Lerp(front, back, 0.3f);

        Vector3 pos = new Vector3(
            x,
            transform.position.y + height,
            transform.position.z
        );

        splashes.Emit(new ParticleSystem.EmitParams
        {
            position = pos,
            applyShapeToPosition = false
        }, 1);
    }
}
