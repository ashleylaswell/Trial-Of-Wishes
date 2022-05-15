using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionScript : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;

    public LayerMask playerLayer;

    private bool stunned;

    public Transform leftCollision, rightCollision, topCollision, downCollision;
    private Vector3 leftCollisionPosition, rightCollisionPosition;

    private PlayerHealthScript playerHealthScript;
    private EnemyCollisionScript enemyCollisionScript;
    private WalkingEnemyScript walkingEnemyScript;

    private MovePlayerController player;
    private ScoreScript scoreScript;

    public float circleSize = 0.03f;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        leftCollisionPosition = leftCollision.position;
        rightCollisionPosition = rightCollision.position;

        playerHealthScript = GetComponent<PlayerHealthScript>();
        enemyCollisionScript = GetComponent<EnemyCollisionScript>();
        walkingEnemyScript = GetComponent<WalkingEnemyScript>();

        player = GetComponent<MovePlayerController>();

        scoreScript = GameObject.FindObjectOfType(typeof(ScoreScript)) as ScoreScript;
    }

    void Update()
    {
        CheckCollision();
    }

    public void CheckCollision()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftCollision.position, Vector2.left, 0.05f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightCollision.position, Vector2.right, 0.05f, playerLayer);
        RaycastHit2D bottomHit = Physics2D.Raycast(downCollision.position, Vector2.down, 0.05f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(topCollision.position, circleSize, playerLayer);

        if (topHit != null)
        {
            if (topHit.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity =
                        new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 2.4f);

                    if (gameObject.tag == MyTags.SKELETON_TAG || gameObject.tag == MyTags.ZOMBIE_TAG)
                    {
                        walkingEnemyScript.canMove = false;
                    }

                    body.velocity = new Vector2(0, 0);

                    if (gameObject.tag == MyTags.SKELETON_TAG)
                    {
                        animator.Play("Skeleton_Stunned");
                    }
                    stunned = true;
                    StartCoroutine(Dead(0.5f));
                    
                    if (gameObject.tag == MyTags.SKELETON_TAG || gameObject.tag == MyTags.ZOMBIE_TAG)
                    {
                        scoreScript.AddToScore(50);
                    }
                    else if (gameObject.tag == MyTags.SPIDER_TAG || gameObject.tag == MyTags.BAT_TAG)
                    {
                        scoreScript.AddToScore(100);
                    }
                    else if (gameObject.tag == MyTags.SLIME_TAG)
                    {
                        scoreScript.AddToScore(150);
                    }
                }
            }
        }

        if (leftHit)
        {
            if (leftHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    //apply damage to player
                    leftHit.collider.gameObject.GetComponent<PlayerHealthScript>().Damage();
                }
            }
        }

        if (rightHit)
        {
            if (rightHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    //apply damage to player
                    rightHit.collider.gameObject.GetComponent<PlayerHealthScript>().Damage();
                }
            }
        }

        if (bottomHit)
        {
            if (bottomHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    //apply damage to player
                    bottomHit.collider.gameObject.GetComponent<PlayerHealthScript>().Damage();
                }
            }
        }
    }

    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (topCollision.position, circleSize);
    }
}
