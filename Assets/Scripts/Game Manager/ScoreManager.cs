using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ScoreManager : MonoBehaviour
{
    [Header("Scored Points")]
    Animator UIAnimator;
    public ScoreDisplay scoreHolder;


    public int KillReward,Score;
    [SerializeField] int minReward=60,maxReward=60;
    
    [Header("UI Elements")]
    [SerializeField] TMP_Text scoreText;
    private void OnEnable()
    {
        UIAnimator = GetComponent<Animator>();

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
        print("Wrong Kill");
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
