using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeEnemy : GenericEnemy
{
    public float stopDistance;

    private float attackTime;

    public float attackSpeed;

    public bool hitTarget;
    public Collider2D triggerCollider;

    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {
            if (Time.time >= attackTime && Vector2.Distance(transform.position, player.position) > stopDistance && hitTarget == false)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                hitTarget = false;
                triggerCollider.enabled = true;
            }
        }

    }
        

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<SimplePlayer>().TakeDamage(damage);
            hitTarget = true;
            triggerCollider.enabled = false;
            attackTime = Time.time + timeBetweenAttacks;
        }
    }
}
