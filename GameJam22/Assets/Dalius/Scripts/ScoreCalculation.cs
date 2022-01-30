using TMPro;
using UnityEngine;

public class ScoreCalculation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private GameObject gameoverScreen;

    public float score { get; private set; } = 0;
    private bool isGameOver = false;

    private void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<GroundCheck>().onGameOver.AddListener(GameOver);
    }

    private void Update()
    {
        scoreText.text = $"Score: {(int) score}";
        finalScoreText.text = $"Final Score: {(int)score}";
    }

    private void FixedUpdate()
    {
        if (!isGameOver)
        {
            score += Time.fixedDeltaTime;
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        gameoverScreen.SetActive(true);
    }
}