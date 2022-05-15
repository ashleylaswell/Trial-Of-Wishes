using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float agroRange;

    [SerializeField]
    private float moveSpeed;
    private float speed;

    private Rigidbody2D body;

    private Animator animator;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = moveSpeed;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < agroRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasingPlayer();
        }
    }

    private void ChasePlayer()
    {
        if(transform.position.x < player.position.x)
        {
            body.velocity = new Vector2(speed, 0);
        }
        else if (transform.position.x > player.position.x)
        {
            body.velocity = new Vector2(-speed, 0);
        }
        if (gameObject.tag == MyTags.SLIME_TAG)
        {
            animator.Play("Slime_Awake");
        }
    }

    private void StopChasingPlayer()
    {
        body.velocity = new Vector2(0, 0);
    }

    IEnumerator SpellWearOff()
    {
        yield return new WaitForSeconds(3f);
        speed = moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == MyTags.SPELL_TAG)
        {
            speed = 0f;

            StartCoroutine(SpellWearOff());
        }
    }
}
