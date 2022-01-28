using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCalculation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private float score = 0;


    private void Update()
    {
        scoreText.text = $"Score: {(int) score}";
    }

    private void FixedUpdate()
    {
        score += Time.fixedDeltaTime;
    }
}
