using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformSpawner : MonoBehaviour
{
    public static PlatformSpawner instance { get; private set; }

    [SerializeField] private List<GameObject> DevilPlatformPrefabs;
    [SerializeField] private List<GameObject> AngelPlatformPrefabs;
    [SerializeField] private float currentPlatformSpeed = 2;
    [SerializeField] private float timeTillSpeedup = 10;
    [SerializeField] private float speedupAmount = 2;
    [SerializeField] private float spawnPointX;

    [HideInInspector]
    public UnityEvent<float> platformSpeedChange = new UnityEvent<float>();

    private GameObject lastSpawnedPlatform = null;

    private bool isGameOver = false;
    private float currSpeedupTime = 0;

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

    private void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<GroundCheck>().onGameOver.AddListener(GameOver);
    }

    private void Update()
    {
        if (isGameOver) { return; }

        SpawnPlatform();

        if (currSpeedupTime <= 0)
        {
            currSpeedupTime = timeTillSpeedup;

            IncreasePlatformSpeed();
        }
        else
        {
            currSpeedupTime -= Time.deltaTime;
        }
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
        var newSpeed = Random.Range(currentPlatformSpeed, currentPlatformSpeed + speedupAmount);
        currentPlatformSpeed = newSpeed;
        platformSpeedChange.Invoke(currentPlatformSpeed);
    }

    private void GameOver()
    {
        isGameOver = true;
    }
}
