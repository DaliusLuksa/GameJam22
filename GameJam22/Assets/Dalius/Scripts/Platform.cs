using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private PlatformType platformType;

    [SerializeField] private int platformSize;
    [SerializeField] private float platformSpeed;

    private float minX = -50;

    public Vector2 PlatformSize => new Vector2(platformSize, 0);

    private void Start()
    {
        platformSpeed = PlatformSpawner.instance.CurrentPlatformSpeed;

        PlatformSpawner.instance.platformSpeedChange.AddListener(UpdateSpeed);
    }

    private void Update()
    {
        // Platform movement
        Movement();

        // Check if the platform is outside enough
        DespawnCheck();
    }

    private void UpdateSpeed(float speed)
    {
        platformSpeed = speed;
    }

    private void Movement()
    {
        transform.Translate(Vector3.left * platformSpeed * Time.deltaTime);
    }

    private void DespawnCheck()
    {
        if (transform.position.x < minX)
        {
            Destroy(gameObject);
        }
    }
}
