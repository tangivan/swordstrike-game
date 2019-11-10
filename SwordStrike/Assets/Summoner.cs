using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Summoner : GenericEnemy
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private Vector2 targetPosition;
    private Animator anim;

    public float timeBetweenSummons;
    private float summonTime;

    public GenericEnemy enemyToSummon;

    public float attackSpeed;
    public float stopDistance;

    private float attackTime;

    public Transform shotPoint1;
    public Transform shotPoint2;
    public Transform shotPoint3;
    public Transform shotPoint4;
    public Transform shotPoint5;
    public Transform shotPoint6;
    public Transform shotPoint7;

    [Header("Projectiles")]
    public GameObject fireball1;
    public GameObject fireball2;
    public GameObject fireball3;
    public GameObject fireball4;
    public GameObject fireball5;
    public GameObject soundlessFireBall;


    [Header("Element Mechanic")]
    public Color waterElement;
    public Color fireElement;
    public Color earthElement;
    public Color airElement;
    public float elementDuration;
    public float timeBetweenElement;
    public SpriteRenderer mySprite1;
    public SpriteRenderer mySprite2;
    public SpriteRenderer mySprite3;

    public GameObject bossBody;


    private Vector3 direction;

    //third attk pattern variables
    private Vector3 movementVector = Vector3.zero;
    private bool isAttacking;
    private Vector3 savedPosition;
    private int attacks;
    public float timeBtwSpawns;
    public float startTimeBtwSpawns;
    public GameObject trail;
    public GameObject dashSound;

    private RigidbodyConstraints2D originalConsraints;
    public int tempCounter;

    private GameObject shieldedHealth;
    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        isBoss = true;
        isAttacking = false;
        attacks = 0;
        originalConsraints = GetComponent<Rigidbody2D>().constraints;
        dpsCounter = 25;
        tempCounter = 25;
        shieldedHealth = GameObject.FindGameObjectWithTag("shieldHealth");
        shieldedHealth.GetComponent<Slider>().maxValue = tempCounter;
        
    }

    private void Update()
    {
        //flips animation
        spriteDirection();
        // Element of boss changes every 'x' seconds. sprite color signifies element
        if(!isDpsCheck)
            elementChange();

        if (health >= 75)
            firstAtkPattern();
        if (health < 75 && health >= 50)
            secondAtkPattern();
        if (health < 50 && health > 25)
            thirdAtkPattern();
        if (health == 25)
            dpsCheck();
        if (health < 25 && health > 0)
            fourthAtkPattern();


    }

    public void Summon()
    {
        if(player !=null)
        {
            Instantiate(enemyToSummon, shotPoint1.position, shotPoint1.rotation);
            Instantiate(enemyToSummon, shotPoint2.position, shotPoint5.rotation);
            Instantiate(enemyToSummon, shotPoint3.position, shotPoint7.rotation);
        }
    }


    public void firstAtkPattern()
    {
        if (player != null)
        {
            anim.SetBool("isRunning", true);

            if (Time.time >= attackTime)
            {
                attackTime = Time.time + 5f;
                anim.SetTrigger("summon");

            }
        }
    }

    public void secondAtkPattern()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        anim.SetBool("isRunning", false);
        if (Time.time >= attackTime)
        {
           
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            targetPosition = new Vector2(randomX, randomY);

            attackTime = Time.time + timeBetweenAttacks;

            StartCoroutine(TeleportAttackCo());

            Instantiate(deathEffect, transform.position, Quaternion.identity);



        }
    }

    private IEnumerator TeleportAttackCo()
    {
        bossBody.SetActive(false);

        Instantiate(deathEffect, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        transform.position = targetPosition;
        RangedAttack();
        bossBody.SetActive(true);
    }


    public void RangedAttack()
    {
        Vector2 direction = player.position - shotPoint2.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        shotPoint2.rotation = rotation;

        Instantiate(fireball1, shotPoint1.position, shotPoint1.rotation);
        Instantiate(fireball1, shotPoint2.position, shotPoint2.rotation);
        Instantiate(fireball2, shotPoint3.position, shotPoint3.rotation);
        Instantiate(fireball3, shotPoint4.position, shotPoint4.rotation);
        Instantiate(fireball4, shotPoint5.position, shotPoint5.rotation);
        Instantiate(fireball5, shotPoint6.position, shotPoint6.rotation);
        Instantiate(soundlessFireBall, shotPoint7.position, shotPoint7.rotation);
    }
    public void thirdAtkPattern()
    {
        GetComponent<Rigidbody2D>().constraints = originalConsraints;
        anim.SetBool("isRunning", true);
        if (Time.time >= attackTime)
        {
            StartCoroutine(Prepare());
            attackTime = Time.time + 5;
        }
    }

    IEnumerator Prepare()
    {
        anim.SetBool("isRunning", false);
        trail.SetActive(true);
        movementVector = (player.position - transform.position).normalized * 50;
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;
        float percent = 0;
    //    Instantiate(enemySound, transform.position, Quaternion.identity);
        while (percent <= 1)
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
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        while (attacks < 3)
        {
            //movementVector decides how fast and how far 
            movementVector = (savedPosition - transform.position).normalized * 175;
            Vector2 originalPosition = transform.position;
            Vector2 targetPosition = player.position;
            float percent = 0;
            //percent decides duration of the dash. 
            Instantiate(dashSound, transform.position, Quaternion.identity);
            Instantiate(dashSound, transform.position, Quaternion.identity);
            while (percent <= 0.75)
            {
                percent += Time.deltaTime * attackSpeed;
                transform.position += movementVector * Time.deltaTime;
                yield return null;
            }
            savedPosition = player.position;
            attacks++;
            yield return new WaitForSeconds(0.3f);
        }
  //      gameObject.layer = LayerMask.NameToLayer("Enemy");
        isAttacking = false;
        trail.SetActive(false);
        anim.SetBool("isRunning", true);
        attacks = 0;

    }
    public void spriteDirection()
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
    }
    public void dpsCheck()
    {
        shieldedHealth.SetActive(true);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        anim.SetBool("isRunning", false);
        isDpsCheck = true;
        // also make him teleport to center.
        if(dpsCounter > 0)
        {
            if (dpsCounter == tempCounter - 1)
            {
                tempCounter = dpsCounter;
                int randomNum = Random.Range(1, 5);
                changeElement(randomNum);
            }
        }
        else
        {
            isDpsCheck = false;
            GetComponent<Rigidbody2D>().constraints = originalConsraints;
            anim.SetBool("isRunning", true);
            health--;
        }
        
    }
    public void fourthAtkPattern()
    {
        if (Time.time >= attackTime)
        {

            int random = Random.Range(0, 3);
            if (random == 0)
            {
                firstAtkPattern();
                StartCoroutine(atkPatternInterval(1));
            }
            if (random == 1)
            {
                anim.SetBool("isRunning", false);
                secondAtkPattern();
                StartCoroutine(atkPatternInterval(2));
            }
            if (random == 2)
            {
                thirdAtkPattern();
                StartCoroutine(atkPatternInterval(3));
            }
        }
    }

    private IEnumerator atkPatternInterval(int pattern)
    {
        if (pattern != 2)
            yield return new WaitForSeconds(1f);
        else
            yield return new WaitForSeconds(0.5f);
    }

    public void elementChange()
    {
        if (Time.time >= elementDuration)
        {
            elementDuration = Time.time + timeBetweenElement;
            int random = Random.Range(1, 5);
            if (random == 4 && element == 4)
                random = 3;
            if (random == 1 && element != 1)
            {
                changeElement(1);
            }
            else if (random == 2 && element != 2)
            {
                changeElement(2);
            }
            else if (random == 3 && element != 3)
            {
                changeElement(3);
            }
            else
            {
                changeElement(4);
            }
        }
    }

    private void changeElement(int newElement)
    {
        if (newElement == 1)
        {
            mySprite1.color = fireElement;
            mySprite2.color = fireElement;
            mySprite3.color = fireElement;
            element = 1;
            Debug.Log("i changed it1" + "it's " + element);
            ColorBlock cb = bossHealthBar.colors;
            cb.disabledColor = new Color32(168, 18, 18, 255);
            bossHealthBar.colors = cb;
            trail.GetComponent<TrailRenderer>().startColor = new Color32(168, 18, 18, 255);
            trail.GetComponent<TrailRenderer>().endColor = Color.white;
        }
        if (newElement == 2)
        {
            mySprite1.color = waterElement;
            mySprite2.color = waterElement;
            mySprite3.color = waterElement;
            element = 2;
            Debug.Log("i changed it2" + "it's " + element);
            ColorBlock cb = bossHealthBar.colors;
            cb.disabledColor = new Color32(0, 39, 212, 255);
            bossHealthBar.colors = cb;
            trail.GetComponent<TrailRenderer>().startColor = new Color32(0, 39, 212, 255);
            trail.GetComponent<TrailRenderer>().endColor = Color.white;
        }
        if (newElement == 3)
        {
            mySprite1.color = airElement;
            mySprite2.color = airElement;
            mySprite3.color = airElement;
            element = 3;
            Debug.Log("i changed it3" + "it's " + element);
            ColorBlock cb = bossHealthBar.colors;
            cb.disabledColor = new Color32(255, 255, 255, 128);
            bossHealthBar.colors = cb;
            trail.GetComponent<TrailRenderer>().startColor = new Color32(255, 255, 255, 128);
            trail.GetComponent<TrailRenderer>().endColor = Color.white;
        }
        if (newElement == 4)
        {
            mySprite1.color = earthElement;
            mySprite2.color = earthElement;
            mySprite3.color = earthElement;
            element = 4;
            Debug.Log("i changed it4" + "it's " + element);
            ColorBlock cb = bossHealthBar.colors;
            cb.disabledColor = new Color32(0, 140, 4, 255);
            bossHealthBar.colors = cb;
            trail.GetComponent<TrailRenderer>().startColor = new Color32(0, 140, 4, 255);
            trail.GetComponent<TrailRenderer>().endColor = Color.white;
        }
    }

    // Rewrites elemental bonus to heal boss when using weak element, deal no damage with neutral element
    // and normal damage with correct element
    public override float elementalBonus(float damageAmount, float wepElement)
    {
        float bonus;
        switch (element)
        {

            case 1:
                if (wepElement == 4)
                {
                    bonus = damageAmount * -1;
                }
                else if (wepElement == 2)
                {
                    bonus = damageAmount;
                }
                else
                {
                    bonus = 0;
                }
                return bonus;
            case 2:
                if (wepElement == 1)
                {
                    bonus = damageAmount * -1;
                }
                else if (wepElement == 3)
                {
                    bonus = damageAmount;
                }
                else
                {
                    bonus = 0;
                }
                return bonus;

            case 3:
                if (wepElement == 2)
                {
                    bonus = damageAmount * -1;
                }
                else if (wepElement == 4)
                {
                    bonus = damageAmount;
                }
                else
                {
                    bonus = 0;
                }
                return bonus;
            default:
                if (wepElement == 3)
                {
                    bonus = damageAmount * -1;
                }
                else if (wepElement == 1)
                {
                    bonus = damageAmount;
                }
                else
                {
                    bonus = 0;
                }
                return bonus;


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GetComponent<Collider2D>().isTrigger)
        {
            player.GetComponent<SimplePlayer>().TakeDamage(damage);
        }
    }
}
