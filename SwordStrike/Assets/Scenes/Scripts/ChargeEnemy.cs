using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemy : GenericEnemy
{
    public float stopDistance;

    private float attackTime;

    public float attackSpeed;

    private Vector3 movementVector = Vector3.zero;

    private Vector3 direction;

    public GameObject enemySound;

    public GameObject trail;

    private bool isAttacking;
    private Vector3 savedPosition;

    public override void Start()
    {
        base.Start();
        isAttacking = false;
    }
    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {
            direction = (transform.position - player.position).normalized;
            if (direction.x < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (direction.x > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }


            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                if (Time.time >= attackTime)
                {   
                    StartCoroutine(Prepare());
                    attackTime = Time.time + timeBetweenAttacks;

                }
            }
        }

    }
    IEnumerator Prepare()
    {
        trail.SetActive(true);
        movementVector = (player.position - transform.position).normalized * 25;
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;
        float percent = 0;
        Instantiate(enemySound, transform.position, Quaternion.identity);
        while (percent <= 2)
        {
            percent += Time.deltaTime * attackSpeed;
            transform.position += -movementVector * Time.deltaTime;

            yield return null;
        }
        if (!isAttacking)
        {
            savedPosition = player.position;
            isAttacking = true;
        }
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        movementVector = (savedPosition- transform.position).normalized * 100;
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;
        float percent = 0;
        while (percent <= 2)
        {
            percent += Time.deltaTime * attackSpeed;
            transform.position += movementVector * Time.deltaTime;

            yield return null;
        }
        gameObject.layer = LayerMask.NameToLayer("Enemy");
     //   GetComponent<Collider2D>().isTrigger = true;
        trail.SetActive(false);
        isAttacking = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GetComponent<Collider2D>().isTrigger)
        {
            player.GetComponent<SimplePlayer>().TakeDamage(damage);
            // StartCoroutine(Phase());



        }
    }

    IEnumerator Phase()
    {
        yield return new WaitForSeconds(0.1f);
        if (GetComponent<Collider2D>().isTrigger)
        {
            player.GetComponent<SimplePlayer>().TakeDamage(damage);
        }
      //  GetComponent<Collider2D>().isTrigger = false;
        gameObject.layer = LayerMask.NameToLayer("PhaseThrough");
    }
}
