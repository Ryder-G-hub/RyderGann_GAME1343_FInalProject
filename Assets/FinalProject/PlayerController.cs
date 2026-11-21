using TMPro;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public GameObject PlayerModel; 
    public float moveSpeed = 4f;
    public float gravity = -9.8f;

    public float jumpHeight = 5f;
    public float groundCheckDistance = 0.2f;
    public float jumpOverCheckDistance = 4.0f;
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public int inputNumber = 1;
    public int playerScore = 0;
    public int playerLives = 3;
    InputData data;
    Rigidbody rb;
    bool jumpHeld = false;
    bool isGameOver = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb  = GetComponent<Rigidbody>();
        UpdateScoreText();
        UpdateLivesText();
    }

    // Update is called once per frame
    void Update()
    {

        if(isGameOver)
        {
            return;
        }
        data = InputPoller.reference.GetInput(inputNumber);

        MovePlayer(data.leftStick.x);
        JumpPlayer(data.leftStick.y);

        CheckJumpOverBarrel();
    }

    public void MovePlayer(float value)
    {
        Vector3 movement = rb.linearVelocity;

        movement.x = value * moveSpeed;

        rb.linearVelocity = movement;


        // No Movement 
        if (value == 0) { return;  }
        
        // Facing up X X+  
        if (value > 0) { PlayerModel.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)); }
        // Facing down X X-
        else { PlayerModel.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));  } 

    }
    public void JumpPlayer(float value)
    {
        bool jumpPressed = value > 0.5f;

        if(jumpPressed && !jumpHeld && IsGrounded())
        {
            Vector3 velocity = rb.linearVelocity;
            velocity.y = Mathf.Sqrt(2f * -Physics.gravity.y * jumpHeight);
            rb.linearVelocity = velocity;
        }

        jumpHeld = jumpPressed;
    }

    bool IsGrounded()
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        return Physics.Raycast(origin, Vector3.down, 0.1f + groundCheckDistance, ~0, QueryTriggerInteraction.Ignore);
    }

    void CheckJumpOverBarrel()
    {
        if (IsGrounded())
        {
            return;
        }

        RaycastHit hit;

        Vector3 origin = transform.position + Vector3.down * 0.05f;
        if (Physics.Raycast(origin, Vector3.down, out hit, jumpOverCheckDistance, ~0, QueryTriggerInteraction.Ignore))
        {
            BarrrelPrefab barrel = hit.collider.GetComponent<BarrrelPrefab>();
            if (barrel != null && !barrel.scored)
            {
                barrel.scored = true;
                AddScore(barrel.scoreGiven);
            }
        }


    }

    public void AddScore(int amount)
    {
        playerScore += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if(scoreText != null)
        {
            scoreText.text = playerScore.ToString();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        BarrrelPrefab barrel = collision.gameObject.GetComponent<BarrrelPrefab>();
        if (barrel != null)
        {
            Destroy(barrel.gameObject);
            LoseLife();
        }
    }

    public void LoseLife()
    {
        if (isGameOver)
        {
            return;
        }

        playerLives = Mathf.Max(playerLives - 1, 0);
        UpdateLivesText();

        if (playerLives == 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
    }

    void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = playerLives.ToString();
        }
    }



}
