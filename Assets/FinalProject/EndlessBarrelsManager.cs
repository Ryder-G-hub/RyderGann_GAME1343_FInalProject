using UnityEngine;
using TMPro;

public class EndlessBarrelsManager : MonoBehaviour
{
    public static EndlessBarrelsManager instance;
    public TextMeshProUGUI playerScoreField;
    public TextMeshProUGUI playerLivesField;

    int playerScore = 0;
    int playerLives = 3;

    private void Awake()
    {
        instance = this;

        playerScore = 0;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void AddScore(int value)
    {
        playerScore += value;
    }

    public void RemoveLives(int value)
    {
        playerLives -= value;
    }

    
}
