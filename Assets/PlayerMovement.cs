using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    [Header("Movement")]
    public float moveSpeed = 8f;
    float horizontalMovement;

    [Header("Jumping")]
    public float jumpPower = 10f;

    [Header("GraundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGraunded())
        {
            if (context.performed)
            {
                Debug.Log("Full jump");
                //Hold down jump button = full height
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            }
           
           else if (context.canceled)
            {
                Debug.Log("Half jump");
                //Light Tap of jump button = half the height
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower * 0.5f);
            }
        }
    }

    private bool isGraunded()
    {
        if(Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
            {
            return true;
            }
        return false;
    }

    //Graundcheck gizmos visualization
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckPos.position, groundCheckSize);
    }
}
