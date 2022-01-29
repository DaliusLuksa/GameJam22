using UnityEngine;
using UnityEngine.Events;

public class Transformation : MonoBehaviour
{
    [SerializeField] private KeyCode transformButton = KeyCode.B;
    [SerializeField] private Sprite angelSprite;
    [SerializeField] private Sprite demonSprite;
    [SerializeField] bool isAngel = true;
    private bool isTransformDisabled = false;

    [HideInInspector]
    public UnityEvent<PlatformType> onPlayerFormChange = new UnityEvent<PlatformType>();

    SpriteRenderer spriteRenderer;
    Animator animator;

    public bool IsAngel => isAngel;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isAngel", isAngel);
        if (Input.GetKeyDown(transformButton) && !isTransformDisabled)
        {
            changeForm();
        }

    }

    private void changeForm()
    {
        isAngel = !isAngel;

        onPlayerFormChange.Invoke(isAngel ? PlatformType.Angel : PlatformType.Devil);
        animator.SetTrigger("Transform");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Platform"))
        {
            if (collision.GetComponent<Platform>().IsDisabled)
            {
                isTransformDisabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Platform"))
        {
            if (collision.GetComponent<Platform>().IsDisabled)
            {
                isTransformDisabled = false;
            }
        }
    }
}
