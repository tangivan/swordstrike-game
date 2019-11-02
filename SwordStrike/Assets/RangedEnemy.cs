using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : GenericEnemy
{

    public float stopDistance;

    private float attackTime;

    private Animator anim;

    public Transform shotPoint;

    public GameObject fireball;

    private Vector3 direction;

    public bool isPatrol;


    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        direction = (transform.position - player.position).normalized;
        //forest noxluff uses this script too, so adjusting hpbar offsets only for rangedBunny.
        if (!isPatrol)
        {
            if (direction.x > 0)
                healthBar.GetComponent<UIAnchor>().screenOffset = new Vector3(30, -65, 0);
            if (direction.x < 0)
                healthBar.GetComponent<UIAnchor>().screenOffset = new Vector3(-10, -65, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                direction = (transform.position - player.position).normalized;
                //to prevent noxluff from flipping
                if (!isPatrol)
                {
                    if (direction.x < 0)
                    {
                        transform.eulerAngles = new Vector3(0, 180, 0);
                        healthBar.GetComponent<UIAnchor>().screenOffset = new Vector3(-10, -65, 0);
                    }
                    else if (direction.x > 0)
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        healthBar.GetComponent<UIAnchor>().screenOffset = new Vector3(30, -65, 0);
                    }
                }
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            }

            if(Time.time >= attackTime && Vector2.Distance(transform.position, player.position) < stopDistance &&!isPatrol)
            {
                attackTime = Time.time + timeBetweenAttacks;
                anim.SetTrigger("attack");
            }

            if (Time.time >= attackTime && isPatrol)
            {
                Vector2 direction = player.position - shotPoint.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                shotPoint.rotation = rotation;

                Instantiate(fireball, shotPoint.position, shotPoint.rotation);
                attackTime = Time.time + timeBetweenAttacks;

            }
        }
    }

    public void RangedAttack()
    {
        Vector2 direction = player.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle -90, Vector3.forward);
        shotPoint.rotation = rotation;

        Instantiate(fireball, shotPoint.position, shotPoint.rotation);
    }
}
