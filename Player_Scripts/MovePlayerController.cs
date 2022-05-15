using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerController : MonoBehaviour
{
    public float speed = 1.01f;

    private Rigidbody2D body;
    private Animator animator;
    public Transform groundCheckPosition;
    public LayerMask groundLayer;

    private bool isGrounded;
    private bool jumped;
    public float jumpPower = 2.4f;

    private float halfPlayerSizeX;

    public GameObject spellRight;
    public GameObject spellLeft;

    private bool facingRight = true;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        halfPlayerSizeX = GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    void Update()
    {
        CheckIfGrounded();
        PlayerJump();
        clampPlayerMovement();
        CastSpell();
    }

    void FixedUpdate()
    {
        PlayerWalk();
    }

    //player cannot move to the left of the camera
    void clampPlayerMovement()
    {
        Vector3 position = transform.position;

        float distance = transform.position.z - Camera.main.transform.position.z;

        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + halfPlayerSizeX;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - halfPlayerSizeX;

        position.x = Mathf.Clamp(position.x, leftBorder, rightBorder);
        transform.position = position;
    }

    void PlayerWalk()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        
            if (horizontal > 0)
            {
                body.velocity = new Vector2(speed, body.velocity.y);

                ChangeDirection(.5f);
                facingRight = true;
            }

            else if (horizontal < 0)
            {
                body.velocity = new Vector2(-speed, body.velocity.y);

                ChangeDirection(-.5f);
                facingRight = false;
            }

            //prevents sliding
            else
            {
                body.velocity = new Vector2(0f, body.velocity.y);
            }

        animator.SetInteger("Speed", Mathf.Abs((int)body.velocity.x));
    }

    //flip the character
    void ChangeDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);
    
        if (isGrounded)
        {
            if (jumped)
            {
                jumped = false;
                animator.SetBool("Jump", false);
            }
        }
    }

    void PlayerJump()
    {
        if (isGrounded)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                body.velocity = new Vector2 (body.velocity.x, jumpPower);
                
                animator.SetBool("Jump", true);
            }
        }
    }

    void CastSpell()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (facingRight == true)
            {
                GameObject spellBullet = Instantiate(spellRight, transform.position, Quaternion.identity);
                spellBullet.GetComponent<SpellBullet>().Speed += transform.localScale.x;
            }
            else if (facingRight == false)
            {
                GameObject spellBullet = Instantiate(spellLeft, transform.position, Quaternion.identity);
                spellBullet.GetComponent<SpellBullet>().Speed += transform.localScale.x;
            }
        }
    }
}
