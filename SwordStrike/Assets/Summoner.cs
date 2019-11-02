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




    public override void Start()
    {
        base.Start();
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);
        anim = GetComponent<Animator>();
        isBoss = true;
    }

    private void Update()
    {
        // Element of boss changes every 'x' seconds. sprite color signifies element
        if (Time.time >= elementDuration)
        {
            elementDuration = Time.time + timeBetweenElement;
            int random = Random.Range(1, 5);
            Debug.Log(random);
            if (random == 4 && element == 4)
                random = 3;
            if(random==1 && element!=1)
            {
                mySprite1.color = fireElement;
                mySprite2.color = fireElement;
                mySprite3.color = fireElement;
                element = 1;
                ColorBlock cb = bossHealthBar.colors;
                cb.disabledColor = new Color32(168, 18, 18, 255);
                bossHealthBar.colors = cb;
            }
            else if (random == 2 && element!=2)
            {
                mySprite1.color = waterElement;
                mySprite2.color = waterElement;
                mySprite3.color = waterElement;
                element = 2;
                ColorBlock cb = bossHealthBar.colors;
                cb.disabledColor = new Color32(0, 39, 212, 255);
                bossHealthBar.colors = cb;
            }
            else if (random == 3 && element!=3)
            {
                mySprite1.color = airElement;
                mySprite2.color = airElement;
                mySprite3.color = airElement;
                element = 3;
                ColorBlock cb = bossHealthBar.colors;
                cb.disabledColor = new Color32(255, 255, 255, 128);
                bossHealthBar.colors = cb;
            }
            else
            {
                mySprite1.color = earthElement;
                mySprite2.color = earthElement;
                mySprite3.color = earthElement;
                element = 4;
                ColorBlock cb = bossHealthBar.colors;
                cb.disabledColor = new Color32(0, 140, 4, 255);
                bossHealthBar.colors = cb;
            }
        }



        if (player != null)
        {
         //   if(Vector2.Distance(transform.position, targetPosition) > .5f)
         //   {
               // transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
              //  anim.SetBool("isRunning", true);
         //   }
         ////   else
        //    {
                anim.SetBool("isRunning", false);

                if(Time.time >= summonTime)
                {
                    summonTime = Time.time + timeBetweenSummons;
                    anim.SetTrigger("shoot");
                    
                }
          //  }
/*
            if (Vector2.Distance(transform.position, player.position) < stopDistance)
            {
                if (Time.time >= attackTime)
                {
                   // StartCoroutine(Attack());
                   // attackTime = Time.time + timeBetweenAttacks;

                }
            }
            */
        }
    }

    public void Summon()
    {
        if(player !=null)
        {
            Instantiate(enemyToSummon, transform.position, transform.rotation);
        }
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
                Debug.Log(bonus);
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
                Debug.Log(bonus);
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
                Debug.Log(bonus);
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
                Debug.Log(bonus);
                return bonus;


        }
    }
}
