using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongEnemyScript : MonoBehaviour
{
    [SerializeField]
    private float input_speed;
    private float speed;

    [SerializeField]
    private Vector3[] positions;

    private int index;

    private Rigidbody2D body;

    void Awake() 
    {
        speed = input_speed;
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, positions[index], Time.deltaTime * speed);
        
        if (transform.position == positions[index])
        {
            if (index == positions.Length - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }
    }

    IEnumerator SpellWearOff()
    {
        yield return new WaitForSeconds(3f);
        speed = input_speed;
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




    

    
