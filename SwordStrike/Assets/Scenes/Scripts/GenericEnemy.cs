using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericEnemy : MonoBehaviour
{
    public float health;

    [HideInInspector]
    public Transform player;

    public float speed;

    public float timeBetweenAttacks;

    public float damage;

    private BoxCollider2D collider;

    public ParticleSystem deathEffect;

    [SerializeField]
    public Slider bossHealthBar;

    public bool needsHealthBar;
    public GameObject healthBar;
    private Canvas parent;

    [Header("Element: Fire: 1 Water: 2 Air: 3 Earth:4")]
    public int element;

    public int pickupChance;
    public GameObject[] pickups;

    public bool isBoss;

    //death confirmation for hitCounter
    private bool isDead;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bossHealthBar = FindObjectOfType<Slider>();
        collider = GetComponent<BoxCollider2D>();
        if(isBoss)
        {
            bossHealthBar.maxValue = health;
            bossHealthBar.value = health;
        }
        if (needsHealthBar)
        {
            healthBar = (GameObject)Instantiate(healthBar, transform.position, Quaternion.identity);
            healthBar.GetComponent<UIAnchor>().objectToFollow = gameObject.transform;
           // healthBar.GetComponent<UIAnchor>().screenOffset = new Vector3(0, -40, 0);
            parent = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
            healthBar.transform.parent = parent.transform;

            healthBar.GetComponent<Slider>().maxValue = health;
            healthBar.GetComponent<Slider>().value = health;
            healthBar.gameObject.SetActive(true);
        }
        isDead = false;
    }



    public virtual bool TakeDamage(float damageAmount, int wepElement)
    {
        switch (element)
        {
            case 1:
                health = health - elementalBonus(damageAmount, wepElement);
                break;
            case 2:
                health = health - elementalBonus(damageAmount, wepElement);
                break;
            case 3:
                health = health - elementalBonus(damageAmount, wepElement);
                break;
            default:
                health = health - elementalBonus(damageAmount, wepElement);
                break;
        }

        if (health <= 0)
        {
            //loot
            int randomNumber = Random.Range(0, 101);
            if (randomNumber < pickupChance)
            {
                GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
                Instantiate(randomPickup, transform.position, transform.rotation);
            }
            Instantiate(deathEffect, transform.position, Quaternion.identity);

            healthBar.SetActive(false);
            Destroy(gameObject);
            Destroy(healthBar);
            isDead = true;
            return isDead;
        }
        if(!isBoss)
          healthBar.GetComponent<Slider>().value = health;
        else if(isBoss)
        {
            bossHealthBar.value = health;
        }

        return isDead;
    }

    public virtual float elementalBonus(float damageAmount,float wepElement)
    {
        float bonus;
        switch (element)
        {
            
            case 1:
                if (wepElement == 4)
                {
                    bonus = damageAmount / 2;
                }
                else if (wepElement == 2)
                {
                    bonus = damageAmount * 2;
                }
                else
                {
                    bonus = damageAmount;
                }
                Debug.Log(bonus);
                return bonus;
            case 2:
                if(wepElement == 1)
                {
                    bonus = damageAmount / 2;
                }
                else if (wepElement == 3)
                {
                    bonus = damageAmount * 2;
                }
                else
                {
                  bonus = damageAmount;
                }
                Debug.Log(bonus);
                return bonus;
            
            case 3:
                if (wepElement == 2)
                {
                    bonus = damageAmount / 2;
                }
                else if (wepElement == 4)
                {
                    bonus = damageAmount * 2;
                }
                else
                {
                    bonus = damageAmount;
                }
                Debug.Log(bonus);
                return bonus;
            default:
                if (wepElement == 3)
                {
                    bonus = damageAmount / 2;
                }
                else if (wepElement == 1)
                {
                    bonus = damageAmount * 2;
                }
                else
                {
                    bonus = damageAmount;
                }
                Debug.Log(bonus);
                return bonus;

                
        }
    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
        
    }


    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
        }
    }

}
