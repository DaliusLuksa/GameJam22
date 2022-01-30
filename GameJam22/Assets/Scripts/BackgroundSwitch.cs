using UnityEngine;

public class BackgroundSwitch : MonoBehaviour
{
    [SerializeField] private Sprite angelBG = null;
    [SerializeField] private Sprite devilgBG = null;
    [SerializeField] private float scoreIntervals = 10;

    private float oldScore = 0;
    private SpriteRenderer spriteRenderer = null;
    private bool isAngelBG = true;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        var newScore = PlatformSpawner.instance.GetComponent<ScoreCalculation>().score;

        if (newScore - oldScore >= scoreIntervals)
        {
            oldScore += scoreIntervals;

            if (isAngelBG)
            {
                spriteRenderer.sprite = devilgBG;
            }
            else
            {
                spriteRenderer.sprite = angelBG;
            }

            isAngelBG = !isAngelBG;
        }
        
    }
}