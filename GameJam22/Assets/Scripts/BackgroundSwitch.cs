using System.Collections;
using UnityEngine;

public class BackgroundSwitch : MonoBehaviour
{
    [SerializeField] private SpriteRenderer angelBG = null;
    [SerializeField] private SpriteRenderer devilgBG = null;
    [SerializeField] private float scoreIntervals = 10;

    private float oldScore = 0;
    private bool isAngelBG = true;

    void Update()
    {
        var newScore = PlatformSpawner.instance.GetComponent<ScoreCalculation>().score;

        if (newScore - oldScore >= scoreIntervals)
        {
            oldScore += scoreIntervals;

            StartCoroutine(BackgroundChange());
        }

    }

    private IEnumerator BackgroundChange()
    {
        if (isAngelBG)
        {
            devilgBG.sortingOrder = 5;
            angelBG.sortingOrder = 10;
            devilgBG.color = new Color(devilgBG.color.r, devilgBG.color.g, devilgBG.color.b, 1);

            while (angelBG.color.a > 0)
            {
                var angelBGColors = angelBG.color;
                angelBGColors = new Color(angelBGColors.r, angelBGColors.g, angelBGColors.b, angelBGColors.a - 0.1f);
                angelBG.color = angelBGColors;

                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            devilgBG.sortingOrder = 10;
            angelBG.sortingOrder = 5;
            angelBG.color = new Color(angelBG.color.r, angelBG.color.g, angelBG.color.b, 1);

            while (devilgBG.color.a > 0)
            {
                var devilBGColors = devilgBG.color;
                devilBGColors = new Color(devilBGColors.r, devilBGColors.g, devilBGColors.b, devilBGColors.a - 0.1f);
                devilgBG.color = devilBGColors;

                yield return new WaitForSeconds(0.1f);
            }
        }

        isAngelBG = !isAngelBG;
    }
}