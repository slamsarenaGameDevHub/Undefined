using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int HighestScore;
    

    public PlayerData(int highScore)
    {
        HighestScore = highScore;      
    }
}
