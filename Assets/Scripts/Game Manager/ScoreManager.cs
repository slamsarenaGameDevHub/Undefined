using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ScoreManager : MonoBehaviour
{
    [Header("Scored Points")]
    Animator UIAnimator;
    public ScoreDisplay scoreHolder;
    public int Score;
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
}
