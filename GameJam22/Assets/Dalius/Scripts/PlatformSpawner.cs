using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformSpawner : MonoBehaviour
{
    public static PlatformSpawner instance { get; private set; }

    [SerializeField] private List<GameObject> DevilPlatformPrefabs;
    [SerializeField] private List<GameObject> AngelPlatformPrefabs;
    [SerializeField] private float currentPlatformSpeed = 2;
    [SerializeField] private float spawnPointX;
    [SerializeField] private KeyCode increasePlatformSpeedKey = KeyCode.H;

    [HideInInspector]
    public UnityEvent<float> platformSpeedChange = new UnityEvent<float>();

    private GameObject lastSpawnedPlatform = null;

    public float CurrentPlatformSpeed => currentPlatformSpeed;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        SpawnPlatform();

        IncreasePlatformSpeed();
    }

    private void SpawnPlatform()
    {
        float dist = 12;

        if (lastSpawnedPlatform != null)
        {
            dist = Vector2.Distance(
                new Vector2(lastSpawnedPlatform.transform.position.x, 0) + lastSpawnedPlatform.GetComponent<Platform>().PlatformSize,
                new Vector2(spawnPointX, 0)
            );
        }

        if (dist >= 12)
        {
            if (Random.Range(0f, 1f) > 0.5f)
            {
                lastSpawnedPlatform = Instantiate(DevilPlatformPrefabs[Random.Range(0, DevilPlatformPrefabs.Count)], new Vector3(spawnPointX, Random.Range(-10f, -6f), 0), Quaternion.identity);
            }
            else
            {
                lastSpawnedPlatform = Instantiate(AngelPlatformPrefabs[Random.Range(0, AngelPlatformPrefabs.Count)], new Vector3(spawnPointX, Random.Range(-10f, -6f), 0), Quaternion.identity);
            }
        }
    }

    private void IncreasePlatformSpeed()
    {
        if (Input.GetKeyDown(increasePlatformSpeedKey))
        {
            var newSpeed = Random.Range(currentPlatformSpeed, currentPlatformSpeed + 1);
            currentPlatformSpeed = newSpeed;
            platformSpeedChange.Invoke(currentPlatformSpeed);
        }
    }
}
