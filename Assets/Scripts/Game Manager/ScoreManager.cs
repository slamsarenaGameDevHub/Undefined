using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ScoreManager : MonoBehaviour
{
    Sniper sniper;
    [Header("Scored Points")]
    Animator UIAnimator;
    public ScoreDisplay scoreHolder;
    public int KillReward,Score;
    [SerializeField] int minReward=10,maxReward=60;
    
    [Header("Game Score UI Elements")]
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text finalScoreText,bestScoreText;

    
    private void OnEnable()
    {
        sniper=FindFirstObjectByType<Sniper>();
        scoreText.text=0.ToString();
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
        sniper.gameObject.SetActive(false);
        finalScoreText.text=Score.ToString();
        if(SavedPlayerData.LoadData()!=null)
        {
            if(Score>SavedPlayerData.LoadData().HighestScore)
            {
                bestScoreText.text=Score.ToString();
            }
            else
            {
                bestScoreText.text=SavedPlayerData.LoadData().HighestScore.ToString();
            }
            scoreText.text = Score.ToString();
        }
        else
        {
            bestScoreText.text = Score.ToString();
            SavedPlayerData.SaveData(new PlayerData(Score));
        }
        UpdateHighScore();
        UIAnimator.SetTrigger("GameOver");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void Update()
    {
        KillReward = Random.Range(minReward, maxReward);
        scoreText.text = Score.ToString();
    }
    public void AddToScore(int score)
    {
        Score += score;
    }
    void UpdateHighScore()
    {
        if(SavedPlayerData.LoadData()!=null)
        {
            if(Score>SavedPlayerData.LoadData().HighestScore)
            {
                SavedPlayerData.SaveData(new PlayerData(Score));
            }
        }
        else
        {
            SavedPlayerData.SaveData(new PlayerData(Score));
        }
    }
}
