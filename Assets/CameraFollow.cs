using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;     // Obiekt do œledzenia (czyli gracz)
    public float smoothSpeed = 0.125f;  // P³ynnoœæ ruchu
    public Vector3 offset;       // Przesuniêcie wzglêdem gracza

    public GameObject normalPrefab;
    public GameObject icePrefab;
    public GameObject spikePrefab;
    public GameObject enemyPrefab;

    public float spawnInterval = 2f;
    public float checkOffset = 5f;

    [Range(0, 1)] public float normalChance = 0.25f;
    [Range(0, 1)] public float iceChance = 0.25f;
    [Range(0, 1)] public float spikeChance = 0.25f;
    [Range(0, 1)] public float enemyChance = 0.25f;

    public float nextSpawnX = 2f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        LoadSpawnChances();
        //SpawnNext(); // Startowy spawn
    }

    void Update()
    {
        float cameraRightEdge = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;

        if (cameraRightEdge + checkOffset >= nextSpawnX)
        {
            SpawnNext();
        }
    }
    float GetBottomY(GameObject obj)
    {
        var colliders = obj.GetComponentsInChildren<Collider>();
        if (colliders.Length == 0) return 0;

        float minY = colliders[0].bounds.min.y;
        foreach (var col in colliders)
        {
            if (col.bounds.min.y < minY)
                minY = col.bounds.min.y;
        }
        return minY;
    }
    void SpawnNext()
    {
        GameObject prefab = GetRandomPrefab();
        Vector3 spawnPos = new Vector3(nextSpawnX, 0, 0);
        GameObject block = Instantiate(prefab, spawnPos, Quaternion.identity);
        float bottomY = GetBottomY(block);
        block.transform.position -= new Vector3(0, bottomY, 0);

        nextSpawnX += spawnInterval;
    }

    GameObject GetRandomPrefab()
    {
        float total = normalChance + iceChance + spikeChance + enemyChance;
        float rand = Random.Range(0f, total);

        if (rand < normalChance)
            return normalPrefab;
        else if (rand < normalChance + iceChance)
            return icePrefab;
        else if (rand < normalChance + iceChance + spikeChance)
            return spikePrefab;
        else
            return enemyPrefab;
    }


void LateUpdate()
    {
        // Œledzimy tylko oœ X, reszta pozostaje taka jak obecna
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z + offset.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void LoadSpawnChances()
    {
        normalChance = PlayerPrefs.GetFloat("normalChance", normalChance);
        iceChance = PlayerPrefs.GetFloat("iceChance", iceChance);
        spikeChance = PlayerPrefs.GetFloat("spikeChance", spikeChance);
        enemyChance = PlayerPrefs.GetFloat("enemyChance", enemyChance);
        nextSpawnX = PlayerPrefs.GetFloat("nextSpawnX", nextSpawnX);
        spawnInterval = PlayerPrefs.GetFloat("nextSpawnX", nextSpawnX);
    }
}
