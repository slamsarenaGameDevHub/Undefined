using TMPro;
using UnityEngine;

[RequireComponent(typeof(DestroyGameObject))]
public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] float yIncrement=0.2f;
    Vector3 scorePosition;
    public void SetScore(int score)
    {
        scoreText.text=score.ToString();
        
    }
    private void Update()
    { 
        transform.rotation=Camera.main.transform.rotation;
        scorePosition= transform.position+new Vector3(0,yIncrement,0);
        transform.position = scorePosition;
    }
}
