using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector2 moveDirection;

    private void Update()
    {
        Movement();
        FlipSprite();
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        moveDirection = new(horizontalInput, 0f);

        transform.Translate(moveSpeed * Time.deltaTime * moveDirection.normalized);
    }

    private void FlipSprite()
    {
        if (moveDirection.x > 0f)
            transform.localScale = Vector3.one;
        else if (moveDirection.x < 0f)
            transform.localScale = new(-1f, 1f, 1f);
    }
}
