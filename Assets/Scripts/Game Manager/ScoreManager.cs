using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ScoreManager : MonoBehaviour
{
    [Header("Scored Points")]
    Animator UIAnimator;
    public ScoreDisplay scoreHolder;
    public int KillReward,Score;
    [SerializeField] int minReward=10,maxReward=60;
    
    [Header("Game Score UI Elements")]
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text finalScoreText,bestScoreText;

    [SerializeField] GameObject scoreOverlay;
    
    private void OnEnable()
    {
        UIAnimator = GetComponent<Animator>();
        scoreOverlay.SetActive(false);
        GameManager.Scored += GivePoint;
        GameManager.Lost += GameLost;
    }
    private void OnDisable()
    {
        GameManager.Scored-= GivePoint;
        GameManager.Lost -= GameLost;
    }
    
    void GivePoint()
    {
       
    }
    void GameLost()
    {
        finalScoreText.text=Score.ToString();
        scoreOverlay.SetActive(true);
    }
    private void Update()
    {
        KillReward = Random.Range(minReward, maxReward);
    }
    public void AddToScore(int score)
    {
        Score += score;
    }
}
