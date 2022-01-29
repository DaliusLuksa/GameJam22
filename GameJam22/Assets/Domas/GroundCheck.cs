using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded;
    public float offset = 1.5f;
    public Vector2 surfacePosition;
    ContactFilter2D filter;
    Collider2D[] results = new Collider2D[2];
    [SerializeField] private GameObject gameoverScreen;

    private void Update()
    {
        Vector2 point = transform.position + Vector3.down * offset;
        Vector2 size = new Vector2(transform.localScale.x, transform.localScale.y);

        if (Physics2D.OverlapBox(point, size, 0, filter.NoFilter(), results) > 1)
        {

            Platform suspectPlatform = null;
            try
            {
                suspectPlatform = results[1].GetComponent<Platform>();
                if (suspectPlatform == null)
                {
                    Collider2D temp = results[0];
                    results[0] = results[1];
                    results[1] = temp;

                    suspectPlatform = results[1].GetComponent<Platform>();
                }
            }
            catch (System.Exception) { }

            if (suspectPlatform == null)
            {
                isGrounded = true;
                surfacePosition = Physics2D.ClosestPoint(transform.position, results[1]);
            }
            else
            {
                isGrounded = !suspectPlatform.IsDisabled;
            }
        }
        else
        {
            isGrounded = false;
        }
        if (transform.position.y <= -20)
        {
            gameoverScreen.SetActive(true);
        }
    }
}
