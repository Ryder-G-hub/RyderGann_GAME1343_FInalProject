using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject PlayerModel; 
    public float moveSpeed = 4f;
    public float gravity = -9.8f;

    public float jumpHeight = 5f;
    public int inputNumber = 1;
    public int playerScore = 0;
    public int playerLives = 3;
    InputData data;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb  = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        data = InputPoller.reference.GetInput(inputNumber);

        MovePlayer(data.leftStick.x);
        JumpPlayer(data.leftStick.y);
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
        
    }
}
