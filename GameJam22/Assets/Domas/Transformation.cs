using UnityEngine;
using UnityEngine.Events;

public class Transformation : MonoBehaviour
{
    [SerializeField] private KeyCode transformButton = KeyCode.B;
    [SerializeField] private Sprite angelSprite;
    [SerializeField] private Sprite demonSprite;
    [SerializeField] bool isAngel = true;

    [HideInInspector]
    public UnityEvent<PlatformType> onPlayerFormChange = new UnityEvent<PlatformType>();

    SpriteRenderer spriteRenderer;

    public bool IsAngel => isAngel;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(transformButton))
        {
            changeForm();
        }
    }

    private void changeForm()
    {
        isAngel = !isAngel;

        onPlayerFormChange.Invoke(isAngel ? PlatformType.Angel : PlatformType.Devil);

        if (isAngel)
        {
            spriteRenderer.sprite = angelSprite;
        }
        else
        {
            spriteRenderer.sprite = demonSprite;
        }
        /*also add logic for changing the properties of movement depending on form*/
    }
}
