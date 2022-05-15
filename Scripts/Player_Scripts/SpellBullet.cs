using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBullet : MonoBehaviour
{
    public float speed = 3f;
    private Animator animator;

    private bool canMove;
    Rigidbody2D body;

    void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        canMove = true;
        StartCoroutine(DisableSpell(5f));
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime * .65f;
            transform.position = temp;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    IEnumerator DisableSpell(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == MyTags.SKELETON_TAG || collision.gameObject.tag == MyTags.SPIDER_TAG || 
           collision.gameObject.tag == MyTags.BAT_TAG || collision.gameObject.tag == MyTags.ZOMBIE_TAG || 
           collision.gameObject.tag == MyTags.SLIME_TAG)
        {
            animator.Play("Cast_Spell");
            canMove = false;
            StartCoroutine(DisableSpell(0.3f));
        }
    }
}
