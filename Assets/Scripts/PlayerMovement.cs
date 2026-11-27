using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public int multipleJumps = 1;
    public float jumpForce = 5f;
    public float moveSpeed = 5f;
    float horizontalMove;
    int jumpsRemaining;

    void Awake()
    {
        jumpsRemaining = Mathf.Max(0, multipleJumps);
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocityX = horizontalMove * moveSpeed;        
    }

    public void Jump(InputAction.CallbackContext ct)
    {
        if (ct.performed && jumpsRemaining > 0)
        {
            rb.linearVelocityY = jumpForce;
            jumpsRemaining--;
        }
    }
    public void Move(InputAction.CallbackContext ct)
    {
        horizontalMove = ct.ReadValue<Vector2>().x;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            if (collision.GetContact(i).normal.y > 0.5f)
            {
                jumpsRemaining = Mathf.Max(0, multipleJumps);
                return;
            }
        }
    }
}
