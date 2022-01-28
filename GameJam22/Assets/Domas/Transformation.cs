using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformation : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite angelSprite;
    public Sprite demonSprite;
    // Start is called before the first frame update
    bool isAngel = true;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Transform"))
        {
            changeForm();
        }
    }
    private void changeForm()
    {
        isAngel = !isAngel;

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
