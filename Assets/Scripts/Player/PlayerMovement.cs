using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float extraRayHeight;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Movement();
        FlipSprite();
        SetAnimations();

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && IsGrounded())
            Jump();
    }

    private void FixedUpdate()
    {
        GravityCompensation();
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        moveDirection = new(horizontalInput, 0f);

        transform.Translate(moveSpeed * Time.deltaTime * moveDirection.normalized);
    }

    private void FlipSprite()
    {
        if (PlayerCombat.isAttacking) return;

        if (moveDirection.x > 0f)
            transform.localScale = Vector3.one;
        else if (moveDirection.x < 0f)
            transform.localScale = new(-1f, 1f, 1f);
    }

    private void Jump()
    {
        rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);

        animator.SetTrigger("Jump");
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraRayHeight, groundLayer);

        return raycastHit.collider != null;
    }

    private void GravityCompensation()
    {    
        if (!IsGrounded())
            rb.AddForce(Vector2.down * gravityMultiplier, ForceMode2D.Force);
    
    }

    private void SetAnimations()
    {
        float xInput = moveDirection.x;
        float yInput = rb.velocity.y;

        if (xInput != 0f)
            animator.SetBool("Moving", true);
        else
            animator.SetBool("Moving", false);

        if (yInput == 0f)
            animator.SetBool("Grounded", true);
        else
            animator.SetBool("Grounded", false);
    }
}
