using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    private bool hasSpawn;

    private WalkingEnemyScript walkingEnemyScript;
    private PingPongEnemyScript pingPongEnemyScript;
    private SlimeScript slimeScript;
    private SpriteRenderer rendererComponent;
    private Animator animator;

    private void Awake()
    {
        walkingEnemyScript = GetComponent<WalkingEnemyScript>();
        pingPongEnemyScript = GetComponent<PingPongEnemyScript>();
        slimeScript = GetComponent<SlimeScript>();
        rendererComponent = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        hasSpawn = false;

        if (gameObject.tag == MyTags.SKELETON_TAG || gameObject.tag == MyTags.ZOMBIE_TAG)
        {
            walkingEnemyScript.enabled = false;
        }
        else if (gameObject.tag == MyTags.SPIDER_TAG || gameObject.tag == MyTags.BAT_TAG)
        {
            pingPongEnemyScript.enabled = false;
        }
        else if (gameObject.tag == MyTags.SLIME_TAG)
        {
            slimeScript.enabled = false;
        }

        animator.enabled = false;
    }

    void Update()
    {
        if (hasSpawn == false)
        {
            if (rendererComponent.IsVisibleFrom(Camera.main))
            {
                Spawn();
            }
        }
    }

    private void Spawn()
    {
        hasSpawn = true;

        if (gameObject.tag == MyTags.SKELETON_TAG || gameObject.tag == MyTags.ZOMBIE_TAG)
        {
            walkingEnemyScript.enabled = true;
        }
        else if (gameObject.tag == MyTags.SPIDER_TAG || gameObject.tag == MyTags.BAT_TAG)
        {
            pingPongEnemyScript.enabled = true;
        }
        else if (gameObject.tag == MyTags.SLIME_TAG)
        {
            slimeScript.enabled = true;
        }

        animator.enabled = true;
    }
}
