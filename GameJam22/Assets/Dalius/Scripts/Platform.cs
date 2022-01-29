using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private PlatformType platformType;

    [SerializeField] private int platformSize;
    [SerializeField] private float platformSpeed;

    private float minX = -50;

    public Vector2 PlatformSize => new Vector2(platformSize, 0);
    public PlatformType PlatformType => platformType;
    public bool IsDisabled => platformType == PlatformType.Default ? false : GameObject.FindGameObjectWithTag("Player").GetComponent<Transformation>().IsAngel ? platformType != PlatformType.Angel : platformType != PlatformType.Devil;

    private void Start()
    {
        platformSpeed = PlatformSpawner.instance.CurrentPlatformSpeed;

        PlatformSpawner.instance.platformSpeedChange.AddListener(UpdateSpeed);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transformation>().onPlayerFormChange.AddListener(ChangeCollision);

        ChangeCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Transformation>().IsAngel ? PlatformType.Angel : PlatformType.Devil);
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

    private void ChangeCollision(PlatformType currentType)
    {
        // if the platform type is Default
        // just enable collision and return
        if (platformType == PlatformType.Default)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            return;
        }

        if (platformType == currentType)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
