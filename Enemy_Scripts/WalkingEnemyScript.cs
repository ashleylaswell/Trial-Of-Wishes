using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyScript : MonoBehaviour
{
    [SerializeField]
    private float inputMoveSpeed;
    private float moveSpeed;

    private Rigidbody2D body;
    private Animator animator;

    private bool moveLeft;

    [HideInInspector]
    public bool canMove;

    public Transform leftCollision, rightCollision;
    private Vector3 leftCollisionPosition, rightCollisionPosition;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        leftCollisionPosition = leftCollision.position;
        rightCollisionPosition = rightCollision.position;

        moveSpeed = inputMoveSpeed;
    }

    void Start()
    {
        moveLeft = true;
        canMove = true;
    }

    void Update()
    {
        if (canMove)
        {
            if (moveLeft)
            {
                body.velocity = new Vector2(-moveSpeed, body.velocity.y);
            }
            else
            {
                body.velocity = new Vector2(moveSpeed, body.velocity.y);
            }
        }
    }

    void ChangeDirection()
    {
        moveLeft = !moveLeft;

        Vector3 tempScale = transform.localScale;

        if (moveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);

            leftCollision.position = leftCollisionPosition;
            rightCollision.position = rightCollisionPosition;
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);

            //swap collision positions
            leftCollision.position = rightCollisionPosition;
            rightCollision.position = leftCollisionPosition;
        }
        transform.localScale = tempScale;
    }

    IEnumerator SpellWearOff()
    {
        yield return new WaitForSeconds(2f);

        if (gameObject.tag == MyTags.SKELETON_TAG)
        {
            animator.Play("Skeleton_Walking");
        }
        moveSpeed = inputMoveSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == MyTags.SPELL_TAG)
        {
            moveSpeed = 0f;

            if (gameObject.tag == MyTags.SKELETON_TAG)
            {
                animator.Play("Skeleton_Stunned");
            }

            StartCoroutine(SpellWearOff());
        }
    }
}
